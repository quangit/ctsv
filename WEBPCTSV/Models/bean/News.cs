namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using WEBPCTSV.Helpers.Common;

    public partial class News
    {
        public News()
        {
            this.Documents = new HashSet<Document>();
            this.NewsEvents = new HashSet<NewsEvent>();
            this.NewsJobs = new HashSet<NewsJob>();
            this.NewsScholarships = new HashSet<NewsScholarship>();
        }

        public News(
            string title,
            string description,
            string contentHtml,
            string type,
            string image,
            string attachedDocuments,
            DateTime updatedDate,
            string createdBy,
            bool isDeleted,
            bool isPinned)
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

        public string AttachedDocuments { get; set; }

        [Required]
        public string ContentHtml { get; set; }

        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        [Key]
        public int IdNews { get; set; }

        public string Image { get; set; }

        public bool? IsDeleted { get; set; }

        public bool? IsPinned { get; set; }

        public virtual ICollection<NewsEvent> NewsEvents { get; set; }

        public virtual ICollection<NewsJob> NewsJobs { get; set; }

        public virtual ICollection<NewsScholarship> NewsScholarships { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Type { get; set; }

        public DateTime UpdatedDate { get; set; }

        public int? ViewCount { get; set; }
    }
}