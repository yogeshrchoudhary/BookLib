using Microsoft.Extensions.DependencyInjection;

namespace BookLibServices
{
    public static class Bootstrapper
    {
        public static void ResolveDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBooksService, BooksServiceImpl>();
            OpenLibraryApiClient.Bootstrapper.ResolveDependencies(serviceCollection);
        }
    }
}
