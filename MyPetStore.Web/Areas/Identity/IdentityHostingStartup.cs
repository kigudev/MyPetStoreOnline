using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ASPNETIdentity.Areas.Identity.IdentityHostingStartup))]

namespace ASPNETIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
            });
        }
    }
}