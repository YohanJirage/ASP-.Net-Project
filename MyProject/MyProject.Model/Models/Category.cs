using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyProject.Models.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Category Name")]
        [MaxLength(40)]
        public string Name { get; set; }

        [Range(1, 100)]
        [DisplayName("Display Order")]
        public int DisplayOrder { get; set; }



    }
}
