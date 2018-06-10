using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mybbs.Models
{
    public class UserService
    {
        private UserDao userDao = new UserDao();
        private PostDao postDao = new PostDao();
        //注册用户
        public void addUser(User user) {
            userDao.add(user);
        }


        //用户登入
        public User userLogin(User user) {
            return userDao.findUser(user);
        }

        //用户是否存在
        public bool userExists(String username) {
            return userDao.userExists(username);
        }

        //修改用户图片
        public void modifyUserPic(User user)
        {
            userDao.updatePic(user);
        }

        //邮件是否已注册
        public bool emailExists(String email) {
            return userDao.emailExists(email);
        }



        //发表帖子
        public void addPost(Post post) {
            postDao.add(post);
        }

        //获取用户帖子
        public List<Post> getPosts(string userId)
        {
            return postDao.findPostsByUserId(userId);

        }


        //查询所有帖子
        public List<Post> queryAllPost() {
            return postDao.findALLPost();
        }

        //删除帖子
        public bool deletePost(String postId,String userId) {
            return postDao.deletePost(postId, userId);
        }

        
    }
}