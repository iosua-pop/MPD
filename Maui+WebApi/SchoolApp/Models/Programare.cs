using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SchoolApp.Models
{
    public class Programare
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Member))]
        public int TeacherID { get; set; }

        [ForeignKey(typeof(Member))]
        public int StudentID { get; set; }

        public DateTime OraProgramarii { get; set; }
        public string AdresaProgramarii { get; set; }

        [OneToMany]
        public List<Feedback> Feedbackuri { get; set; }

        [Ignore]
        public string DisplayName { get; set; }
    }
}
