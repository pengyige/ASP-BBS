using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace mybbs.Models
{
    public class PostDao
    {
        //发表帖子
        public void add(Post post) {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            try
            {
                conn = DBManage.getConn();
                conn.Open();
                comm = conn.CreateCommand();
                comm.CommandText = "insert into post(userId,title,content,postTime,userIp,replyNum,browseNum) values (@userId,@title,@content,@postTime,@userIp,@replyNum,@browseNum)";

                //动态设置参数
                List<OleDbParameter> paras = new List<OleDbParameter>();
                //paras.Add(new OleDbParameter() { ParameterName = "@postId", Value = post.PostId });
                paras.Add(new OleDbParameter() { ParameterName = "@userId", Value = post.UserId });
                paras.Add(new OleDbParameter() { ParameterName = "@title", Value = post.Title });
                paras.Add(new OleDbParameter() { ParameterName = "@content", Value = post.Content });
                paras.Add(new OleDbParameter() { ParameterName = "@postTime", Value = post.PostTime.ToString() });
                paras.Add(new OleDbParameter() { ParameterName = "@userIp", Value = post.UserIp });
                paras.Add(new OleDbParameter() { ParameterName = "@replyNum", Value = post.ReplyNum });
                paras.Add(new OleDbParameter() { ParameterName = "@browseNum", Value = post.BrowseNum });
                comm.Parameters.AddRange(paras.ToArray<OleDbParameter>());

                //3.执行命令

                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw new Exception();
            }
            finally {
                if (conn != null) {
                    DBManage.closeConn(conn);
                }
            }

        }


        //查询所有帖子
        public List<Post> findALLPost() {
            List<Post> listPost = new List<Post>();
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;
            UserDao userDao = new UserDao();
            try
            {
                conn = DBManage.getConn();
                conn.Open();
                comm = conn.CreateCommand();
                comm.CommandText = "select * from post order by postTime desc";

                reader = comm.ExecuteReader();
                while (reader.Read()) {
                Post post = new Post();
                post.PostId = reader.GetInt32(0);
                post.UserId = reader.GetString(1);
                post.Title = reader.GetString(2);
                post.Content = reader.GetString(3);
                post.PostTime = (DateTime)reader.GetValue(4);
                post.UserIp = reader.GetString(5);
                post.ReplyNum = reader.GetInt32(6);
                post.BrowseNum = reader.GetInt32(7);
                
                //关联查询出user
                User user = userDao.findUserById(post.UserId);
                post.User = user;

                listPost.Add(post);

                }

                return listPost;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
            finally { 
                if (conn != null) {
                    DBManage.closeConn(conn);
                }
            }

         
        }

        //查询用户帖子
        public List<Post> findPostsByUserId(String userId) {
            List<Post> listPost = new List<Post>();
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;
            UserDao userDao = new UserDao();
            try
            {
                conn = DBManage.getConn();
                conn.Open();
                comm = conn.CreateCommand();
                comm.CommandText = "select * from post where userId = '" + userId + "'";

                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    Post post = new Post();
                    post.PostId = reader.GetInt32(0);
                    post.UserId = reader.GetString(1);
                    post.Title = reader.GetString(2);
                    post.Content = reader.GetString(3);
                    post.PostTime = (DateTime)reader.GetValue(4);
                    post.UserIp = reader.GetString(5);
                    post.ReplyNum = reader.GetInt32(6);
                    post.BrowseNum = reader.GetInt32(7);

                    //关联查询出user
                    User user = userDao.findUserById(post.UserId);
                    post.User = user;

                    listPost.Add(post);

                }

                return listPost;
            }catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    DBManage.closeConn(conn);
                }
            }


           
        }

        //通过ID删除帖子
        public bool deletePost(String postId,String userId) {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            conn = DBManage.getConn();
            conn.Open();
            comm = conn.CreateCommand();
            comm.CommandText = "delete from [post] where postId = @postId and userId = @userId";

            List<OleDbParameter> paras = DBManage.getListParameter(new String[] { "@postId", "@userId" },
                new Object[]{Convert.ToInt32(postId),userId});
            comm.Parameters.AddRange(paras.ToArray<OleDbParameter>());
            int flag ;
            try
            {
              flag  = comm.ExecuteNonQuery();
              if (flag == 1)
              {
                  return true;
              }
              else
              {
                  return false;
              }
            }
            catch (Exception e) {
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    DBManage.closeConn(conn);
                }
            }  
       
            
          
        }


        //根据帖子id查询
        public Post findPostByPostId(int postId)
        {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;
            UserDao userDao = new UserDao();
            Post post = null;
            try
            {
                conn = DBManage.getConn();
                conn.Open();
                comm = conn.CreateCommand();
                comm.CommandText = "select * from post where postId = "+ postId ;

                reader = comm.ExecuteReader();
                if(reader.Read())
                {
                    post = new Post();
                    post.PostId = reader.GetInt32(0);
                    post.UserId = reader.GetString(1);
                    post.Title = reader.GetString(2);
                    post.Content = reader.GetString(3);
                    post.PostTime = (DateTime)reader.GetValue(4);
                    post.UserIp = reader.GetString(5);
                    post.ReplyNum = reader.GetInt32(6);
                    post.BrowseNum = reader.GetInt32(7);
                
                    //关联查询出user
                    User user = userDao.findUserById(post.UserId);
                    post.User = user;
                }

                return post;
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    DBManage.closeConn(conn);
                }
            }
        }

        //修改浏览次数
        public void modifyNum(int postId)
        {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            try
            {
                conn = DBManage.getConn();
                //1.打开数据库
                conn.Open();
                //2.创建command对象
                comm = conn.CreateCommand();
                comm.CommandText = "update [post] set browseNum = browseNum + 1 where postId = @postId ";
                List<OleDbParameter> paras = DBManage.getListParameter(new String[] { "@postId" }, new Object[] { postId });
                comm.Parameters.AddRange(paras.ToArray<OleDbParameter>());

                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw new Exception();
            }
            finally
            {
                if (conn != null)
                {
                    DBManage.closeConn(conn);
                }
            }
        }

        //通过帖子Id修改回复数量
        internal void modifyReplyNum(int postId)
        {
            OleDbConnection c = null;
            OleDbCommand a = null;
            c = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ssl\Desktop\bbs\bbs.accdb");
            c.Open();
            a = c.CreateCommand();
            a.CommandText = "update post set replyNum = replyNum+1 where postId = " + postId ;
            a.ExecuteNonQuery();
        
        }
    }
}
