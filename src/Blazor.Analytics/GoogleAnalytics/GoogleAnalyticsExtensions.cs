using Blazor.Analytics.GoogleAnalytics;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Analytics
{
    public static class GoogleAnalyticsExtensions
    {
        public static IServiceCollection AddGoogleAnalytics(this IServiceCollection services) => AddGoogleAnalytics(services, null);

        public static IServiceCollection AddGoogleAnalytics(
            this IServiceCollection services,
            string trackingId)
        {
            return services.AddScoped<IAnalytics>(p =>
            {
                var googleAnalytics = ActivatorUtilities.CreateInstance<GoogleAnalyticsStrategy>(p);

                if (trackingId != null)
                {
                    googleAnalytics.Configure(trackingId);
                }

                return googleAnalytics;
            });
        }
    }
}
