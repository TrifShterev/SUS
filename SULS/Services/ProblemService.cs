using SULS.Data;
using SULS.ViewModels.Problems;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SULS.Services
{
    public class ProblemService : IProblemService
    {
        private readonly ApplicationDbContext _db;

        public ProblemService(ApplicationDbContext db)
        {
            _db = db;
        }
        public void Create(string name, int points)
        {
            var problem = new Problem
            {
                Name = name,
                Points = points
            };
            this._db.Problems.Add(problem);

            this._db.SaveChanges();
        }

        public IEnumerable<HomePageProblemViewModel> GetAll()
        {
            var problems = this._db.Problems.Select(x => new HomePageProblemViewModel
            {
                Id= x.Id,
                Name= x.Name,
                Count= x.Submissions.Count()
            }).ToList();

            return problems;
        }

        public string GetNameById(string id)
        {
            var problemName = this._db.Problems
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefault();

            return problemName;
        }

        public ProblemViewModel GetById(string id)
        {
            return this._db.Problems.Where(x => x.Id == id)
                .Select(x => new ProblemViewModel
                {
                    Name = x.Name,
                    Submissions = x.Submissions.Select(s => new SubmissionViewModel
                    {
                        CreatedOn = s.CreatedOn,
                        SubmissionId = s.Id,
                        AchievedResult = s.AchievedResult,
                        Username = s.User.Username,
                        MaxPoints = x.Points
                    })
                }).FirstOrDefault();
        }
    }
}