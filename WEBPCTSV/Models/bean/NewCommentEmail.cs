namespace WEBPCTSV.Models.bean
{
    using Postal;

    public class NewCommentEmail : Email
    {
        public string Categories { get; set; }

        public string Comment { get; set; }

        public string Link { get; set; }

        public string Title { get; set; }

        public string To { get; set; }
    }
}