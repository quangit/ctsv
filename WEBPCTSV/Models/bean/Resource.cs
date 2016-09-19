namespace WEBPCTSV.Models.bean
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Resource")]
    public partial class Resource
    {
        [Key]
        public int IdResource { get; set; }

        public string ResourceAcronym { get; set; }

        public string ResourceContent { get; set; }

        [Required]
        public string ResourceName { get; set; }

        [Required]
        [StringLength(20)]
        public string ResourceType { get; set; }
    }
}