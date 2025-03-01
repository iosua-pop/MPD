using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SchoolApp.Models
{
    public class Member
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Nume { get; set; }
        public string Prenume { get; set; }
        [Unique]
        public string Email { get; set; }

        public string PasswordHash { get; set; }
        public string Role { get; set; } = "Student";


        public string NumarTelefon { get; set; }
        public string Adresa { get; set; }
        public DateTime CreatedDate { get; set; }

        [OneToMany]
        public List<Programare> Programari { get; set; }

        [OneToMany]
        public List<Feedback> Feedbackuri { get; set; }

        public string FullName => $"{Prenume} {Nume}";
    }
}
