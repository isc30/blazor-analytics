using System.Threading.Tasks;

namespace Blazor.Analytics
{
    public interface IAnalytics
    {
        Task Initialize(string trackingId);

        Task TrackNavigation(string uri);

        Task TrackEvent(string eventName, string eventValue, string eventCategory = null);
    }
}
