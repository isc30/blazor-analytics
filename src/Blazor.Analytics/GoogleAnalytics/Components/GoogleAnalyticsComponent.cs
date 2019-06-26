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
        protected IUriHelper UriHelper { get; set; } = null;

        [Inject]
        protected IJSRuntime JSRuntime { get; set; } = null;

        protected override async Task OnInitAsync()
        {
            base.OnInit();
            
            UriHelper.OnLocationChanged += OnLocationChanged;

            await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.Configure,
                TrackingId);
        }

        public void Dispose()
        {
            UriHelper.OnLocationChanged -= OnLocationChanged;
        }

        private async void OnLocationChanged(object sender, Microsoft.AspNetCore.Components.Routing.LocationChangedEventArgs e)
        {
            var relativeUri = new Uri(e.Location).PathAndQuery;

            await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.Navigate,
                TrackingId, relativeUri);
        }

    }
}
