using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mybbs.Models;

namespace mybbs.Controllers
{
    public class PostController : Controller
    {
        private UserService userService = new UserService();
     

        // POST : /Post/sendPost   return:0:失败 1:成功 -1 用户未登入 
        
         [ValidateInput(false)]
          [HttpPost]
        public JsonResult SendPost(string title,string content) {
            //1.判断用户是否登入
            User user = (User)Session["user"];
            Message message = new Message("result","1");
            if (user == null) {
                message.value = "-1";
                return Json(message);
            }

            //2.封装发帖信息
            Post post = new Post(user.UserId, title,content, DateTime.Now, Request.ServerVariables.Get("Remote_Addr").ToString(), 0, 0);

           
            //3.业务逻辑处理
            try
            {
                userService.addPost(post);
            }
            catch (Exception e) {
           
                return Json(message);
            }
            return Json(message);
           
        }


        // // POST : /Post/GetAllPost
        // [AcceptVerbs(HttpVerbs.Post)]
        // public void GetALLPost() {
        //     List<Post> listPost = userService.queryAllPost();

        // }


        ////POST : /Post/GetPosts
        // [AcceptVerbs(HttpVerbs.Post)]
        // public JsonResult GetPosts() {
        //     //1.判断用户是否登入
        //     User user = (User)Session["user"];
        //     if (user == null)
        //     {
                
        //         return Json("0");
        //     }

        //     List<Post> listPost = userService.getPosts(user.UserId);
            
        //     return Json(listPost);
        // }

         //POST : /Post/DeletePost  return:1成功 0失败
         [AcceptVerbs(HttpVerbs.Post)]
         public JsonResult DeletePost(string postId) {
               Message message = new Message("result","1");
             //1.检查用户是否登入
             User user = (User)Session["user"];
             if (user == null) {
                 message.value = "-1";
                 return Json(message);
             }

           
             if (!userService.deletePost(postId,user.UserId)) {
                 message.value = "0";
             }
            
            
              return Json(message);
         }

    }

    
}
