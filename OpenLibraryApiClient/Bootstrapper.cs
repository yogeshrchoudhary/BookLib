using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("OpenLibraryApiClient.Tests")]
namespace OpenLibraryApiClient
{
    public static class Bootstrapper
    {
        public static void ResolveDependencies(IServiceCollection services)
        {
            services.AddScoped<IBookStore, BookStore>();
        }
    }
}
