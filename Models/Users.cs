using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace pr_26_Toshmatov.Models
{
    internal class Users
    {
        public int Id { get; set; }

        public string FIO { get; set; }

        public DateTime RenStart { get; set; }

        public int Duration { get; set; }

        public string Idclub { get; set; }

    }
}
