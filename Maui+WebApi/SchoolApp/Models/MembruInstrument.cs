using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace SchoolApp.Models
{
    public class MembruInstrument
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(Member))]
        public int MembruID { get; set; }

        [ForeignKey(typeof(Instrument))]
        public int InstrumentID { get; set; }
    }
}
