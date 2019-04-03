using System;
using SQLite;

namespace MyImdbLibrary
{
    [Table("CountryEnum")]
    public class CountryEnum
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Code_Bis { get; set; }
        public string Flag { get; set; }

        public CountryEnum() { }
        public CountryEnum(string name, string code, string code_bis, string flag)
        {
            Name = name;
            Code = code;
            Code_Bis = code_bis;
            Flag = flag;
        }
    }
}
