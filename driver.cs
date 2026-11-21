using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

class Program {
    static string RootDir = GetRootDir();
    static string Url = "http://localhost:8080/";

    static string GetRootDir() {
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string localSite = Path.Combine(baseDir, "site");
        if (Directory.Exists(localSite)) {
            return Path.GetFullPath(localSite);
        }

        string fallback = @"D:\XJ68-WebDriver\site";
        if (Directory.Exists(fallback)) {
            return Path.GetFullPath(fallback);
        }

        return Path.GetFullPath(localSite);
    }

    static void Main() {
        HttpListener listener = new HttpListener();
        listener.Prefixes.Add(Url);
        try {
            listener.Start();
        } catch (Exception ex) {
            Console.WriteLine("Failed to bind port 8080: " + ex.Message);
            Console.ReadLine();
            return;
        }

        Console.WriteLine("Serving: " + RootDir);
        Console.WriteLine("XJ68 WebDriver running at " + Url + "drive/");
        Console.WriteLine("Keep this window open while using the configurator. Press Enter to exit.");

        ThreadPool.QueueUserWorkItem(_ => {
            Thread.Sleep(400);
            try {
                Process.Start(Url + "drive/");
            } catch {}
        });

        ThreadPool.QueueUserWorkItem(_ => {
            while (listener.IsListening) {
                try {
                    HttpListenerContext ctx = listener.GetContext();
                    ThreadPool.QueueUserWorkItem(__ => HandleRequest(ctx));
                } catch {
                    break;
                }
            }
        });

        Console.ReadLine();
        listener.Stop();
        listener.Close();
    }

    static void HandleRequest(HttpListenerContext ctx) {
        try {
            string path = ctx.Request.Url.AbsolutePath;
            if (path == "/" || path == "/drive" || path == "/drive/") {
                path = "/drive/index.html";
            }

            string relPath = path.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            string fullRoot = Path.GetFullPath(RootDir);
            if (!fullRoot.EndsWith(Path.DirectorySeparatorChar.ToString())) {
                fullRoot += Path.DirectorySeparatorChar;
            }

            string fullPath = Path.GetFullPath(Path.Combine(fullRoot, relPath));
            if (!fullPath.StartsWith(fullRoot, StringComparison.OrdinalIgnoreCase) || !File.Exists(fullPath)) {
                ctx.Response.StatusCode = 404;
                ctx.Response.Close();
                return;
            }

            ctx.Response.ContentType = GetMimeType(Path.GetExtension(fullPath));
            byte[] data = File.ReadAllBytes(fullPath);
            ctx.Response.ContentLength64 = data.Length;
            ctx.Response.OutputStream.Write(data, 0, data.Length);
            ctx.Response.OutputStream.Close();
        } catch {
            try { ctx.Response.Abort(); } catch {}
        }
    }

    static string GetMimeType(string ext) {
        switch (ext.ToLowerInvariant()) {
            case ".html": return "text/html; charset=utf-8";
            case ".js": return "application/javascript; charset=utf-8";
            case ".css": return "text/css; charset=utf-8";
            case ".json": return "application/json; charset=utf-8";
            case ".svg": return "image/svg+xml";
            case ".png": return "image/png";
            case ".jpg":
            case ".jpeg": return "image/jpeg";
            case ".gif": return "image/gif";
            case ".ico": return "image/x-icon";
            case ".wasm": return "application/wasm";
            default: return "application/octet-stream";
        }
    }
}