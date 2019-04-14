using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static DataLibrary.BusinessLogic.Validator;
using static DataLibrary.BusinessLogic.Verifier;

namespace DataLibrary.BusinessLogic
{
    public class DeleteLibraryItemLogic
    {
        private bool IsVerified { get; set; }
        private bool IsValidated { get; set; }

        public AuthResponseModel DeleteLibraryItem(string token, int id, string category)
        {
            var validateResponse = SetResponse(ValidateInput(token, id));

            if (IsValidated)
            {
                var result = VerifyDeleteLibraryItem(token, id, category);
                var verifyResponse = SetResponse(result);

                if (IsVerified)
                {
                    return SetResponse(6);
                }

                return verifyResponse;
            }
            else
            {
                return validateResponse;
            }
        }

        private int ValidateInput(string token, int id)
        {
            return ValidateDeleteLibraryItem(token, id);
        }

        private AuthResponseModel SetResponse(int decision)
        {
            var jsonResponse = new AuthResponseModel();

            switch (decision)
            {
                case -1:
                    jsonResponse.Response = 401;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "No permission to complete this action.";
                    break;
                case 0:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "Could not complete this action. Contact Administrator.";
                    break;
                case 1:
                    IsVerified = true;
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
                    jsonResponse.Info = "Auth Token is required to complete this action.";
                    break;
                case 5:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "<Id> is required";
                    break;
                case 6:
                    jsonResponse.Response = 200;
                    jsonResponse.Status = "Success";
                    jsonResponse.Info = "Item Deleted successfully!";
                    break;
            }

            return jsonResponse;
        }
    }
}
