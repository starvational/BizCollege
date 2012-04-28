using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace BizCollege.DataAccessLayer.Repository
{
    public sealed class BizCollegeRepository<T,K> : IRepository<T, K> where T : class
    {
        public T Get(K itemId)
        {
            using (ISession session = NHibernateSessionFactory.OpenSession())
            {
                T item = session.Get<T>(itemId);
                return item;
            }
        }

        public T AddOrUpdate(T item)
        {
            T detachedInstance = null;
            using (ISession session = NHibernateSessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    detachedInstance = (T)session.Merge(item);
                    transaction.Commit();
                }
            }
            return detachedInstance;
        }

        public void Remove(K itemId)
        {
            using (ISession session = NHibernateSessionFactory.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    T item = session.Get<T>(itemId);

                    if (item != null)
                    {
                        session.Delete(item);
                        transaction.Commit();
                    }
                }
            }
        }

        public ICollection<T> GetAllItems()
        {
            using (ISession session = NHibernateSessionFactory.OpenSession())
            {
                return session.CreateCriteria<T>().List<T>();
            }
        }
    }
}
