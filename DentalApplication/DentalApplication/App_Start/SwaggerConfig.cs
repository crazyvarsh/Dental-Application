using System.Web.Http;
using WebActivatorEx;
using DentalApplication;
using Swashbuckle.Application;
using System.Net;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace DentalApplication
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "DentalApplication");
                        c.OperationFilter<AddAuthorizationHeaderParameterOperationFilter>();
                    })
                    .EnableSwaggerUi(c =>
                    {
                        c.EnableOAuth2Support(
                        clientId: "test-client-id",
                        clientSecret: null,
                        realm: "test-realm",
                        appName: "Swagger UI"
                    );
                        c.EnableApiKeySupport("apiKey", "header");
                        c.EnableOAuth2Support("sampleapi", "samplerealm", "Swagger UI");
                    });
        }
    }
}
