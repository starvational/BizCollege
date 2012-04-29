using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizCollege.DataAccessLayer.Helper
{
    public static class SqlServerHelper
    {
        /// <summary>
        ///  "The minimum and the maximum value of DateTime in .NET and in SQL-Server are not the same. 
        ///  .NET DateTime minimum value is 1/1/0001 00:00:00 and the maximum is 12/31/9999 23:59:59.999, 
        ///  while Sql minimum value is 1/1/1753 00:00:00.003 and the maximum is  12/31/9999 23:59:59.997 
        ///  If we will try to send to the SP a datetime that is less then the minimum of the Sql or 
        ///  DateTime.MaxValue, we will get an Exception, so we need to correct it before"
        ///  
        ///  reference:  http://mironabramson.com/blog/post/2007/09/Caution-When-passing-Null-or-DateTime-into-Store-Procedure.aspx
        /// </summary>
        /// <returns>The minimum DateTime struct value that SQL Server will allow (since .Net and SQL server minimum values differ)</returns>
        public static DateTime GetSqlServerMinimumDateTimeValue()
        {
            return System.Data.SqlTypes.SqlDateTime.MinValue.Value;
        }
    }
}
