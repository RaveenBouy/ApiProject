using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static DataLibrary.BusinessLogic.Validator;
using static DataLibrary.BusinessLogic.Verifier;

namespace DataLibrary.BusinessLogic
{
    public class UpdateLibraryItemLogic
    {
        private bool IsVerified { get; set; }
        private bool IsValidated { get; set; }

        public AuthResponseModel UpdateLibraryItem(DynamicUpdateModel updateModel, string category)
        {
            var validateResponse = SetResponse(ValidateInput(updateModel.AuthToken, updateModel.Id, updateModel.Type, updateModel.Value));

            if (IsValidated)
            {
                var result = VerifyUpdateLibraryItem(updateModel.AuthToken, updateModel.Id, updateModel.Type, updateModel.Value, category);
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

        private int ValidateInput(string token, int id, string type, string value)
        {
            return ValidateUpdateLibraryItem(token, id, type, value);
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
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "<Type> is required";
                    break;
                case 7:
                    jsonResponse.Response = 403;
                    jsonResponse.Status = "Error";
                    jsonResponse.Info = "<Value> is required";
                    break;
                case 8:
                    jsonResponse.Response = 200;
                    jsonResponse.Status = "Success";
                    jsonResponse.Info = "Item Updated successfully!";
                    break;
            }

            return jsonResponse;
        }
    }
}
