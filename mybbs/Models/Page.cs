using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mybbs.Models
{
    public class Page
    {
        //总记录数
        private int allCount;
        public int AllCount
        {
            get { return allCount; }
            set { allCount = value; }
        }

        //每页显示条数 默认为10条数据
        private int everyPageSize = 4;
        public int EveryPageSize
        {
            get { return everyPageSize; }
            set { everyPageSize = value; }
        }

        //根据总记录数和每页显示跳绳算出总页数
        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        //当前页
        private int currentPage;
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }

        //分页页面数据
        private List<Reply> postReplys;
        public List<Reply> PostReplys
        {
            get { return postReplys; }
            set { postReplys = value; }
        }

        //是否有下一页
        private bool isHasNext;
        public bool IsHasNext
        {
            get { return isHasNext; }
            set { isHasNext = value; }
        }

        //是否有下一页
        private bool isHasPre;
        public bool IsHasPre
        {
            get { return isHasPre; }
            set { isHasPre = value; }
        }

        
    }
}