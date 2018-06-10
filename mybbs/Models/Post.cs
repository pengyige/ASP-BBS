using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mybbs.Models
{
    public class Post
    {

        private int postId ;
        public int PostId
        {
            get { return postId; }
            set { postId = value; }
        }


        private String userId = "";
        public String UserId
        {
            get { return userId; }
            set { userId = value; }
        }


        private String title = "";
        public String Title
        {
            get { return title; }
            set { title = value; }
        }

        private String content = "" ;
        public String Content
        {
            get { return content; }
            set { content = value; }
        }


        private DateTime postTime;
        public DateTime PostTime
        {
            get { return postTime; }
            set { postTime = value; }
        }


        private String userIp = "";
        public String UserIp
        {
            get { return userIp; }
            set { userIp = value; }
        }


        private int replyNum;
        public int ReplyNum
        {
            get { return replyNum; }
            set { replyNum = value; }
        }

        //维护用户关系
        private User user;
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        //维护回复表关系
        private List<Reply> replys;
        public List<Reply> Replys
        {
            get { return replys; }
            set { replys = value; }
        }

        private int browseNum;
        public int BrowseNum
        {
            get { return browseNum; }
            set { browseNum = value; }
        }
        public Post( String userId, String title, String content, DateTime postTime, String userIp, int replyNum, int browseNum) {
           
            this.userId = userId;
            this.title = title;
            this.content = content;
            this.postTime = postTime;
            this.userIp = userIp;
            this.replyNum = replyNum;
            this.browseNum = browseNum;
        }

        public Post() { 
        }
    }
}