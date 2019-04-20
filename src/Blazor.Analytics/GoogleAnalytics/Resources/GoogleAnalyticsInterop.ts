// declare window globals
interface Window
{
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

namespace GoogleAnalyticsInterop
{
    export function configure(trackingId: string): void
    {
        const script = document.createElement("script");
        script.async = true;
        script.src = "https://www.googletagmanager.com/gtag/js?id=" + trackingId;

        document.head.appendChild(script);

        gtag("config", trackingId);

        console.log(`[GTAG][${trackingId}] Configured`);
    }

    export function navigate(trackingId: string, href: string): void
    {
        gtag("config", trackingId, { page_path: href });

        console.log(`[GTAG][${trackingId}] Navigated: ${href}`);
    }
}
