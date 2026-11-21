import{k as f,r as t,c as p,aa as m,o as _,e as v,$ as l,a0 as w,_ as n,X as i}from"./vue-vendor-DQaUVmVM.js";import{_ as b}from"./index-BlvsD3cH.js";import"./vendor-A6E2TEUN.js";import"./tdesign-zxiacUkM.js";import"./axios-B9ygI19o.js";const g={class:"webview-page"},y={key:0,class:"webview-page__loading"},k=["src"],h=f({__name:"index",setup(x){const u=m(),e=t(null),a=t(!0),r=p(()=>u.query.url||"");function c(){a.value=!1}function d(){if(e.value){e.value.src="about:blank";const o=e.value;o.parentNode?.removeChild(o),e.value=null}}return _(()=>{r.value||(a.value=!1)}),v(()=>{d()}),(o,s)=>(i(),l("div",g,[a.value?(i(),l("div",y,[...s[0]||(s[0]=[n("div",{class:"webview-page__spinner"},null,-1),n("span",null,"加载中...",-1)])])):w("",!0),n("iframe",{ref_key:"iframeRef",ref:e,src:r.value,class:"webview-page__iframe",frameborder:"0",allow:`
        hid;
        usb;
        serial;
        bluetooth;
        accelerometer;
        autoplay;
        clipboard-write;
        encrypted-media;
        gyroscope;
        picture-in-picture;
        fullscreen;
      `,allowfullscreen:"",onLoad:c},null,40,k)]))}}),V=b(h,[["__scopeId","data-v-60c01246"]]);export{V as default};
