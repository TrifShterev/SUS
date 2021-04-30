using System.Collections.Generic;
using SULS.Services;
using SULS.ViewModels.Problems;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class HomeController: Controller
    {
        private readonly IProblemService _problemService;

        public HomeController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserSignedIn())
            {
                var viewModel = _problemService.GetAll();

                return this.View(viewModel,"IndexLoggedIn");
            }
            else
            {
                return this.View();
            }

           
        }
    }
}