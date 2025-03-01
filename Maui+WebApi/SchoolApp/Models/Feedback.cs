using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SchoolApp.Models
{
    public class Feedback
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Programare))]
        public int ProgramareID { get; set; }

        [ForeignKey(typeof(Member))]
        public int MembruID { get; set; }

        public int Stars { get; set; }
        public string Description { get; set; }
    }
}
