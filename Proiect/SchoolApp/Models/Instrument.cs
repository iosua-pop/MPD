using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SchoolApp.Models
{
    public class Instrument
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string NumeInstrument { get; set; }

        [ManyToMany(typeof(MembruInstrument))]
        public List<Member> Membri { get; set; }
    }
}
