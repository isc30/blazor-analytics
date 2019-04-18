Blazor extensions for Analytics: Google Analytics, GTAG, ...

## Google Analytics, GTAG

First, import the component in `_ViewImports.cshtml`

```
@using Blazor.Analytics
@addTagHelper *, Blazor.Analytics
```

Then, add the `GoogleAnalytics` component below your Router in `App.cshtml`.<br/>
The tracker listens to every navigation change while it's rendered on a page.

```
<Router AppAssembly="typeof(App).Assembly" />
<GoogleAnalytics TrackingId="UA-XXXXXXXXX-X" />
```
