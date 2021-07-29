using Blazor.Analytics.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Analytics.GoogleAnalytics
{
    public class TrackingNavigationState : ITrackingNavigationState
    {
        private bool _isEnableTracking = true;

        public void DisableTracking() => _isEnableTracking = false;

        public void EnableTracking() => _isEnableTracking = true;

        public bool IsTrackingEnabled() => _isEnableTracking;
    }
}
