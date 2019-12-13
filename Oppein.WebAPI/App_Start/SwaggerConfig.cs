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
                           .License(y => y.Name("By Oppein����"));

                        var dir = GetXmlCommentsPath();
                        c.IncludeXmlComments(dir);
                        //c.IgnoreObsoleteActions();
                        //c.DescribeAllEnumsAsStrings(camelCase: false);


                    })
                .EnableSwaggerUi(c =>
                    {
                        c.DocumentTitle("MSCS PlatForm");

                        c.InjectJavaScript(System.Reflection.Assembly.GetExecutingAssembly(), "Oppein.WebAPI.swagger.js");  //�û�����Swagger�����ϵ�Ӣ�ģ����磺��Ӧ�� (״̬ 200)�ȣ�
                    });
        }

        /// <summary>
        /// ��ȡxml·��
        /// </summary>
        /// <returns></returns>
        protected static string GetXmlCommentsPath()
        {
            return System.String.Format(@"{0}\bin\Oppein.WebAPI.XML", System.AppDomain.CurrentDomain.BaseDirectory);
        }
    }
}
