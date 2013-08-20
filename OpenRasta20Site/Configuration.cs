using System;
using System.Web;
using OpenRasta.Configuration;
using OpenRasta20Site.Resources;
using OpenRasta20Site.Handlers;

namespace OpenRasta20Site
{
    public class Configuration : IConfigurationSource
    {
        public void Configure()
        {
            using (OpenRastaConfiguration.Manual)
            {
                ResourceSpace.Uses.PipelineContributor<ErrorCheckingContributor>();

                ResourceSpace.Has.ResourcesOfType<Home>()
                    .AtUri("/home")
                    .HandledBy<HomeHandler>()
                    .RenderedByAspx("~/Views/HomeView.aspx");
            }
        }
    }
}