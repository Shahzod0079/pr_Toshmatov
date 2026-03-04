using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;

namespace WpfApp1.Classes.Common
{
    public class Config
    {
        public static string ConnectionConfig = "server=10.0.201.4;uid=root;pwd=;database=pcClub;";
       public static Action<MySqlDbContextOptionsBuilder> Version;
    }
}