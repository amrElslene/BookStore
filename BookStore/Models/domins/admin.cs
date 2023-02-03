using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.domins
{
    public class admin
    {
        [Key]
        public string adUsername { get; set; }
        [Required]
        public string adPassword { get; set; }

    }
}
