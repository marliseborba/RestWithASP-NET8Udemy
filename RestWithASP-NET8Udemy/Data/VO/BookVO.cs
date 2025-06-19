using RestWithASP_NET8Udemy.Model.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASP_NET8Udemy.Data.VO
{
    public class BookVO
    {
        public long Id { get; set; }

        public string Author { get; set; }

        public DateTime LaunchDate { get; set; }

        public double Price { get; set; }

        public string Title { get; set; }


    }
}
