// declare window globals
interface Window
{
    dataLayer: any[];
    gtag: (...args: any[]) => void;
}

interface ObjectConstructor {
    assign(...objects: Object[]): Object;
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
    export function configure(trackingId: string, globalConfigObject: ConfigObject, debug: boolean = false): void
    {
        this.debug = debug;
        this.globalConfigObject = globalConfigObject;
        const script = document.createElement("script");
        script.async = true;
        script.src = "https://www.googletagmanager.com/gtag/js?id=" + trackingId;

        document.head.appendChild(script);

        let configObject: ConfigObject = {};
        configObject.send_page_view = false;
        Object.assign(configObject, globalConfigObject)

        gtag("config", trackingId, configObject);

        if(this.debug){
            console.log(`[GTAG][${trackingId}] Configured!`);
        }
    }

    export function navigate(trackingId: string, href: string): void
    {
        let configObject: ConfigObject = {};

        configObject.page_location = href;
        Object.assign(configObject, this.globalConfigObject)
        gtag("config", trackingId, configObject);

        if(this.debug){
            console.log(`[GTAG][${trackingId}] Navigated: '${href}'`);
        }
    }

    export function trackEvent(eventName: string, eventData: EventDataObject, globalEventData: EventDataObject)
    {
        Object.assign(eventData, globalEventData)

        gtag("event", eventName, eventData);
        if (this.debug) {
          console.log(`[GTAG][Event triggered]: ${eventName}`);
        }
    }
}
