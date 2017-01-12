using Spring.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DI
{
    public static class SpringHelper
    {

        /// <summary>
        /// Spring容器上下文
        /// </summary>
        private static IApplicationContext SpringContext
        {
            get {
                return Spring.Context.Support.ContextRegistry.GetContext();
            }
        }
        /// <summary>
        /// 获取配置文件 配置的 对象 
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="objName">配置文件中对象的Id名称</param>
        /// <returns></returns>
        public static T GetObject<T>(string objName) where T : class
        {
            T t = (T)SpringContext.GetObject(objName);
            return t;
        }
    }
}
