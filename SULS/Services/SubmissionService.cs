using System;
using System.Linq;
using SULS.Data;

namespace SULS.Services
{
    public class SubmissionService:ISubmissionService
    {
        private readonly ApplicationDbContext _db;
        private readonly Random _random;

        public SubmissionService(ApplicationDbContext db, Random random)
        {
            _db = db;
            _random = random;
        }

        public void Create(string problemId,string userId, string code)
        {
            var problemMaxPoints = _db.Problems
                .Where(x => x.Id == problemId)
                .Select(x => x.Points)
                .FirstOrDefault();

            var submission = new Submission
            {
                Code = code,
                ProblemId = problemId,
                UserId = userId,
                AchievedResult = _random.Next(0, problemMaxPoints + 1),
                CreatedOn = DateTime.UtcNow,
            };

            _db.Submissions.Add(submission);
            _db.SaveChanges();
        }

        public void Delete(string id)
        {
           
            var submission = this._db.Submissions.Find(id);
            this._db.Submissions.Remove(submission);
            this._db.SaveChanges();
        }
    }
}