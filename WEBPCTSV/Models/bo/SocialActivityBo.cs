namespace WEBPCTSV.Models.bo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using WEBPCTSV.Models.bean;
    using WEBPCTSV.Models.Support;

    public class SocialActivityBo
    {
        readonly DSAContext context = new DSAContext();

        readonly int rowInPage = new Configuration().rowInPage;

        public void Delete(int idSocialActivity)
        {
            SocialActivity socialActivity =
                this.context.SocialActivities.Single(s => s.IdSocialActivity == idSocialActivity);
            this.context.SocialActivities.Remove(socialActivity);
            this.context.SaveChanges();
        }

        public List<SocialActivity> GetListSocialActivity(int page)
        {
            return
                this.context.SocialActivities.OrderByDescending(st => st.IdSocialActivity)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }

        public List<SocialActivity> GetListSocialActivity(int page, string search)
        {
            return
                this.context.SocialActivities.Where(
                    s =>
                    s.SocialActivityName.Contains(search) || s.OrganizationName.Contains(search)
                    || s.Place.Contains(search))
                    .OrderByDescending(st => st.IdSocialActivity)
                    .Skip((page - 1) * this.rowInPage)
                    .Take(this.rowInPage)
                    .ToList();
        }

        public void Insert(FormCollection form)
        {
            SocialActivity socialActivity = new SocialActivity();
            socialActivity.SocialActivityName = form["SocialActivityName"];
            socialActivity.OrganizationName = form["OrganizationName"];
            if (form["timeSocialActivity"] != string.Empty) socialActivity.Time = Convert.ToDateTime(form["timeSocialActivity"]);
            socialActivity.Place = form["placeSocialActivity"];
            this.context.SocialActivities.Add(socialActivity);
            this.context.SaveChanges();
        }

        public PageNumber TotalPage(int page)
        {
            PageNumber pageNumber = new PageNumber();
            pageNumber.PageNumberTotal = this.context.SocialActivities.ToList().Count / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/SocialActivity/ManageSocialActivity?page=";
            return pageNumber;
        }

        public PageNumber TotalPage(int page, string search)
        {
            PageNumber pageNumber = new PageNumber();
            pageNumber.PageNumberTotal =
                this.context.SocialActivities.Where(
                    s =>
                    s.SocialActivityName.Contains(search) || s.OrganizationName.Contains(search)
                    || s.Place.Contains(search)).ToList().Count / this.rowInPage + 1;
            pageNumber.PageNumberCurrent = page;
            pageNumber.Link = "/SocialActivity/SearchSocialActivity?search=" + search + "&page=";
            return pageNumber;
        }

        public void Update(int idSocialActivity, FormCollection form)
        {
            SocialActivity socialActivity =
                this.context.SocialActivities.Single(s => s.IdSocialActivity == idSocialActivity);
            socialActivity.SocialActivityName = form["SocialActivityName"];
            socialActivity.OrganizationName = form["OrganizationName"];
            if (form["timeSocialActivity"] != string.Empty) socialActivity.Time = Convert.ToDateTime(form["timeSocialActivity"]);
            socialActivity.Place = form["placeSocialActivity"];
            this.context.SocialActivities.Add(socialActivity);
            this.context.SaveChanges();
        }
    }
}