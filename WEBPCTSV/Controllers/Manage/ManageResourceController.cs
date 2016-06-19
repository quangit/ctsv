using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.bo;

namespace WEBPCTSV.Controllers
{
    public class ManageResourceController : Controller
    {
        private DSAContext dsaContext;

        public ManageResourceController()
        {
            dsaContext = new DSAContext();
        }

        #region infomation of alumni
        public ActionResult ManageResource()
        {
            ResourceBO resourceBO = new ResourceBO(dsaContext);
            ViewBag.functionAndDuty = resourceBO.getResourceObjectByAcronym("CNNV");
            ViewBag.lecturerOperation = resourceBO.getResourceObjectByAcronym("HoatDongGVCN");
            ViewBag.visionAndMission = resourceBO.getResourceObjectByAcronym("TNSM");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ManageResource(FormCollection col)
        {
                AccountSession accountSession = (AccountSession)Session["AccountSession"];
                if (accountSession != null)
                {
                    bool isGranted = (accountSession.Functions.IndexOf("ManageConfigWebsite") != -1);
                    if (!isGranted)
                    {
                        return View("~/Views/Shared/AdminDenyFunction.cshtml");
                    }
                    else
                    {
                        // Granted
                        ResourceBO resourceBO = new ResourceBO(dsaContext);
                        Dictionary<String, String> conductTtemsParameter = col.AllKeys.ToDictionary(k => k, v => col[v]);
                        foreach (KeyValuePair<string, String> itemDic in conductTtemsParameter)
                        {
                            int id;
                            try
                            {
                                String idResource = (itemDic.Key.Split('_'))[1]; ;
                                id = Int32.Parse(idResource);
                                resourceBO.UpdateResource(id, itemDic.Value);
                            }
                            catch
                            {
                                continue;
                            }
                        }
                        return Redirect("/QuanLy/CauHinhWebsite");
                    }
                }
                else
                {
                    return Redirect("/QuanLy");
                }
        }
        #endregion
    }
}
