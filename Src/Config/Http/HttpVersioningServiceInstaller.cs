using Asp.Versioning;

namespace UserService.Config.Http
{
    public class HttpVersioningServiceInstaller : IServiceInstaller
    {
        public void Install(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApiVersioning(
                options =>
                {
                    // reporting api versions will return the headers
                    // "api-supported-versions" and "api-deprecated-versions"
                    options.ReportApiVersions = true;

                    options.Policies.Sunset(0.9)
                                    .Effective(DateTimeOffset.Now.AddDays(60))
                                    .Link("policy.html")
                                        .Title("Versioning Policy")
                                        .Type("text/html");
                })
                .AddApiExplorer(
                    options =>
                    {
                        // add the versioned api explorer, which also adds IApiVersionDescriptionProvider service
                        // note: the specified format code will format the version as "'v'major[.minor][-status]"
                        options.GroupNameFormat = "'v'VVV";

                        // note: this option is only necessary when versioning by url segment. the SubstitutionFormat
                        // can also be used to control the format of the API version in route templates
                        options.SubstituteApiVersionInUrl = true;
                    }
                );
        }
    }
}