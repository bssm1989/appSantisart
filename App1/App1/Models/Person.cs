using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace App1.Models
{
    class Person
    {
        [PrimaryKey, AutoIncrement]
        public int PersonID { get; set; }
        public string EmpName { get; set; }
    }
}
