namespace WEBPCTSV.Models.bean
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Function")]
    public partial class Function
    {
        public Function()
        {
            this.Decentralizations = new HashSet<Decentralization>();
        }

        public Function(string idFunction, string nameFunction)
        {
            this.IdFunction = idFunction;
            this.FunctionName = nameFunction;
        }

        public virtual ICollection<Decentralization> Decentralizations { get; set; }

        [StringLength(45)]
        public string FunctionName { get; set; }

        [Key]
        public string IdFunction { get; set; }
    }
}