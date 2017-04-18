using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    public class UnitOfWork : IDisposable
    {
        private readonly RelationsContext context;

        public UnitOfWork()
            : this(new RelationsContext())
        { }

        public UnitOfWork(RelationsContext ctx)
        {
            context = ctx;
        }

        public T GetRepository<T>() where T : class 
        {
            

            return null;
        }


        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
