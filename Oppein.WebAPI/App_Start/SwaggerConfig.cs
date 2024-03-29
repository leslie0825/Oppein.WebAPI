﻿using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Routing.Constraints;
using System.Collections.Generic;

using Oppein.WebAPI;
using Swagger.Net.Application;
using Swagger.Net;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace Oppein.WebAPI
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        //c.SingleApiVersion("v1", "欧派");
                        //c.AccessControlAllowOrigin("*");
                        //c.IncludeAllXmlComments(thisAssembly, AppDomain.CurrentDomain.BaseDirectory);
                        //c.IgnoreIsSpecifiedMembers();
                        //c.DescribeAllEnumsAsStrings(camelCase: false);

                        c.SingleApiVersion("v1", "产品协同平台接口")
                        .Description("产品协同平台接口")
                        .License(y => y.Name("By Leslie"));

                        c.AccessControlAllowOrigin("*");
                        c.IncludeAllXmlComments(thisAssembly, AppDomain.CurrentDomain.BaseDirectory);
                        c.IgnoreIsSpecifiedMembers();

                        c.IgnoreObsoleteActions();
                        c.DescribeAllEnumsAsStrings(camelCase: false);
                    })
                .EnableSwaggerUi(c =>
                    {
                        //c.ShowExtensions(true);
                        //c.SetValidatorUrl("https://online.swagger.io/validator");
                        //c.UImaxDisplayedTags(100);
                        //c.UIfilter("''");

                        c.DocumentTitle("产品协同平台接口");
                        c.ShowExtensions(true);

                        c.InjectJavaScript(System.Reflection.Assembly.GetExecutingAssembly(), "Oppein.WebAPI.swagger.js");  //用户汉化Swagger界面上的英文（例如：响应类 (状态 200)等）
                    });
        }

        public static bool ResolveVersionSupportByRouteConstraint(ApiDescription apiDesc, string targetApiVersion)
        {
            return (apiDesc.Route.RouteTemplate.ToLower().Contains(targetApiVersion.ToLower()));
        }

        private class ApplyDocumentVendorExtensions : IDocumentFilter
        {
            public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
            {
                // Include the given data type in the final SwaggerDocument
                //
                //schemaRegistry.GetOrRegister(typeof(ExtraType));
            }
        }

        public class AssignOAuth2SecurityRequirements : IOperationFilter
        {
            public void Apply(Operation operation, SchemaRegistry schemaRegistry, ApiDescription apiDescription)
            {
                // Correspond each "Authorize" role to an oauth2 scope
                var scopes = apiDescription.ActionDescriptor.GetFilterPipeline()
                    .Select(filterInfo => filterInfo.Instance)
                    .OfType<AuthorizeAttribute>()
                    .SelectMany(attr => attr.Roles.Split(','))
                    .Distinct();

                if (scopes.Any())
                {
                    if (operation.security == null)
                        operation.security = new List<IDictionary<string, IEnumerable<string>>>();

                    var oAuthRequirements = new Dictionary<string, IEnumerable<string>>
                    {
                        { "oauth2", scopes }
                    };

                    operation.security.Add(oAuthRequirements);
                }
            }
        }

        private class ApplySchemaVendorExtensions : ISchemaFilter
        {
            public void Apply(Schema schema, SchemaRegistry schemaRegistry, Type type)
            {
                // Modify the example values in the final SwaggerDocument
                //
                if (schema.properties != null)
                {
                    foreach (var p in schema.properties)
                    {
                        switch (p.Value.format)
                        {
                            case "int32":
                                p.Value.example = 123;
                                break;
                            case "double":
                                p.Value.example = 9858.216;
                                break;
                        }
                    }
                }
            }
        }
    }
}
