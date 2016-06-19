namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using WEBPCTSV.Helpers.Common;

    public partial class News
    {
        public News()
        {
            Documents = new HashSet<Document>();
            NewsEvents = new HashSet<NewsEvent>();
            NewsJobs = new HashSet<NewsJob>();
            NewsScholarships = new HashSet<NewsScholarship>();
        }
        public News(string title, string description, string contentHtml, string type, string image, string attachedDocuments, DateTime updatedDate, string createdBy, bool isDeleted, bool isPinned)
        {
            if (!StringExtension.IsNullOrWhiteSpace(title))
            {
                this.Title = title;
            }
            if (!StringExtension.IsNullOrWhiteSpace(description))
            {
                this.Description = description;
            }
            if (!StringExtension.IsNullOrWhiteSpace(contentHtml))
            {
                this.ContentHtml = contentHtml;
            }
            if (!StringExtension.IsNullOrWhiteSpace(type))
            {
                this.Type = type;
            }
            if (!StringExtension.IsNullOrWhiteSpace(image))
            {
                this.Image = image;
            }
            if (!StringExtension.IsNullOrWhiteSpace(attachedDocuments))
            {
                this.AttachedDocuments = attachedDocuments;
            }
            this.CreatedDate = DateTime.Now;
            this.UpdatedDate = DateTime.Now;
            this.CreatedBy = createdBy;
            this.ViewCount = 0;
            this.IsDeleted = isDeleted;
            this.IsPinned = isPinned;
        }
        [Key]
        public int IdNews { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string ContentHtml { get; set; }

        [Required]
        public string Type { get; set; }

        public string Image { get; set; }

        public string AttachedDocuments { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public int? ViewCount { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsPinned { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<NewsEvent> NewsEvents { get; set; }

        public virtual ICollection<NewsJob> NewsJobs { get; set; }

        public virtual ICollection<NewsScholarship> NewsScholarships { get; set; }
    }
}
