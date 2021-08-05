using System;
using System.Collections.Generic;
using System.Text;

namespace Blazor.Analytics.Abstractions
{
    public interface ITrackingNavigationState
    {
        void EnableTracking(bool globally = false);

        void DisableTracking(bool globally = false);

        bool IsTrackingEnabled();
    }
}
