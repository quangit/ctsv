using WEBPCTSV.Models.bean;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBPCTSV.Models.bo
{
    public class SocialPolicyBeneficiaryBo
    {
        DSAContext context = new DSAContext();
        public List<SocialPolicyBeneficiary> GetListSocialPolicyBeneficiary()
        {
            return context.SocialPolicyBeneficiaries.ToList();
        }

        public bool UpdateSocialPolicyBeneficiaryStudent(int idSocialPolicyBeneficiary,int idStudent)
        {
            SocialPolicyBeneficiariesStudent s = null;
            try
            {
                try
                {
                    s = context.SocialPolicyBeneficiariesStudents.Where(so => so.IdStudent == idStudent && so.IdSocialPolicyBeneficiaries == idSocialPolicyBeneficiary).First();
                    if (s != null)
                    {
                        context.SocialPolicyBeneficiariesStudents.Remove(s);
                        context.SaveChanges();
                        return true;
                    }
                }
                catch { }
                s = new SocialPolicyBeneficiariesStudent();
                s.IdSocialPolicyBeneficiaries = idSocialPolicyBeneficiary;
                s.IdStudent = idStudent;
                context.SocialPolicyBeneficiariesStudents.Add(s);
                context.SaveChanges();
                return true;
            }
            catch { }
            return false;
            
        }

    }
}