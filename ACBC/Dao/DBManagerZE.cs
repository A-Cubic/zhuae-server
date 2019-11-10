using Com.ACBC.Framework.Database;
using System;

namespace ACBC.Dao
{
    public class DBManagerZE : IType
    {
        private DBType dbt;
        private string str = "";

        public DBManagerZE()
        {
            var url = System.Environment.GetEnvironmentVariable("ZEMysqlDBUrl");
            var uid = System.Environment.GetEnvironmentVariable("ZEMysqlDBUser");
            var port = System.Environment.GetEnvironmentVariable("ZEMysqlDBPort");
            var passd = System.Environment.GetEnvironmentVariable("ZEMysqlDBPassword");

            this.str = "Server=" + url
                     + ";Port=" + port
                     + ";Database=dolldb;Uid=" + uid
                     + ";Pwd=" + passd
                     + ";CharSet=utf8mb4; SslMode =none;";
           
            Console.Write(this.str);
            this.dbt = DBType.Mysql;
        }

        public DBManagerZE(DBType d, string s)
        {
            this.dbt = d;
            this.str = s;
        }

        public DBType getDBType()
        {
            return dbt;
        }

        public string getConnString()
        {
            return str;
        }

        public void setConnString(string s)
        {
            this.str = s;
        }
    }
}
