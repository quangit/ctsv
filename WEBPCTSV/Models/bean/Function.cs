namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Function")]
    public partial class Function
    {
        public Function()
        {
            Decentralizations = new HashSet<Decentralization>();
        }
        public Function(string idFunction, string nameFunction)
        {
            this.IdFunction = idFunction;
            this.FunctionName = nameFunction;
        }
        [Key]
        public string IdFunction { get; set; }

        [StringLength(45)]
        public string FunctionName { get; set; }

        public virtual ICollection<Decentralization> Decentralizations { get; set; }
    }
}
