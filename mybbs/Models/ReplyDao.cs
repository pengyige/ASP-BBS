using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using mybbs.Models;
using System.Data.OleDb;
namespace mybbs.Models
{
    public class ReplyDao
    {
        public void add(Reply reply)
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
                comm.CommandText = "insert into reply(replyId,postId,userId,replyMessage,replyTime) values(@replyId,@postId,@userId,@replyMessage,@replyTime)";
                List<OleDbParameter> paras = DBManage.getListParameter(
                    new String[] { "@replyId", "@postId", "@userId", "@replyMessage", "@replyTime" },
                    new Object[] { reply.ReplyId, reply.PostId, reply.UserId, reply.ReplyMessage, reply.ReplyTime.ToString() });
                comm.Parameters.AddRange(paras.ToArray<OleDbParameter>());

                //3.执行命令

                comm.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw new Exception();
            }
            finally
            {
                //4.关闭连接
                if (conn != null)
                {
                    DBManage.closeConn(conn);
                }
            }

        }



        //通过postId获取所有回复数量
        public int getAllReplyCount(int postId)
        {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;
            int count = 0;
            try
            {
                conn = DBManage.getConn();
                //1.打开数据库
                conn.Open();
                //2.创建command对象
                comm = conn.CreateCommand();
                comm.CommandText = "select count(*) from reply where postId = "+postId;

                //3.执行命令
                reader = comm.ExecuteReader();
                if (reader.Read()) {
                    count = reader.GetInt32(0);
                }
                return count;

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw new Exception();
            }
            finally
            {
                //4.关闭连接
                if (conn != null)
                {
                    DBManage.closeConn(conn);
                }
            }
       }

        
       //通过postId获取回复数据，限制数据条数
        public List<Reply> getReplysByPostId(int postId,int currentPage,int everyPageSize)
        {
            OleDbConnection conn = null;
            OleDbCommand comm = null;
            OleDbDataReader reader = null;
            List<Reply> listReplys = new List<Reply>();
            UserDao userDao = new UserDao();
            try
            {
                conn = DBManage.getConn();
                //1.打开数据库
                conn.Open();
                //2.创建command对象
                comm = conn.CreateCommand();
                //comm.CommandText ="select top @everyPageSize * from "
                //    +"(select top @currentPage*@everyPageSize * from reply where postId = @postId order by replyTime desc)"+" order by replyTime asc" ;
                int num = currentPage * everyPageSize;
                //先获取currentPage前所有数据并降序，在获取每页大小并升序。结果就是第currentPage数据
                comm.CommandText = "select top " + everyPageSize + " * from ( select top " + num + " * from reply where postId = "+postId+" order by replyTime desc ) order by replyTime asc";

                //List<OleDbParameter> paras = DBManage.getListParameter(
                // new String[] { "@postId"},
                // new Object[] { 15});
                //comm.Parameters.AddRange(paras.ToArray<OleDbParameter>());

                //3.执行命令
                reader = comm.ExecuteReader();
                while (reader.Read()) {
                    Reply reply = new Reply();
                    reply.ReplyId = reader.GetString(0);
                    reply.PostId = reader.GetInt32(1);
                    reply.UserId = reader.GetString(2);
                    reply.ReplyMessage = reader.GetString(3);
                    reply.ReplyTime = reader.GetDateTime(4);

                    //通过userId获取对于的回复人信息
                    reply.User = userDao.findUserById(reply.UserId);

                    listReplys.Add(reply);
                }

            }
            catch (Exception e)
            {
                Console.Write(e.Message);
                throw new Exception();
            }
            finally
            {
                //4.关闭连接
                if (conn != null)
                {
                    DBManage.closeConn(conn);
                }
            }

            return listReplys;
            
        }
    }
}