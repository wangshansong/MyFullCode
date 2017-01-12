using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using System.Runtime.Remoting.Messaging;

namespace DALMSSQL
{
    public  class DBSessionFactory:IDBSessionFactory
    {
        /// <summary>
        /// 此方法的作用： 提高效率，在线程中 共用一个 DBSession 对象！
        /// </summary>
        /// <returns></returns>
        public IDBSession GetDBSession()
        {
            //从当前线程中获取 DBContext 数据仓储 对象
            IDBSession dbSession = CallContext.GetData(typeof(DBSessionFactory).Name) as DBSession;
            if (dbSession == null)
            {
                dbSession = new DBSession();
                //将创建的 DbContext对象存到 当前线程中
                CallContext.SetData(typeof(DBSessionFactory).Name, dbSession);
            }
            return dbSession;
        }
    }
}
