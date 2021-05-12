Blazor extensions for Analytics: Google Analytics, GTAG, etc...<br/>
AspNetCore Version: 3.1.8

# NuGet Package
https://nuget.org/packages/Blazor-Analytics

# Configuration

## For Every Tracker

First, import the namespaces in `_Imports.razor`

```
@using Blazor.Analytics
@using Blazor.Analytics.Components
```

Then, add the `NavigationTracker` component below your Router in `App.razor`.<br/>
The tracker listens to every navigation change while it's rendered on a page.

```diff
    <Router ... />
+   <NavigationTracker />
```

### ServerSide Specific Configuration

Edit `_Host.cshtml` and apply the following change:

```diff
    <script src="_framework/blazor.server.js"></script>
+   <script src="_content/Blazor-Analytics/blazor-analytics.js"></script>
```

### WASM Specific Configuration

Edit `index.html` and apply the following change:

```diff
    <script src="_framework/blazor.webassembly.js"></script>
+   <script src="_content/Blazor-Analytics/blazor-analytics.js"></script>
```

## Setting up GoogleAnalytics

Inside your main `Startup`/`Program`, call `AddGoogleAnalytics`. This will configure your GTAG_ID automatically.

```diff
+   services.AddGoogleAnalytics("YOUR_GTAG_ID");
```

# How to trigger an Analytics Event

1. Inject `IAnalytics` wherever you want to trigger the event.
2. Call `IAnalytics.TrackEvent` passing the `EventName` and `EventData` (an object containing the event data).
<br>Or<br>
 Call `IAnalytics.TrackEvent` passing the `EventName`, `Value` and `Category` (optional).

```
@inject Blazor.Analytics.IAnalytics Analytics

Analytics.TrackEvent("generate_lead", new {currency = "USD", value = 99.99});
```

# Changelog
### v3.1.0
- Support for Events
### v3.0.0
- Added support for
  - ServerSide (pre-rendering)
  - ServerSide (runtime)
  - WASM (runtime)
