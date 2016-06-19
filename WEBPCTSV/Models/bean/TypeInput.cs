namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TypeInput")]
    public partial class TypeInput
    {
        
        public TypeInput()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int IdTypeInput { get; set; }

        public string TypeInputName { get; set; }

        
        public virtual ICollection<Student> Students { get; set; }
    }
}
