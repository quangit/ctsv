using WEBPCTSV.Models.bean;
using WEBPCTSV.Models.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WEBPCTSV.Models.bo
{
    public class SocialActivityBo
    {
        DSAContext context = new DSAContext();
        int rowInPage = new Configuration().rowInPage;
        public List<SocialActivity> GetListSocialActivity(int page)
        {
            return context.SocialActivities.OrderByDescending(st => st.IdSocialActivity).Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
        }
        public List<SocialActivity> GetListSocialActivity(int page,string search)
        {
            return context.SocialActivities.Where(
                s=>s.SocialActivityName.Contains(search)
                    || s.OrganizationName.Contains(search)
                    || s.Place.Contains(search)
                ).OrderByDescending(st => st.IdSocialActivity).Skip((page - 1) * rowInPage).Take(rowInPage).ToList();
        }

        public PageNumber TotalPage(int page)
        {
            PageNumber pageNumber = new PageNumber();
            pageNumber.PageNumberTotal = context.SocialActivities.ToList().Count/rowInPage+1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/SocialActivity/ManageSocialActivity?page=";
            return pageNumber;
        }
        public PageNumber TotalPage(int page,string search)
        {
            PageNumber pageNumber = new PageNumber();
            pageNumber.PageNumberTotal = context.SocialActivities.Where(
                s => s.SocialActivityName.Contains(search)
                    || s.OrganizationName.Contains(search)
                    || s.Place.Contains(search)
                ).ToList().Count / rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/SocialActivity/SearchSocialActivity?search="+search+"&page=";
            return pageNumber;
        }

        public void Update(int idSocialActivity , FormCollection form)
        {
            SocialActivity socialActivity = context.SocialActivities.Single(s=>s.IdSocialActivity == idSocialActivity);
            socialActivity.SocialActivityName = form["SocialActivityName"];
            socialActivity.OrganizationName = form["OrganizationName"];
            if (form["timeSocialActivity"] != "") socialActivity.Time = Convert.ToDateTime(form["timeSocialActivity"]);
            socialActivity.Place = form["placeSocialActivity"];
            context.SocialActivities.Add(socialActivity);
            context.SaveChanges();
        }

        public void Insert(FormCollection form)
        {
            SocialActivity socialActivity = new SocialActivity();
            socialActivity.SocialActivityName = form["SocialActivityName"];
            socialActivity.OrganizationName = form["OrganizationName"];
            if (form["timeSocialActivity"] != "") socialActivity.Time = Convert.ToDateTime(form["timeSocialActivity"]);
            socialActivity.Place = form["placeSocialActivity"];
            context.SocialActivities.Add(socialActivity);
            context.SaveChanges();
        }
        public void Delete(int idSocialActivity)
        {
            SocialActivity socialActivity = context.SocialActivities.Single(s => s.IdSocialActivity == idSocialActivity);
            context.SocialActivities.Remove(socialActivity);
            context.SaveChanges();
        }

        
        


    }
}