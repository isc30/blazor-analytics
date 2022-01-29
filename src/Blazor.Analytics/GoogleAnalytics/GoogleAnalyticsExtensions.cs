using Blazor.Analytics.Abstractions;
using Blazor.Analytics.GoogleAnalytics;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Analytics
{
    public static class GoogleAnalyticsExtensions
    {
        public static IServiceCollection AddGoogleAnalytics(
            this IServiceCollection services,
            string trackingId = null,
            bool debug = false,
            bool anonymizeIp = false)
        {
            services.AddScoped<ITrackingNavigationState, TrackingNavigationState>();
            return services.AddScoped<IAnalytics>(p =>
            {
                var googleAnalytics = ActivatorUtilities.CreateInstance<GoogleAnalyticsStrategy>(p);

                if (trackingId != null)
                {
                    googleAnalytics.Configure(trackingId, debug, anonymizeIp);
                }

                return googleAnalytics;
            });
        }
    }
}
