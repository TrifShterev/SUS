using System.Collections.Generic;
using SULS.ViewModels.Problems;

namespace SULS.Services
{
    public interface IProblemService
    {
        void Create(string name, int points);

        IEnumerable<HomePageProblemViewModel> GetAll();

        string GetNameById(string id);

        public ProblemViewModel GetById(string id);
    }
}