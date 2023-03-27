using System;
using System.Collections.Generic;
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
        private Dictionary<string, object> _globalConfigData = new Dictionary<string, object>();
        private Dictionary<string, object> _globalEventData = new Dictionary<string, object>();
        private bool _isInitialized = false;
        private bool _debug = false;
        private bool _interopError = false;
        
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

            try
            {
                await _jsRuntime.InvokeAsync<string>(
                    GoogleAnalyticsInterop.Configure, _trackingId, _globalConfigData, _debug);
            }
            catch (JSException)
            {
                _interopError = true;
                Disable();
            }
            
            _isInitialized = true;
        }

        public async Task ConfigureGlobalConfigData(Dictionary<string, object> globalConfigData)
        {
            if (!_isInitialized)
            {
                this._globalConfigData = globalConfigData;

                await Initialize();
            }
        }

        public async Task ConfigureGlobalEventData(Dictionary<string, object> globalEventData)
        {
            this._globalEventData = globalEventData;
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

            if (_interopError)
            {
                return;
            }

            try
            {
                await _jsRuntime.InvokeAsync<string>(
                    GoogleAnalyticsInterop.Navigate, _trackingId, uri);
            }
            catch (JSException)
            {
                _interopError = true;
                Disable();
            }
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

            if (_interopError)
            {
                return;
            }

            try
            {
                await _jsRuntime.InvokeAsync<string>(
                    GoogleAnalyticsInterop.TrackEvent,
                    eventName, eventData, _globalEventData);
            }
            catch (JSException)
            {
                _interopError = true;
                Disable();
            }
        }

        public void Enable()
        {
            if (_interopError) 
                return;

            _isGloballyEnabledTracking = true;
        }

        public void Disable() => _isGloballyEnabledTracking = false;
    }
}
