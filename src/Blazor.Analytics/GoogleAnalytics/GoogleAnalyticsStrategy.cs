using System;
using System.Threading.Tasks;
using Blazor.Analytics.Abstractions;
using Blazor.Analytics.Constants;
using Microsoft.JSInterop;

namespace Blazor.Analytics.GoogleAnalytics
{
    public sealed class GoogleAnalyticsStrategy : IAnalytics
    {
        private readonly IJSRuntime _jsRuntime;
        private bool _isGloballyEnabledTracking = true;

        private string _trackingId = null;
        private string _userId = null;
        private bool _isInitialized = false;
        private bool _debug = false;

        public GoogleAnalyticsStrategy(
            IJSRuntime jsRuntime
        )
        {
            _jsRuntime = jsRuntime;
        }

        public void Configure(string trackingId, bool debug)
        {
            _trackingId = trackingId;
            _debug = debug;
        }

        private async Task Initialize()
        {
            if (_trackingId == null)
            {
                throw new InvalidOperationException("Invalid TrackingId");
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.Configure, _trackingId, _userId, _debug);
            
            _isInitialized = true;
        }

        public async Task ConfigureExtra(string userId)
        {
            if (!_isInitialized)
            {
                this._userId = userId;

                await Initialize();
            }
        }

        public async Task TrackNavigation(string uri)
        {
            if (!_isGloballyEnabledTracking)
            {
                return;
            }

            if (!_isInitialized)
            {
                await Initialize();
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.Navigate, _trackingId, uri);
        }

        public async Task TrackEvent(
            string eventName,
            string eventCategory = null,
            string eventLabel = null,
            int? eventValue = null)
        {
            await TrackEvent(eventName, new
            {
                event_category = eventCategory, 
                event_label = eventLabel, 
                value = eventValue
            });
        }

        public Task TrackEvent(string eventName, int eventValue, string eventCategory = null, string eventLabel = null)
        {
            return TrackEvent (eventName, eventCategory, eventLabel, eventValue);
        }

        public async Task TrackEvent(string eventName, object eventData)
        {
            if (!_isGloballyEnabledTracking)
            {
                return;
            }

            if (!_isInitialized)
            {
                await Initialize();
            }

            await _jsRuntime.InvokeAsync<string>(
                GoogleAnalyticsInterop.TrackEvent,
                eventName, eventData);
        }

        public void Enable() => _isGloballyEnabledTracking = true;

        public void Disable() => _isGloballyEnabledTracking = false;
    }
}
