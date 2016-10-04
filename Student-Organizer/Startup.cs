using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Student_Organizer.Startup))]
namespace Student_Organizer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
