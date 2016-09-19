namespace WEBPCTSV.Models.bean
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Message")]
    public class Message
    {
        public virtual Account AccountReceiver { get; set; }

        public virtual Account AccountSender { get; set; }

        public string ContentMessage { get; set; }

        public int IdAccountReceiver { get; set; }

        public int IdAccountSender { get; set; }

        [Key]
        public int IdMessage { get; set; }

        public bool isReaded { get; set; }

        public string TextSummary { get; set; }

        public DateTime Time { get; set; }

        public string TitleMessage { get; set; }
    }
}