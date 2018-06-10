using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mybbs.Models;
namespace mybbs.Controllers
{
    public class UploadController : Controller
    {
        UserService userService = new UserService();
        //
        // GET: /Upload/

        public ActionResult Index()
        {
            return View();
        }


        //POST: /Upload/UserPic
        [HttpPost]
        public JsonResult UserPic(HttpPostedFileBase file) 
        {
            Message message = new Message("result","1");

            try
            {
                string fileName = System.IO.Path.GetFileName(file.FileName);
                string UUIDFileName = Guid.NewGuid().ToString() + fileName;
                string uploadPath = Server.MapPath("/UploadFiles/UserPic/" + UUIDFileName);
                file.SaveAs(uploadPath);  //上传图片到指定文件夹

                //保存到数据库
                User user =  (User)Session["user"];
                if (user == null) {
                    message.value = "-1";
                    return Json(message);
                }
                user.Picture = "/UploadFiles/UserPic/" + UUIDFileName;
                userService.modifyUserPic(user);
                return Json(message);
            }
            catch (Exception e)
            {
                message.value = "0";
                return Json(message);
            }
          
        }


        //发帖中的图片保存到服务器中
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            string fileName = System.IO.Path.GetFileName(upload.FileName);

            string timeFileName = Guid.NewGuid().ToString() + fileName;
            string uploadPath = Server.MapPath("/UploadFiles/" + timeFileName);


            upload.SaveAs(uploadPath);  //上传图片到指定文件夹



            //string clientFilePath = System.IO.Path.GetFullPath(upload.FileName);

            var CKEditorFuncNum = System.Web.HttpContext.Current.Request["CKEditorFuncNum"];
            var url = "/UploadFiles/" + timeFileName;
            return Content("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
        }


    }

  
   
}
