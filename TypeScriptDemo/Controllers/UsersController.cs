using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TypeScriptDemo.Entity.POCO;

namespace TypeScriptDemo.Controllers
{
    public class UsersController : Controller
    {
        private readonly CodeHubsAPIContext _context = null;

        public UsersController()
        {
            _context = new CodeHubsAPIContext();
        }

        [Route("api/users")]
        public JsonResult GetUsers() => Json(new { userList = _context.Users.ToList() }, JsonRequestBehavior.AllowGet);

        [Route("api/users/{userId}")]
        public JsonResult GetUserById(int userId) => Json(new { user = _context.Users.FirstOrDefault(x => x.Id == userId) }, JsonRequestBehavior.AllowGet);

        [Route("api/users/modify")]
        public JsonResult AddEditUser()
        {
            try
            {
                var formData = Request.Form;
                string userData = formData["userData"];
                var user = JsonConvert.DeserializeAnonymousType(userData, new Users());
                if (user == null)
                {
                    return Json(new { isSuccess = false, message = "Data not found" }, JsonRequestBehavior.AllowGet);
                }
                if (user.Id == 0)
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    return Json(new { isSuccess = true, message = "User added successfully" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var isUserExist = _context.Users.FirstOrDefault(x => x.Id == user.Id);
                    if (isUserExist == null)
                    {
                        return Json(new { isSuccess = false, message = "User not exist" }, JsonRequestBehavior.AllowGet);
                    }
                    isUserExist.Name = user.Name;
                    isUserExist.Email = user.Email;
                    isUserExist.Phone = user.Phone;
                    _context.SaveChanges();
                    return Json(new { isSuccess = true, message = "User updated successfully" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { isSuccess = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("api/users/delete")]
        public JsonResult DeleteUserById(int userId)
        {
            var isUserExist = _context.Users.FirstOrDefault(x => x.Id == userId);
            if (isUserExist == null)
            {
                return Json(new { isSuccess = false, message = "User not exist" }, JsonRequestBehavior.AllowGet);
            }
            _context.Users.Remove(isUserExist);
            _context.SaveChanges();
            return Json(new { isSuccess = true, message = "User deleted successfully" }, JsonRequestBehavior.AllowGet);
        }
    }
}