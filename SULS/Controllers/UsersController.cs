using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;
using SULS.Services;
using SULS.ViewModels.Users;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class UsersController:Controller
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        public HttpResponse Login()
        {
            if (this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            var userId = this._usersService.GetUserId(username, password);

            if (userId==null)
            {
                return this.Error("Invalid username or password");
            }
            this.SignIn(userId);
            return this.Redirect("/");
        }
        public HttpResponse Register()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterInputModel input)
        {
            if (string.IsNullOrEmpty(input.Username)||input.Username.Length<5 ||input.Username.Length>20)
            {
                return this.Error("Username should be between 5-20 characters");
            }
            if (!this._usersService.IsUsernameAvailable(input.Username))
            {
                return this.Error("Username already taken!");
            }

            if (string.IsNullOrEmpty(input.Email) || !new EmailAddressAttribute().IsValid(input.Email))
            {
                return this.Error("Invalid email address");
            }

            if (!this._usersService.IsEmailAvailable(input.Email))
            {
                return this.Error("Email already taken!");
            }

            if (string.IsNullOrEmpty(input.Password)||input.Password.Length<6||input.Password.Length>20)
            {
                return this.Error("Password must be between 6 and 20 characters!");
            }

            if (input.Password!=input.ConfirmPassword)
            {
                return this.Error("Passwords do not match!");
            }

            this._usersService.CreateUser(input.Username,input.Email,input.Password);

            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            this.SignOut();
            return this.Redirect("/");
        }
    }
}