using System;
using SQLite;

namespace MyImdbLibrary
{
    [Table("MovieTypeEnum")]
    public class MovieTypeEnum
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        [MaxLength(8)]
        public string Name { get; set; }

        public MovieTypeEnum() { }
    }
}