namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        public Question() { }
        public Question(string typeRequest, string information, string name, string email, string field, string title, string contentHtml)
        {
            this.TypeRequest = typeRequest;
            this.Infomation = information;
            this.Name = name;
            this.Email = email;
            this.Field = field;
            this.Title = title;
            this.ContentHtml = contentHtml;
            this.IsDeleted = false;
            this.IsPinned = false;
            this.CreatedDate = DateTime.Now;
            this.ViewCount = 0;
        }
        [Key]
        public int IdQuestion { get; set; }

        [Required]
        public string TypeRequest { get; set; }

        public string Infomation { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Field { get; set; }

        [Required]
        public string Title { get; set; }

        public string ContentHtml { get; set; }

        public string Reply { get; set; }

        [StringLength(50)]
        public string ReplyBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? ReplyDate { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsPinned { get; set; }

        public int? ViewCount { get; set; }
    }
}
