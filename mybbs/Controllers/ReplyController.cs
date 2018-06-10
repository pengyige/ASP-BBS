using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mybbs.Models;

namespace mybbs.Controllers
{
    public class ReplyController : Controller
    {
        ReplyDao replyDao = new ReplyDao();
        PostDao postDao = new PostDao();
        //
        // Post: /Reply/AddReply return: 1成功;0用户为登入;-1失败
        [HttpPost]
        public JsonResult AddReply(string postId,string replyMessage)
        {
            Message message= new Message("result","1"); 

            //1.检查用户是否登入
            User user = (User)Session["user"];
            if(user == null){
                message.value="0";
                return Json(message);
            }
            
            //封装数据
            Reply reply = new Reply(Guid.NewGuid().ToString(),postId,user.UserId,replyMessage,DateTime.Now);
            try
            {
                replyDao.add(reply);
                int a = Convert.ToInt16(postId);
                postDao.modifyReplyNum(a);
                return Json(message);
            }
            catch (Exception e)
            {
                message.value = "-1";
                return Json(message);
            }
          
            
          

            
        }

        public void TestReply() {
            replyDao.getAllReplyCount(15);
            replyDao.getReplysByPostId(15,2,2);
        }

    }
}
