using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using static DataLibrary.BusinessLogic.Validator;
using static DataLibrary.BusinessLogic.Verifier;

namespace DataLibrary.BusinessLogic
{
    public class UserLoginLogic
    {
        private string Token { get; set; }
        private bool IsVerified { get; set; }
        private int UserRole { get; set; }
        private bool IsValidated { get; set; }
        private new UserModel User { get; set; } = new UserModel();

        public AuthResponseModel Login(string username, string password)
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

            while (VerifyToken(Token))
            {
                Token = TokenProcessor.GenerateToken();
            }

            UserRole = TokenProcessor.WriteAuthenticationToken(User.Id, Token);

            return Token;
        }

        private void BgWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            
        }

        private AuthResponseModel SetResponse(int decision)
        {
            var jsonResponse = new AuthResponseModel();

            switch (decision)
            {
                case 0:
                    jsonResponse.Response = 401;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "User account is not active yet. Contact the librarian.";
                    break;
                case 1:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Auth failed";
                    break;
                case 2:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Invalid username";
                    break;
                case 3:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Invalid password";
                    break;
                case 4:
                    jsonResponse.Response = 500;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Internal server error";
                    break;
                case 5:
                    jsonResponse.Response = 200;
                    jsonResponse.Status = "Success";
                    jsonResponse.Token = GenerateUserToken();
                    jsonResponse.Value = UserRole;
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
