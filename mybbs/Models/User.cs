using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mybbs.Models
{
    public class User
    {

        private String userId = "";
        public String UserId
        {
            get { return userId; }
            set { userId = value; }
        }


        private String username = "";
        public String Username
        {
            get { return username; }
            set { username = value; }
        }


        private String password = "";
        public String Password
        {
            get { return password; }
            set { password = value; }
        }


        private String email = "";
        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        private String phone = "";
        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }


        private String qq = "";
        public String QQ
        {
            get { return qq; }
            set { qq = value; }
        }



        private String picture = "";
        public String Picture
        {
            get { return picture; }
            set { picture = value; }
        }

        public User(String userId, String username, String password, String email, String phone, String qq)
        {
            this.userId = userId;
            this.username = username;
            this.password = password;
            this.email = email;
            this.phone = phone;
            this.qq = qq;


        }

        public User() { 
        }
    }
}