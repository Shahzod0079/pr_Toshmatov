using System;

namespace pr_26_Toshmatov.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public DateTime RentStart { get; set; }
        public int Duration { get; set; }
        public int IdClub { get; set; }
    }
}