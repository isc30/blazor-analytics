using Microsoft.AspNetCore.Builder;
using System;
using System.IO;
using System.Reflection;

namespace Blazor.Analytics
{
    /// <summary>
    /// Referenced https://github.com/Mewriick/Blazor.FlexGrid/blob/master/src/Blazor.FlexGrid/ApplicationBuilderExtensions.cs
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBlazorAnalytics(this IApplicationBuilder applicationBuilder, string webRootPath)
        {
            var flexGridAssembly = Assembly.GetExecutingAssembly();
            var resources = flexGridAssembly.GetManifestResourceNames();

            var destinationFolderPath = Path.Combine(webRootPath, "Blazor.Analytics");
            if (!Directory.Exists(destinationFolderPath))
            {
                Directory.CreateDirectory(destinationFolderPath);
            }

            foreach (var resource in resources)
            {
                var colonPosition = resource.LastIndexOf(":");
                var resourceName = resource.Substring(colonPosition + 1, resource.Length - colonPosition - 1);
                AddResource(flexGridAssembly, destinationFolderPath, resource, resourceName);
            }

            return applicationBuilder;
        }

        private static void AddResource(Assembly flexGridAssembly, string destinationFolderPath, string resource, string resourceName)
        {
            using (var resourceStream = flexGridAssembly.GetManifestResourceStream(resource))
            {
                if (resourceStream == null)
                {
                    throw new Exception($"Couldn't find the resource '{resource}' in the assembly");
                }

                var bufferSize = 1024 * 1024;
                using (var fileStream = new FileStream(Path.Combine(destinationFolderPath, resourceName), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fileStream.SetLength(resourceStream.Length);
                    var bytesRead = -1;
                    var bytes = new byte[bufferSize];

                    while ((bytesRead = resourceStream.Read(bytes, 0, bufferSize)) > 0)
                    {
                        fileStream.Write(bytes, 0, bytesRead);
                    }
                }
            }
        }
    }
}
