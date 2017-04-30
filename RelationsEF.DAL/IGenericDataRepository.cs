using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    public interface IGenericDataRepository<T> where T : class
    {
        IList<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);
        IList<T> GetList(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void Add(params T[] items);
        void Update(params T[] items);
        void Remove(params T[] items);
        void UpdateRelated(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);
        void UpdateRelated(Func<T, bool> where, IEnumerable<object> updatedSet, string relatedPropertyName, string relatedPropertyKeyName);
    }
}
