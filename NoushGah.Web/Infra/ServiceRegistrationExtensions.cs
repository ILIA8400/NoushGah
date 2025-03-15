using System.Reflection;

namespace NoushGah.Web.Infra
{
    public static class ServiceRegistrationExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services, params Assembly[] assemblies)
        {
            var allTypes = assemblies.SelectMany(a => a.GetTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .ToList();

            foreach (var implementationType in allTypes)
            {
                var interfaceType = implementationType.GetInterfaces()
                    .FirstOrDefault(i => "I" + implementationType.Name == i.Name);

                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, implementationType);
                }
            }
        }
    }
}
