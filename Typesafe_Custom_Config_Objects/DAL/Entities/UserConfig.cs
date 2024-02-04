using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ConfigFactory.DAL.Entities
{
    public class UserConfig
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ConfigName { get; set; }

        [Required]
        [MaxLength(50)]
        public string ConfigValue { get; set; }

        public int ConfigCategoryId { get; set; }
    }
}
