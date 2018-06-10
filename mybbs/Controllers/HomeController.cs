using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mybbs.Models;

namespace mybbs.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {

        private PostDao postDao = new PostDao();
        private ReplyDao replyDao = new ReplyDao();
        //跳转到首页
        public ActionResult Index()
        {

    
            List<Post> listPost = postDao.findALLPost();
            if (listPost == null)
            {
                return Redirect("/Shared/Error");
              
            }
            ViewData["listPost"] = listPost;
            return View();
                
           
        }

        //跳转到详细帖子页面
        public ActionResult Readpost(string postId,string currentPage)
        {
            

            int ipostId = Convert.ToInt32(postId);
            int icurrentPage =  Convert.ToInt32(currentPage);
            //封装page对象
            Page page = new Page();

            page.AllCount = replyDao.getAllReplyCount(ipostId);
            //总页数
            if (page.AllCount % page.EveryPageSize == 0)
            {
                page.PageSize = page.AllCount / page.EveryPageSize;
            }
            else {
                page.PageSize = page.AllCount / page.EveryPageSize + 1;
                
            }


            //第一次跳转
            if(currentPage == null || icurrentPage < 1){
                icurrentPage = 1;
                //浏览次数加1
                if(currentPage == null) {
                    postDao.modifyNum(ipostId);
                }

            }

            if (Convert.ToInt32(currentPage) > page.PageSize) {
                icurrentPage = page.PageSize;
            }
            //页面回复数据
            page.CurrentPage = icurrentPage;
            page.PostReplys = replyDao.getReplysByPostId(ipostId, page.CurrentPage, page.EveryPageSize);

               //若不能整除，会多取重复的数据
            if (page.AllCount > page.EveryPageSize)
            {
                if (page.CurrentPage == page.PageSize && page.AllCount % page.EveryPageSize != 0)
                {
                    int recount = page.AllCount % page.EveryPageSize;
                    int re = page.EveryPageSize - recount;
                    page.PostReplys.RemoveRange(recount, re);
                }
            }

            //帖子主人数据
            Post post = postDao.findPostByPostId(ipostId);     
            ViewData["post"] = post;
            ViewData["page"] = page;
            return View();
        }

        //跳转到个人中心
        public ActionResult Personal()
        {
            User user = (User)Session["user"];
            if (user == null) {
                return Redirect("/Shared/UserNOTLogin");
            }
            List<Post> userListPost = postDao.findPostsByUserId(user.UserId);
            ViewData["userListPost"] = userListPost;
            return View();
        }


        //跳转到注册页面
        public ActionResult Register()
        {
            return View();
        }

    
       
    }
}
