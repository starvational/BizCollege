using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace BizCollege.DataAccessLayer.Tests
{
    [TestFixture]
    public class GenerateSchema_Fixture
    {
        [Test]
        public void Can_generate_schema()
        {
            var nhibernateConfig = new Configuration();
            nhibernateConfig.Configure();
            nhibernateConfig.AddAssembly(typeof(BizCollege.DataAccessLayer.Domain.Enrollment).Assembly);

            new SchemaUpdate(nhibernateConfig).Execute(false, true);
            new SchemaExport(nhibernateConfig).Execute(false, true, false);
        }
    }
}
