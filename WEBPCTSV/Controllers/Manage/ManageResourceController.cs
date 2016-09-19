namespace WEBPCTSV.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.bo;

    public class ManageResourceController : Controller
    {
        private readonly DSAContext dsaContext;

        public ManageResourceController()
        {
            this.dsaContext = new DSAContext();
        }

        public ActionResult ManageResource()
        {
            ResourceBO resourceBO = new ResourceBO(this.dsaContext);
            this.ViewBag.functionAndDuty = resourceBO.getResourceObjectByAcronym("CNNV");
            this.ViewBag.lecturerOperation = resourceBO.getResourceObjectByAcronym("HoatDongGVCN");
            this.ViewBag.visionAndMission = resourceBO.getResourceObjectByAcronym("TNSM");
            return this.View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageResource(FormCollection col)
        {
            AccountSession accountSession = (AccountSession)this.Session["AccountSession"];
            if (accountSession != null)
            {
                bool isGranted = accountSession.Functions.IndexOf("ManageConfigWebsite") != -1;
                if (!isGranted)
                {
                    return this.View("~/Views/Shared/AdminDenyFunction.cshtml");
                }
                else
                {
                    // Granted
                    ResourceBO resourceBO = new ResourceBO(this.dsaContext);
                    Dictionary<string, string> conductTtemsParameter = col.AllKeys.ToDictionary(k => k, v => col[v]);
                    foreach (KeyValuePair<string, string> itemDic in conductTtemsParameter)
                    {
                        int id;
                        try
                        {
                            string idResource = itemDic.Key.Split('_')[1];
                            
                            id = int.Parse(idResource);
                            resourceBO.UpdateResource(id, itemDic.Value);
                        }
                        catch
                        {
                            continue;
                        }
                    }

                    return this.Redirect("/QuanLy/CauHinhWebsite");
                }
            }
            else
            {
                return this.Redirect("/QuanLy");
            }
        }
    }
}