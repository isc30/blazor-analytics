using System;
using System.Threading.Tasks;
using Blazor.Analytics.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.JSInterop;

namespace Blazor.Analytics.GoogleAnalytics.Components
{
    public class GoogleAnalyticsComponent : ComponentBase, IDisposable
    {
        [Parameter]
        public string TrackingId { get; set; } = null;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = null;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            NavigationManager.LocationChanged -= OnLocationChanged;
            NavigationManager.LocationChanged += OnLocationChanged;

            await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.Configure,
                TrackingId);
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }

        private async void OnLocationChanged(object sender, LocationChangedEventArgs args)
        {
            var relativeUri = new Uri(args.Location).PathAndQuery;

            await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.Navigate,
                TrackingId, relativeUri);
        }
    }
}
