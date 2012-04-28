using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace BizCollege.DataAccessLayer.Repository
{
    internal class NHibernateSessionFactory
    {
        private static Configuration m_nhibernateConfig;
        private static ISessionFactory m_sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                // An MHibernate application should only have a single
                // global SessionFactory, since it's expensive to create
                if (m_sessionFactory == null)
                {
                    m_nhibernateConfig = new Configuration();
                    m_nhibernateConfig.Configure();
                    m_nhibernateConfig.AddAssembly(typeof(NHibernateSessionFactory).Assembly);

                    m_sessionFactory = m_nhibernateConfig.BuildSessionFactory();
                }
                return m_sessionFactory;
            }
        }

        /// <summary>
        /// Creates a database connection and opens a session using
        /// the global Nhibernate session factory configuration
        /// </summary>
        /// <returns></returns>
        public static ISession OpenSession()
        {
            return NHibernateSessionFactory.SessionFactory.OpenSession();
        }
    }
}
