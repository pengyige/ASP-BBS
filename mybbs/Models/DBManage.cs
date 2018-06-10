using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace mybbs.Models
{
    public class DBManage
    {
        public const string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\ssl\Desktop\bbs\bbs.accdb";


        //得到连接对象
        public static OleDbConnection getConn(){
            return new OleDbConnection(connString);
        }

        //关闭连接对象
        public static void closeConn(OleDbConnection conn){
            if(conn != null){
                conn.Close();
            }
        }

        //封装动态数据
        public static List<OleDbParameter> getListParameter(String[] key,Object[] value){
           List<OleDbParameter> paras = new List<OleDbParameter>();
            for(int i = 0 ; i < key.Length; i ++){
                 paras.Add(new OleDbParameter() { ParameterName = key[i], Value = value[i] });
            }
            return paras;
        }
    }
}