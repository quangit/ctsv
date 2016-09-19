namespace WEBPCTSV.Controllers.Client
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    using WEBPCTSV.Helpers.Common;
    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class QuestionController : Controller
    {
        readonly DSAContext dsaContext;

        readonly QuestionBO questionBO;

        public QuestionController()
        {
            this.dsaContext = new DSAContext();
            this.questionBO = new QuestionBO(this.dsaContext);
        }

        public ActionResult CommonQuestion(int? page)
        {
            return this.View(this.questionBO.GetCommonQuestion(page));
        }

        public ActionResult DetailQuestion(int id)
        {
            return this.View(this.questionBO.GetQuestion(id));
        }

        public ActionResult ListQuestion(int? page)
        {
            return this.View(this.questionBO.GetListQuestionReplied(page));
        }

        public ActionResult RandomQuestion(int count)
        {
            this.ViewBag.randomQuestion = this.questionBO.GetRandomQuestion(count);
            return this.View();
        }

        public ActionResult RequestQuestion()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult RequestQuestion(FormCollection col)
        {
            Dictionary<string, string> body = new Dictionary<string, string>();
            body.Add("secret", "6LdUlR8TAAAAALR4yeUPsq-O842J1CzGIoej_Gi-");
            body.Add("response", col["g-recaptcha-response"]);
            string json = JsonUtils.GetJsonFromLink("https://www.google.com/recaptcha/api/siteverify", "POST", body);
            dynamic values = Newtonsoft.Json.Linq.JObject.Parse(json);
            string typeRequest = col["TypeRequest"];
            string information = col["Information"];
            string contentHtml = col["ContentHtml"];
            string name = col["Name"];
            string email = col["Email"];
            string field = col["Field"];
            string title = col["Title"];
            bool result = bool.Parse(values.success.ToString());
            if (result)
            {
                int idQuestion = this.questionBO.AddQuestion(
                    typeRequest,
                    information,
                    name,
                    email,
                    field,
                    title,
                    contentHtml);
                if (idQuestion != -1)
                {
                    return
                        this.Content(
                            "Câu hỏi của bạn đã được ghi nhận! Nội dung câu trả lời sẽ được gửi sớm qua email cho bạn!");
                }
                else
                {
                    return this.Content("Lỗi xảy ra trong quá trình gửi câu hỏi! Vui lòng Kiểm tra lại thông tin.");
                }
            }
            else
            {
                return this.Content("Nhập sai mã xác nhận! Vui lòng Kiểm tra lại thông tin.");
            }
        }
    }
}