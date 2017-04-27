using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    public class GenericDataRepository<T> : IGenericDataRepository<T> where T : class
    {
        private DbContext context;
        private bool disposed = false;

        public GenericDataRepository(RelationsContext ctx)
        {
            context = ctx;
        }

        public GenericDataRepository()
        {
            if (context == null)
            {
                context = new RelationsContext();
            }
        }

        public virtual IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list = null;

            //using (var context = new RelationsContext())
            //{
                IQueryable<T> dbQuery = ApplyEagerLoading(navigationProperties, context);

                list = dbQuery
                    .AsNoTracking()
                    .ToList<T>();
            //}

            return list;
        }

        public virtual IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            List<T> list = null;

            //using (var context = new RelationsContext())
            //{
                context.Database.Log = message => Trace.Write(message);

                IQueryable<T> dbQuery = ApplyEagerLoading(navigationProperties, context);

                list = dbQuery
                    .AsNoTracking()
                    .Where(where)
                    .ToList<T>();
            //}

            return list;
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

            //using (var context = new RelationsContext())
            //{
                IQueryable<T> dbQuery = ApplyEagerLoading(navigationProperties, context);

                item = dbQuery
                    .AsNoTracking()
                    .FirstOrDefault(where);
            //}

            return item;
        }

        //public virtual async Task Add(params T[] items)
        public virtual void Add(params T[] items)
        {
            //using (var context = new RelationsContext())
            //{
                context.Database.Log = message => Trace.Write(message);

                foreach (T item in items)
                {
                    context.Entry(item).State = EntityState.Added;
                }

                //await context.SaveChangesAsync();
            //}
        }

        //public virtual async Task Update(params T[] items)
        public virtual void Update(params T[] items)
        {
            //using (var context = new RelationsContext())
            //{
                context.Database.Log = message => Trace.Write(message);

                foreach (var item in items)
                {
                    context.Entry(item).State = EntityState.Modified;
                }

                //await context.SaveChangesAsync();
            //}
        }

        //public virtual async Task Remove(params T[] items)
        public virtual void Remove(params T[] items)
        {
            //using (var context = new RelationsContext())
            //{
                foreach (var item in items)
                {
                    context.Entry(item).State = EntityState.Deleted;
                }

                //await context.SaveChangesAsync();
            //}
        }

        public virtual void UpdateRelated(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            var objCtx = ((IObjectContextAdapter)context).ObjectContext;
            ObjectSet<T> objSet = objCtx.CreateObjectSet<T>();
            var a = objSet.EntitySet.ElementType;

            var items = GetList(where, navigationProperties);

            foreach (var item in items)
            {
                foreach (var navProp in navigationProperties)
                {
                    var b = navProp.Body;

                    //var coll = context.Entry(item).Collection(navProp)
                }
            }
        }

        private static IQueryable<T> ApplyEagerLoading(Expression<Func<T, object>>[] navigationProperties, DbContext context)
        {
            IQueryable<T> dbQuery = context.Set<T>();

            foreach (var navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.Include<T, object>(navigationProperty);
            }

            return dbQuery;
        }
    }
}
