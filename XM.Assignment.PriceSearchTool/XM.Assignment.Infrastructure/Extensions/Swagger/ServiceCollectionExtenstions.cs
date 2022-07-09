using Microsoft.Extensions.DependencyInjection;

namespace XM.Assignment.Infrastructure.Extensions.Swagger;

public static partial class ServiceCollectionExtenstions
{
    private static IEnumerable<string> _documentedProjectNames = new[]
    {
        "XM.Assignment.API",
        "XM.Assignment.Domain"
    };

    public static IServiceCollection ConfigureSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            foreach (var assembly in 
                AppDomain.CurrentDomain.GetAssemblies().Where(a => _documentedProjectNames.Contains(a.GetName().Name)))
            {
                var xmlFileName = assembly.GetName().Name + ".xml";
                var xmlFilePath = Path.Combine(Path.GetDirectoryName(assembly.Location), xmlFileName);
                options.IncludeXmlComments(xmlFilePath);
            }
        });

        return services;
    }
}
