using System;
using System.Configuration;
using GraphAnalysisCore.Normalization;
using GraphDataStorageCore.Context;
using GraphDataStorageCore.Repositories;
using GraphServicesHostApplication.Unity;
using Microsoft.Practices.Unity;

namespace GraphServicesHostApplication
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var mongoDbConnectionString = ConfigurationManager.AppSettings["MongoDbConnectionString"];
            Container.Instance.RegisterType<IGraphDbContext, GraphDbContext>(
                new InjectionConstructor(mongoDbConnectionString));
            Container.Instance.RegisterType<IGraphRepository, GraphRepository>();
            Container.Instance.RegisterType<IGraphNormalizer, UndirectedGraphNormalizer>();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
        }
    }
}