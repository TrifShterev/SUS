using SULS.Services;
using SULS.ViewModels.Submissions;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class SubmissionsController: Controller
    {
        private readonly IProblemService _problemService;
        private readonly ISubmissionService _submissionService;

        public SubmissionsController(IProblemService problemService, ISubmissionService submissionService)
        {
            _problemService = problemService;
            _submissionService = submissionService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            var viewModel = new CreateViewModel
            {
                ProblemId = id,
                Name = this._problemService.GetNameById(id)
            };
            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, string code)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            if (string.IsNullOrEmpty(code)|| code.Length<30||code.Length>80)
            {
                return Error("Code should be between 30 and 800 characters long");
            }
            var userId = this.GetUserId();
            this._submissionService.Create(problemId,userId,code);
            return this.Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }
            this._submissionService.Delete(id);
            return this.Redirect("/");
        }
    }
}