using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SULS.Data;
using SULS.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS
{
    public class StartUp: IMvcApplication 
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {
            //Here registering the services
            serviceCollection.Add<IUsersService,UsersService>();
            serviceCollection.Add<IProblemService,ProblemService>();
            serviceCollection.Add<ISubmissionService,Submission>();
        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }
    }
}