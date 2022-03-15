// declare window globals
interface Window
{
    dataLayer: any[];
    gtag: (...args: any[]) => void;
}

interface ConfigObject {
    [key: string]: any
}

interface EventDataObject {
    [key: string]: any
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
    export function configure(trackingId: string, userId: string, debug: boolean = false): void
    {
        this.userId = userId;
        this.debug = debug;
        const script = document.createElement("script");
        script.async = true;
        script.src = "https://www.googletagmanager.com/gtag/js?id=" + trackingId;

        document.head.appendChild(script);

        let configObject: ConfigObject = {};
        configObject.send_page_view = false;
        if (userId !== null && userId !== undefined) {
            configObject.user_id = userId;
        }

        gtag("config", trackingId, configObject);

        if(this.debug){
            console.log(`[GTAG][${trackingId}] Configured!`);
        }
    }

    export function navigate(trackingId: string, href: string): void
    {
        let configObject: ConfigObject = {};

        configObject.page_location = href;
        if (this.userId !== null && this.userId !== undefined) {
            configObject.user_id = this.userId;
        }

        gtag("config", trackingId, configObject);

        if(this.debug){
            console.log(`[GTAG][${trackingId}] Navigated: '${href}'`);
        }
    }

    export function trackEvent(eventName: string, eventData: EventDataObject)
    {
        if (this.userId !== null && this.userId !== undefined) {
            eventData.user_id = this.userId;
        }

        gtag("event", eventName, eventData);
        if (this.debug) {
          console.log(`[GTAG][Event triggered]: ${eventName}`);
        }
    }
}
