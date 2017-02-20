using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.EasyUIModel
{
    public  class DataGridModel
    {
        /// <summary>
        /// 数据总条数
        /// </summary>
        public int total { set; get; }
        /// <summary>
        /// Json格式的数据内容
        /// </summary>
        public object rows { set; get; }
        /// <summary>
        /// 脚
        /// </summary>
        public object footer { set; get; }
    }
}
