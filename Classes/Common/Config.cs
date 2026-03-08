using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace pr_26_Toshmatov.Classes.Common
{
    public class Config
    {
        public static string ConnectionConfig = "server=127.0.0.1;uid=root;pwd=;database=pcClub;";
       public static Action<MySqlDbContextOptionsBuilder> Version;
    }
}