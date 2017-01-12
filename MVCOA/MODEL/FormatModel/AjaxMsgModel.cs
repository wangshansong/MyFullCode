using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.FormatModel
{
    /// <summary>
    /// 统一的Ajax格式类
    /// </summary>
    public class AjaxMsgModel
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 状态 如：ok,err
        /// </summary>
        public string Statu { get; set; }
        /// <summary>
        /// 要跳转的URL地址
        /// </summary>
        public string BackUrl { get; set; }
        /// <summary>
        /// 数据对象
        /// </summary>
        public object Data { get; set; }
    }
}
