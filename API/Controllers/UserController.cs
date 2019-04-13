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
        [HttpGet]
        [Route("api/login")]
        public AuthResponseModel UserLogin([FromQuery] string username, string password)
        {
            UserLoginLogic login = new UserLoginLogic();
            return login.Login(username, password);
        }

        [HttpPost]
        [Route("api/register")]
        public AuthResponseModel UserRegister([FromBody] UserModel userModel)
        {
            UserRegisterLogic logic = new UserRegisterLogic();
            return logic.UserRegister(userModel);
        }
    }
}