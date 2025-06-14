using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASP_NET8Udemy.Model
{
    [Table("Books")]
    public class Books
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("author")]
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime LaunchDate { get; set; }
        [Column("price")]
        public double Price { get; set; }
        [Column("title")]
        public string Title { get; set; }


    }
}
