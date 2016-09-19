namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TypeInput")]
    public partial class TypeInput
    {
        public TypeInput()
        {
            this.Students = new HashSet<Student>();
        }

        [Key]
        public int IdTypeInput { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public string TypeInputName { get; set; }
    }
}