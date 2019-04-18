// init globals
window.dataLayer = window.dataLayer || [];
window.gtag = window.gtag || function () { dataLayer.push(arguments); };
// configure first timestamp
gtag("js", new Date());
var GoogleAnalyticsInterop;
(function (GoogleAnalyticsInterop) {
    function configure(trackingId) {
        var script = document.createElement("script");
        script.src = "https://www.googletagmanager.com/gtag/js?id=" + trackingId;
        document.head.appendChild(script);
        gtag("config", trackingId);
        console.log("[GTAG][" + trackingId + "] Configured");
    }
    GoogleAnalyticsInterop.configure = configure;
    function navigate(trackingId, href) {
        gtag("config", trackingId, { page_path: href });
        console.log("[GTAG][" + trackingId + "] Navigated: " + href);
    }
    GoogleAnalyticsInterop.navigate = navigate;
})(GoogleAnalyticsInterop || (GoogleAnalyticsInterop = {}));
//# sourceMappingURL=GoogleAnalyticsInterop.js.map