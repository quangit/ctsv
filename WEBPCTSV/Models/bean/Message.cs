using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bean
{
    [Table("Message")]
    public class Message
    {
        [Key]
        public int IdMessage { get; set; }
        public string TitleMessage { get; set; }
        public string ContentMessage { get; set; }
        public int IdAccountSender { get; set; }
        public int IdAccountReceiver { get; set; }

        public DateTime Time { get; set; }
        public bool isReaded { get; set; }

        public string TextSummary { get; set; }

        public virtual Account AccountSender { get; set; }
        public virtual Account AccountReceiver { get; set; }
    }
}