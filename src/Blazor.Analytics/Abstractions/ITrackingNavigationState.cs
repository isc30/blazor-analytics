using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Analytics.Abstractions
{
    public interface ITrackingNavigationState
    {
        void EnableTracking();

        void DisableTracking();

        bool IsEnabledTracking();
    }
}
