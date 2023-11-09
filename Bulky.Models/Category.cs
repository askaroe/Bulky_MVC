using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key] // data annotation, primary key 
        public int Id { get; set; }
        [Required] // cannot be null
        [MaxLength(30)] // the length of the name of category must be less than 30
        [DisplayName("Category Name")] 
        public string Name { get; set; }
        [DisplayName("Display Order")] // how this will be displayed in view
        [Range(1, 100)] // the value of display order must be between 1 and 100
        public int DisplayOrder { get; set; }
    }
}
