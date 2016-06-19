namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Resource")]
    public partial class Resource
    {
        [Key]
        public int IdResource { get; set; }

        [Required]
        public string ResourceName { get; set; }

        public string ResourceContent { get; set; }

        public string ResourceAcronym { get; set; }

        [Required]
        [StringLength(20)]
        public string ResourceType { get; set; }
    }
}
