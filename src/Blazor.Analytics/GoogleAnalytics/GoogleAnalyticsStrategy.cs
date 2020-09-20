using System;
using System.Threading.Tasks;
using Blazor.Analytics.Constants;
using Microsoft.JSInterop;

namespace Blazor.Analytics.GoogleAnalytics
{
    public sealed class GoogleAnalyticsStrategy : IAnalytics
    {
        private readonly IJSRuntime _jsRuntime;

        private string _trackingId = null;
        public bool _isInitialized = false;

        public GoogleAnalyticsStrategy(
            IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        public void Configure(string trackingId)
        {
            _trackingId = trackingId;
        }

        public async Task Initialize(string trackingId)
        {
            if (trackingId == null)
            {
                throw new InvalidOperationException("Invalid TrackingId");
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.Configure, trackingId);

            _trackingId = trackingId;
            _isInitialized = true;
        }

        public async Task TrackNavigation(string uri)
        {
            if (!_isInitialized)
            {
                await Initialize(_trackingId);
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.Navigate, _trackingId, uri);
        }

        public async Task TrackEvent(
            string eventName,
            string eventValue,
            string eventCategory = null)
        {
            if (!_isInitialized)
            {
                await Initialize(_trackingId);
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.TrackEvent,
                _trackingId, eventName, eventValue, eventCategory);
        }
    }
}
