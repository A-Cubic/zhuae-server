using ACBC.Buss;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ACBC.Dao
{
    public class SqlServerDB : DbContext
    {
        public DbSet<GMember> GMember { set; get; }
        public DbSet<GRechargeDetail> GRechargeDetail { set; get; }

        public SqlServerDB()
        {   //如果没有数据库，自动创建，有了什么都不干
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {   
            optionsBuilder.UseSqlServer("server=LAPTOP-F9D0E2DE\\SQLEXPRESS;Initial Catalog=GameDBZYT;Integrated Security=True");

            base.OnConfiguring(optionsBuilder);
        }

    }
    public class GMember
    {
        [Key]
        public string ME_ID { set; get; }

        public Int32 ME_Score { set; get; }
        public Int32 ME_Point { set; get; }
        public string ME_MobileNum { set; get; }
    }
    public class GRechargeDetail
    {
        [Key]
        public string RD_GUID { set; get; }
        public string RD_ME_ID { set; get; }
        public Int32 RD_TYPE { set; get; }
        public Int32 RD_Money { set; get; }
        public Int32 RD_Point { set; get; }
        public Int32 RD_GiveScore { set; get; }
        public DateTime RD_Time { set; get; }
    }
}
