using ProjectManager.Data.Models.Custom;
using System.Collections.Generic;
using System.Web.Http;
using ProjectManager.Business;

namespace ProjectManager.Controllers
{
    public class UserController : ApiController
    {
        UserBusiness userBusiness;

        [Route("UserList")]
        [HttpGet]
        public List<UserModel> GetActiveUserList()
        {
            userBusiness = new UserBusiness();
            var result = userBusiness.GetActiveUserList();
            return result;
        }


        [Route("AddUpdate")]
        [HttpPost]
        public string AddOrUpdateUser(object user)
        {
            string result = string.Empty;
            userBusiness = new UserBusiness();
            result = userBusiness.AddOrUpdateUser(user);
            return result;
        }

        [Route("Delete")]
        [HttpPost]
        public bool DeleteUser(object user)
        {
            userBusiness = new UserBusiness();
            return userBusiness.DeleteUser(user);
        }
    }
}
