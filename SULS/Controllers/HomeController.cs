using System.Collections.Generic;
using SULS.ViewModels.Problems;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class HomeController: Controller
    {

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                return this.View(new List<HomePageProblemViewModel>(),"IndexLoggedIn");
            }
            else
            {
                return this.View();
            }

           
        }
    }
}