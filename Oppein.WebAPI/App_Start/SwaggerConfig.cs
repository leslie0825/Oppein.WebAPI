using System.Web.Http;
using WebActivatorEx;
using Oppein.WebAPI;
using Swashbuckle.Application;
using System;

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
                        c.SingleApiVersion("v1", "MSCS-I am Title")
                           .Description("MSCS-I am Description")
                           .License(y => y.Name("By Oppein制作"));

                        var dir = GetXmlCommentsPath();
                        c.IncludeXmlComments(dir);
                        //c.IgnoreObsoleteActions();
                        //c.DescribeAllEnumsAsStrings(camelCase: false);


                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("MSCS PlatForm");

                        c.InjectJavaScript(System.Reflection.Assembly.GetExecutingAssembly(), "Oppein.WebAPI.swagger.js");  //用户汉化Swagger界面上的英文（例如：响应类 (状态 200)等）
                    });
        }

        /// <summary>
        /// 获取xml路径
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\Oppein.WebAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
