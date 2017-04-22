using Ninject;
using Ninject.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    public class UnitOfWork : IUnitOfWork
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
            using (var kernel = new StandardKernel())
            {
                kernel.Load(Assembly.GetExecutingAssembly());
                var result = kernel.Get<T>(new ConstructorArgument("context", context));

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        public void Commit()
        {
            context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Rollback()
        {
            context
                .ChangeTracker
                .Entries()
                .ToList()
                .ForEach(x => x.Reload());
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
