using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Udemy.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        [DisplayName("Category name")]
        public string Name { get; set; }
        [DisplayName("Display order")]
        [Range(1, 100, ErrorMessage = "Display order should be in range from 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
