using System;
using System.Collections;
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

        public async Task UpdateRelated(Expression<Func<T, bool>> where, IEnumerable<object> updatedSet, string relatedPropertyName, string relatedPropertyKeyName)
        {
            if (updatedSet != null && updatedSet.Count() > 0)
            {
                context.Database.Log = message => Trace.Write(message);
                
                // Get the generic type of the set
                var type = updatedSet.First().GetType();

                var items = context.Set<T>()
                    .Include(relatedPropertyName)
                    //.AsNoTracking()
                    .Where(where.Compile())
                    .ToList();

                foreach (var item in items)
                {
                    var values = CreateList(type);

                    //var a = updatedSet
                    //        .Select(obj => obj.GetType());

                    //var b = a.Select(t => t.GetProperty(relatedPropertyName));

                    //var c = b.Select(pi => pi.GetValue())

                    //var aa = updatedSet
                    //        .Select(obj => (int)(obj
                    //        .GetType()
                    //        .GetProperty(relatedPropertyKeyName)
                    //        .GetValue(obj, null)));

                    //var bb = aa.Select(val => context.Set(type).Find(val));

                    var qry = updatedSet
                            .Select(obj => (int)(obj
                            .GetType()
                            .GetProperty(relatedPropertyKeyName)
                            .GetValue(obj, null)));

                    var relatedEntries = qry
                        .Select(val => context.Set(type).Find(val));
                        

                    foreach (var entry in relatedEntries)
                    {
                        //await context.Entry(entry).ReloadAsync();
                        values.Add(entry);
                    }

                    context.Entry(item).Collection(relatedPropertyName).CurrentValue = values;
                    context.Entry(item).State = EntityState.Modified;
                }
            }
        }

        private IList CreateList(Type type)
        {
            var genericList = typeof(List<>).MakeGenericType(type);
            return (IList)Activator.CreateInstance(genericList);
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
