using Blazor.Analytics.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Analytics.GoogleAnalytics
{
    public class TrackingNavigationState : ITrackingNavigationState
    {
        private bool _isEnabledTracking = true;
        private bool _isGloballyEnabledTracking = true;

        public void DisableTracking(bool globally = false)
        {
            if (globally)
            {
                _isGloballyEnabledTracking = false;
            }

            _isEnabledTracking = false;
        }

        public void EnableTracking(bool globally = false)
        {
            if (globally)
            {
                _isGloballyEnabledTracking = true;
            }

            _isEnabledTracking = true;
        }

        public bool IsTrackingEnabled() => _isGloballyEnabledTracking && _isEnabledTracking;
    }
}
