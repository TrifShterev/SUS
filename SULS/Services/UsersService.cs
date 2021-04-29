using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SULS.Data;

namespace SULS.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateUser(string username, string email, string password)
        {
            var user = new User
            {
                Email = email,
                Username = username,
                Password = ComputeHash(password)
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var passwordHash = ComputeHash(password);

            var user = db.Users.FirstOrDefault(x => x.Username == username && x.Password == passwordHash);

            //this is the same as return user ==null ? null :user.Id (if is NULL return NULL otherwise return user.Id)
            return user?.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return !this.db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !this.db.Users.Any(x => x.Username == username);
        }
        //Hashing method-algorithm by Niki Kostov
        private static string ComputeHash(string input)
        {
            var bytes = Encoding.UTF8.GetBytes(input);
            using var hash = SHA512.Create();
            var hashedInputBytes = hash.ComputeHash(bytes);
            // Convert to text
            // StringBuilder Capacity is 128, because 512 bits / 8 bits in byte * 2 symbols for byte 
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
}