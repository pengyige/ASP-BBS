using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mybbs.Models
{
    public class Reply
    {
        private String replyId;
        public String ReplyId {
            get { return replyId; }
            set { replyId = value; }
        }

        private int postId;
        public int PostId {
            get { return postId; }
            set { postId = value; }
        }

        private String userId;
        public String UserId {
            get { return userId; }
            set { userId = value; }
        }

        private String replyMessage;
        public String ReplyMessage {
            get { return replyMessage; }
            set { replyMessage = value; }
        }

        private DateTime replyTime;
        public DateTime ReplyTime {
            get { return replyTime; }
            set { replyTime = value; }
        }

        private User user;
        public User User
        {
            get { return user; }
            set { user = value; }
        }

        public Reply(String replyId, String postId, String userId, String replyMessage, DateTime replyTime) {
            this.ReplyId = replyId;
            this.PostId = Convert.ToInt32(postId);
            this.UserId = userId;
            this.ReplyMessage = replyMessage;
            this.ReplyTime = replyTime;
        }

        public Reply()
        { }
    }
}