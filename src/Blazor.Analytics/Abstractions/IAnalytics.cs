using System.Threading.Tasks;

namespace Blazor.Analytics
{
    public interface IAnalytics
    {
        //Task Initialize(string trackingId);

        Task ConfigureExtra(string userId);

        Task TrackNavigation(string uri);

        Task TrackEvent(string eventName, string eventCategory = null, string eventLabel = null, int? eventValue = null);
        Task TrackEvent(string eventName, int eventValue, string eventCategory = null, string eventLabel = null);
        Task TrackEvent(string eventName, object eventData);

        void Enable();
        void Disable();
    }
}
