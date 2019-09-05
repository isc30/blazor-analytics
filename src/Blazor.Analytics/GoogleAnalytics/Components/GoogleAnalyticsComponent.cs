using System;
using System.Threading.Tasks;
using Blazor.Analytics.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazor.Analytics.GoogleAnalytics.Components
{
    public class GoogleAnalyticsComponent : ComponentBase, IDisposable
    {
        [Parameter]
        protected string TrackingId { get; set; } = null;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = null;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = null;

        protected override async Task OnInitAsync()
        {
            base.OnInit();

            NavigationManager.LocationChanged += OnLocationChanged;

            await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.Configure,
                TrackingId);
        }

        public void Dispose()
        {
            NavigationManager.LocationChanged -= OnLocationChanged;
        }

        private async void OnLocationChanged(object sender, string absoluteUri)
        {
            var relativeUri = new Uri(absoluteUri).PathAndQuery;

            await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.Navigate,
                TrackingId, relativeUri);
        }
    }
}
