using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Analytics
{
    public interface IAnalytics
    {
        Task ConfigureGlobalConfigData(Dictionary<string, object> globalConfigData);
        void ConfigureGlobalEventData(Dictionary<string, object> globalEventData);

        Task TrackNavigation(string uri);

        Task TrackEvent(string eventName, string eventCategory = null, string eventLabel = null, int? eventValue = null);
        Task TrackEvent(string eventName, int eventValue, string eventCategory = null, string eventLabel = null);
        Task TrackEvent(string eventName, object eventData);

        void Enable();
        void Disable();
    }
}
