using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;
using WEBPCTSV.Helpers.Common;
namespace WEBPCTSV.Controllers.Client
{
    public class QuestionController : Controller
    {
        DSAContext dsaContext;
        QuestionBO questionBO;

        public QuestionController()
        {
            dsaContext = new DSAContext();
            questionBO = new QuestionBO(dsaContext);
        }
        public ActionResult RandomQuestion(int count)
        {
            ViewBag.randomQuestion = questionBO.GetRandomQuestion(count);
            return View();
        }

        public ActionResult CommonQuestion(int? page)
        {
            return View(questionBO.GetCommonQuestion(page));
        }
        public ActionResult ListQuestion(int? page)
        {
            return View(questionBO.GetListQuestionReplied(page));
        }
        public ActionResult DetailQuestion(int id)
        {
            return View(questionBO.GetQuestion(id));
        }
        public ActionResult RequestQuestion()
        {
            return View();
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
                int idQuestion = questionBO.AddQuestion(typeRequest, information, name, email, field, title, contentHtml);
                if (idQuestion != -1)
                {
                    return Content("Câu hỏi của bạn đã được ghi nhận! Nội dung câu trả lời sẽ được gửi sớm qua email cho bạn!");
                }
                else
                {
                    return Content("Lỗi xảy ra trong quá trình gửi câu hỏi! Vui lòng Kiểm tra lại thông tin.");
                }
            }
            else
            {
                return Content("Nhập sai mã xác nhận! Vui lòng Kiểm tra lại thông tin.");
            }
        }
    }
}
