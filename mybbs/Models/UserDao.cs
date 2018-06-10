using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace mybbs.Models
{
    public class UserDao
    {


        //注册用户
        public  void add(User user) {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            try
            {
                conn = DBManage.getConn();
                //1.打开数据库
                conn.Open();
                //2.创建command对象
                comm = conn.CreateCommand();
                comm.CommandText = "insert into [user] values(@userId,@username,@password,@email,@phone,@QQ,@picture)";

                //动态设置参数
                //List<OleDbParameter> paras = new List<OleDbParameter>();
                //paras.Add(new OleDbParameter(){ParameterName   = "@userId",Value=user.UserId});
                //paras.Add(new OleDbParameter() { ParameterName = "@username", Value = user.Username });
                //paras.Add(new OleDbParameter() { ParameterName = "@password", Value = user.Password });
                //paras.Add(new OleDbParameter() { ParameterName = "@email", Value = user.Email });
                //paras.Add(new OleDbParameter() { ParameterName = "@phone", Value = user.Phone });
                //paras.Add(new OleDbParameter() { ParameterName = "@QQ", Value = user.QQ });
                //paras.Add(new OleDbParameter() { ParameterName = "@picture", Value = user.Picture });
                List<OleDbParameter> paras = DBManage.getListParameter(
                    new String[] { "@userId", "@username", "@password", "@email",  "@phone", "@QQ", "@picture" },
                    new Object[] { user.UserId,user.Username, user.Password, user.Email, user.Phone,user.QQ,user.Picture });
                comm.Parameters.AddRange(paras.ToArray<OleDbParameter>());
                
                //3.执行命令
                
                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally {
                //4.关闭连接
                if (conn != null) {
                    DBManage.closeConn(conn);
                }
            }
           


        }
      
        //用户登入验证
        public User findUser(User user)
        {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;
            User findUser = null;
            conn = DBManage.getConn();
            //1.打开数据库
            conn.Open();
            //2.创建command对象
            comm = conn.CreateCommand();
            comm.CommandText = "select * from [user] where ( username = @username or email = @email ) and password = @password";

            //List<OleDbParameter> paras = new List<OleDbParameter>();
            //paras.Add(new OleDbParameter() { ParameterName = "@username", Value = user.Username });
            //paras.Add(new OleDbParameter() { ParameterName = "@email", Value = user.Username });
            //paras.Add(new OleDbParameter() { ParameterName = "@password", Value = user.Password });

             List<OleDbParameter> paras = DBManage.getListParameter(new String[]{"@username","@email","password"},new Object[]{user.Username,user.Username,user.Password});
            comm.Parameters.AddRange(paras.ToArray<OleDbParameter>());

            reader =  comm.ExecuteReader();
            while (reader.Read()) {
                findUser = new User();
                findUser.UserId = reader.GetString(0);
                findUser.Username = reader.GetString(1);
                findUser.Password = reader.GetString(2);
                findUser.Email = reader.GetString(3);
                findUser.Phone = reader.GetString(4);
                findUser.QQ = reader.GetString(5);
                findUser.Picture = reader.GetString(6);
            }
           
            return findUser;
        }


        //根据id号获取用户
        public User findUserById(String userId) { 
            User user = null;
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;

            conn = DBManage.getConn();
            //1.打开数据库
            conn.Open();
            //2.创建command对象
            comm = conn.CreateCommand();
            comm.CommandText = "select * from [user] where userId = '"+userId+"'";

            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                user = new User();
                user.UserId = reader.GetString(0);
                user.Username = reader.GetString(1);
                user.Password = reader.GetString(2);
                user.Email = reader.GetString(3);
                user.Phone = reader.GetString(4);
                user.QQ = reader.GetString(5);
                user.Picture = reader.GetString(6);
            }

            return user;
        }

        //用户名称是否存在
        public bool userExists(string username)
        {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;
            conn = DBManage.getConn();
            //1.打开数据库
            conn.Open();
            //2.创建command对象
            comm = conn.CreateCommand();
            comm.CommandText = "select * from [user] where username = "+username;
            reader = comm.ExecuteReader();
            if (reader.Read()) {
                return true;
            }

            return false;
        }

        //邮箱是否存在
        public bool emailExists(string email)
        {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;
            conn = DBManage.getConn();
            //1.打开数据库
            conn.Open();
            //2.创建command对象
            comm = conn.CreateCommand();
            comm.CommandText = "select * from [user] where email = " + email;
            reader = comm.ExecuteReader();
            if (reader.Read())
            {
                return true;
            }

            return false;
        }


        //修改用户图片
        public void updatePic(User user)
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
                comm.CommandText = "update [user] set picture = @picture where userId = @userId ";
                List<OleDbParameter> paras = DBManage.getListParameter(new String[] { "@picture", "@userId" }, new Object[] { user.Picture, user.UserId });
                comm.Parameters.AddRange(paras.ToArray<OleDbParameter>());

                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            finally {
                if (conn != null)
                {
                    DBManage.closeConn(conn);
                }
            }

           
        }
    }
}