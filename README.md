Blazor extensions for Analytics: Google Analytics, GTAG, ...

Blazor Version: 3.0.0-preview9-19424-04

# NuGet Package
https://nuget.org/packages/Blazor-Analytics

# Configuration

### Google Analytics, GTAG

First, import the component in `_Imports.razor`

```
@using Blazor.Analytics.GoogleAnalytics.Components
```

Then, add the `GoogleAnalytics` component below your Router in `App.razor`.<br/>
The tracker listens to every navigation change while it's rendered on a page.

```
<Router AppAssembly="typeof(App).Assembly" />
<GoogleAnalytics TrackingId="UA-XXXXXXXXX-X" />
```
