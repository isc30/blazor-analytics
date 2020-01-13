Blazor extensions for Analytics: Google Analytics, GTAG, ...<br/>
AspNetCore Version: 3.0.0

# NuGet Package
https://nuget.org/packages/Blazor-Analytics

# Configuration

## Google Analytics, GTAG

First, import the component in `_Imports.razor`

```
@using Blazor.Analytics.GoogleAnalytics.Components
```

Then, add the `GoogleAnalytics` component below your Router in `App.razor`.<br/>
The tracker listens to every navigation change while it's rendered on a page.

```diff
    <Router ... />
+   <GoogleAnalytics TrackingId="UA-XXXXXXXXX-X" />
```

### ServerSide Specific Configuration

Edit `_Host.cshtml` and apply the following change:

```diff
    <script src="_framework/blazor.server.js"></script>
+   <script src="_content/Blazor-Analytics/blazor-analytics.js"></script>
```


# Changelog
### v3.0.0
- Added support for
  - ServerSide (pre-rendering)
  - ServerSide (runtime)
  - WASM (runtime)
