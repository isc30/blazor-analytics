// declare window globals
interface Window {
    dataLayer: any[];
    gtag: (...args: any[]) => void;
}

// declare globals
declare const dataLayer: any[];
declare const gtag: (...args: any[]) => void;

// init globals
window.dataLayer = window.dataLayer || [];
window.gtag = window.gtag || function () { dataLayer.push(arguments); };

// configure first timestamp
gtag("js", new Date());

namespace GoogleAnalyticsInterop {
    export function configure(trackingId: string): void {
        const script = document.createElement("script");
        script.async = true;
        script.src = "https://www.googletagmanager.com/gtag/js?id=" + trackingId;

        document.head.appendChild(script);

        gtag("config", trackingId);

        console.log(`[GTAG][${trackingId}] Configured`);
    }

    export function navigate(trackingId: string, href: string): void {
        gtag("config", trackingId, { page_path: href });

        console.log(`[GTAG][${trackingId}] Navigated: ${href}`);
    }

    export function googleTagManager(containerId: string): void {
        const bodyScript = document.createElement("noscript");
        const iframe = document.createElement("iframe");
        iframe.src = "https://www.googletagmanager.com/ns.html?id=" + containerId;
        iframe.setAttribute("height", "0");
        iframe.setAttribute("width", "0");
        iframe.setAttribute("style", "display:none;visibility:hidden");
        bodyScript.appendChild(iframe);

        document.body.appendChild(bodyScript);
    }
}