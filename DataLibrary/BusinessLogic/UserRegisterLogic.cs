using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static DataLibrary.BusinessLogic.Validator;
using static DataLibrary.BusinessLogic.Verifier;

namespace DataLibrary.BusinessLogic
{
    public class UserRegisterLogic
    {
        private bool IsVerified { get; set; }
        private bool IsValidated { get; set; }

        public AuthResponseModel UserRegister(UserModel userModel)
        {
            var validateResponse = SetResponse(ValidateInput(userModel));

            if (IsValidated)
            {
                var result = VerifyRegistration(userModel);
                var verifyResponse = SetResponse(result);

                if (IsVerified)
                {
                    return SetResponse(8);
                }

                return verifyResponse;
            }
            else
            {
                return validateResponse;
            }
        }

        private int ValidateInput(UserModel userModel)
        {
            return ValidateUserRegistration(userModel);
        }

        private AuthResponseModel SetResponse(int decision)
        {
            var jsonResponse = new AuthResponseModel();

            switch (decision)
            {
                case 0:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "UserName already Taken";
                    break;
                case 1:
                    IsVerified= true;
                    break;
                case 2:
                    IsValidated = true;
                    break;
                case 3:
                    jsonResponse.Response = 500;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Internal server error";
                    break;
                case 4:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Invalid format for username";
                    break;
                case 5:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Invalid format for Email";
                    break;
                case 6:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Invalid format for password";
                    break;
                case 7:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "User type should be either 1/2";
                    break;
                case 8:
                    jsonResponse.Response = 200;
                    jsonResponse.Status = "Successful";
                    jsonResponse.Info = "Registration successful!";
                    break;
            }

            return jsonResponse;
        }
    }
}
