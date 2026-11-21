# Gravion Raxon MK68HE web-driver (XJ68 Offline WebHID Configurator)

Offline local server and mirrored web configurator for the XJ68 magnetic keyboard (`VID_5577&PID_A7EC`), archived from `raxon.graviontr.com` (originally hosted on `liulistudio.cn`).

## Build

Open PowerShell in the project folder and compile `driver.cs`:

```powershell
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe /target:exe /out:XJ68Launcher.exe driver.cs
```

## Run

Run `XJ68Launcher.exe` (keep the `site` folder right beside it) and open the configurator in a WebHID-supported browser such as Chrome or Edge., or you can install the ZIP from relases and use the exe there.
