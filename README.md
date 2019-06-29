
# Folked from https://github.com/isc30/blazor-analytics
Because I would like to add some more features what would break the orginal ideas of isc30

Blazor extensions for Analytics: Google Analytics, GTAG, ...

Blazor Version: 3.0.0-preview6.19307.2

# NuGet Package (Original package)
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
<GoogleAnalytics TrackingId="UA-XXXXXXXXX-X" ContainerId="GTM-XXXXXXX" />
```
Remove `ContainerId` attribute if you don't use Google Tag Manager

### Blazor Server Side Rendering Project
At least for `3.0.0-preview6.19307.2`, We need to manually add scrips to `head` tag:
```
<environment include="Development">
    <link rel="stylesheet" href="css/bootstrap/bootstrap.min.css" />
    <script src="_content/Blazor-Analytics/blazor-analytics.js"></script>
</environment>
```
https://devblogs.microsoft.com/aspnet/asp-net-core-and-blazor-updates-in-net-core-3-0-preview-6/