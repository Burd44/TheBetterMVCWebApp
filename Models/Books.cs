using System.ComponentModel.DataAnnotations;

namespace MVC.Models
{
    public class Books
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Author { get; set; }  

        public string Description { get; set; }

        public int Release_Year { get; set; }

        public DateTime Registration_Year { get; set; } = DateTime.Now;
    }
}
