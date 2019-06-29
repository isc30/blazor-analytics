using System;
using System.Threading.Tasks;
using Blazor.Analytics.Constants;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Routing;

namespace Blazor.Analytics.GoogleAnalytics.Components
{
    public class GoogleAnalyticsComponent : ComponentBase, IDisposable
    {
        [Parameter]
        protected string TrackingId { get; set; }

        [Parameter]
        protected string ContainerId { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitAsync()
        {
            base.OnInit();

            UriHelper.OnLocationChanged += OnLocationChangedAsync;

        }

        protected override async Task OnAfterRenderAsync()
        {
            await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.Configure,
                TrackingId);

            if (this.ContainerId != null)
            {
                await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.GoogleTagManager, ContainerId);
            }

            base.OnAfterRenderAsync();
        }

        public void Dispose()
        {
            UriHelper.OnLocationChanged -= OnLocationChangedAsync;
        }

        private async void OnLocationChangedAsync(object sender, LocationChangedEventArgs e)
        {
            var relativeUri = new Uri(e.Location).PathAndQuery;

            await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.Navigate,
                TrackingId, relativeUri);

            //if (this.ContainerId != null)
            //{
            //    await JSRuntime.InvokeAsync<string>(GoogleAnalyticsInterop.GoogleTagManager, ContainerId);
            //}
        }

    }
}
