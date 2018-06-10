using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mybbs.Models;

namespace mybbs.Controllers
{
    public class LoginController : Controller
    {
        private UserService userService = new UserService();

        // GET: /Login/Login

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }


        //POST: /Login/
        //自动封装参数到参数列表
        [AcceptVerbs(HttpVerbs.Post)]
        public void  Index(string username,string password)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;

            user = userService.userLogin(user);
            if (user == null) {
                Response.Write("<script>alert('用户名或密码不正确!')</script>");
            }else{
                Response.Write("<script>alert('OKOKOK!')</script>");
                Session["user"] = user;
                Response.Redirect("/Home/Index");
            }
        }


        // POST: /Login/RegisterUser
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult RegisterUser(string username,string password,string email,string telphone,string qq)
        { 
            //1.封装数据

            User user = new User(System.Guid.NewGuid().ToString(),username,password,email,telphone,qq);

            //设置默认头像
            user.Picture = "/UploadFiles/commonPic/default.jpg";
            try
            {
                //2.业务逻辑处理
                userService.addUser(user);
                Session["user"] = user;
                return Redirect("/Shared/Success");
            }
            catch (Exception e) {
                return Redirect("/Shared/Error");
            }

            
        }


        //POST: /Login/LogoutUser  return:-1 未登入；1成功
        public JsonResult LogoutUser() 
        {
            User user = (User)Session["user"];
            Message message = new Message("result","1");
            if (user != null)
            {
                Session.Remove("user");

            }
            else {

                message.value = "-1";
            }

            return Json(message);
        }
    }
}
