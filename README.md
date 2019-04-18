Blazor extensions for Analytics: Google Analytics, GTAG, ...

## Google Analytics, GTAG
Add the `GoogleAnalytics` component below your Router in `App.cshtml`.<br/>
The tracker tracks every navigation when it's rendered on a page.

```
<Router AppAssembly="typeof(App).Assembly" />
<GoogleAnalytics TrackingId="UA-XXXXXXXXX-X" />
```
