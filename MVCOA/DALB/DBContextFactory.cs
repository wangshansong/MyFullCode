using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using System.Data.Entity;
using System.Runtime.Remoting.Messaging;
using MODEL;

namespace DALMSSQL
{
    public class DBContextFactory:IDBContextFactory
    {
        public DbContext GetDbContext()
        {
            //获取EF上下文对象，如果没有放到线程槽中
            DbContext dbContext = CallContext.GetData(typeof(DBContextFactory).Name) as DbContext;
            if (dbContext == null)
            {
                dbContext = new OuOAEntities();
                CallContext.SetData(typeof(DBContextFactory).Name,dbContext);
            }
            return dbContext;
        }
    }
}
