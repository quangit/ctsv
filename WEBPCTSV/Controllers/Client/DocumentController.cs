using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class DocumentController : Controller
    {
        DSAContext dsaContext;
        DocumentBO documentBO;

        public DocumentController()
        {
            dsaContext = new DSAContext();
            documentBO = new DocumentBO(dsaContext);
        }
        public ActionResult ListDocument(int? page, string type = null)
        {
            ViewBag.type = type;
            return View(documentBO.GetListDocument(page, type));
        }
        public ActionResult DetailDocument(int id)
        {
            return View(documentBO.GetDocument(id));
        }
        public ActionResult TopDocument(int count)
        {
            ViewBag.topDocument = documentBO.GetTopDocument(count);
            return View();
        }
    }
}
