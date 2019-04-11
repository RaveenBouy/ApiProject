using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.Validator;
using static DataLibrary.BusinessLogic.Verifier;

namespace API.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        private string Token { get; set; }
        private bool IsVerified { get; set; }
        private bool IsValidated { get; set; }
        private new UserModel User { get; set; } = new UserModel();

        [HttpGet]
        [Route("api/login")]
        public AuthResponseModel UserLogin([FromQuery] string username, string password)
        {
            var validateResponse = SetResponse(ValidateInput(username, password));

            if (IsValidated)
            {
                var (result, userModel) = VerifyUser(username, password);
                var verifyResponse = SetResponse(result);
                User = userModel;

                if (IsVerified)
                {
                    return SetResponse(5);
                }

                 return verifyResponse;
            }
            else
            {
                return validateResponse;
            }
        }

        [HttpPost]
        [Route("api/register")]
        public UserModel UserRegister([FromBody] UserModel userModel)
        {
            UserProcessor.RegisterUser(userModel);
            return userModel;
        }

        private (int, UserModel) VerifyUser(string username, string password)
        {
            return VerifyeUser(username, password);
        }

        private int ValidateInput(string username, string password)
        {
            return ValidateUser(username, password);
        }

        private string GenerateUserToken()
        {
            Token = TokenProcessor.GenerateToken();

            while (!VerifyToken(Token))
            {
                Token = TokenProcessor.GenerateToken();
            }

            using (var bgWorker = new BackgroundWorker())
            {
                bgWorker.DoWork += BgWorkerOnDoWork;
                bgWorker.RunWorkerAsync();
                return Token;
            }
        }

        private void BgWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            TokenProcessor.WriteAuthenticationToken(User.Id, Token);
        }

        private AuthResponseModel SetResponse(int decision)
        {
            var jsonResponse = new AuthResponseModel();

            switch (decision)
            {
                case 1:
                    jsonResponse.Response = 403;
                    jsonResponse.Error = "Auth failed";
                    break;
                case 2:
                    jsonResponse.Response = 403;
                    jsonResponse.Error = "Invalid username";
                    break;
                case 3:
                    jsonResponse.Response = 403;
                    jsonResponse.Error = "Invalid password";
                    break;
                case 4:
                    jsonResponse.Response = 500;
                    jsonResponse.Error = "Internal server error";
                    break;
                case 5:
                    jsonResponse.Response = 200;
                    jsonResponse.Error = null;
                    jsonResponse.Token = GenerateUserToken();
                    break;
                case 6:
                    IsVerified = true;
                    break;
                case 7:
                    IsValidated = true;
                    break;
            }

            return jsonResponse;
        }

    }
}