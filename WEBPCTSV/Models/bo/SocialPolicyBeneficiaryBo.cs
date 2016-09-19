namespace WEBPCTSV.Models.bo
{
    using System.Collections.Generic;
    using System.Linq;

    using WEBPCTSV.Models.bean;

    public class SocialPolicyBeneficiaryBo
    {
        readonly DSAContext context = new DSAContext();

        public List<SocialPolicyBeneficiary> GetListSocialPolicyBeneficiary()
        {
            return this.context.SocialPolicyBeneficiaries.ToList();
        }

        public bool UpdateSocialPolicyBeneficiaryStudent(int idSocialPolicyBeneficiary, int idStudent)
        {
            SocialPolicyBeneficiariesStudent s = null;
            try
            {
                try
                {
                    s =
                        this.context.SocialPolicyBeneficiariesStudents.Where(
                            so =>
                            so.IdStudent == idStudent && so.IdSocialPolicyBeneficiaries == idSocialPolicyBeneficiary)
                            .First();
                    if (s != null)
                    {
                        this.context.SocialPolicyBeneficiariesStudents.Remove(s);
                        this.context.SaveChanges();
                        return true;
                    }
                }
                catch
                {
                }

                s = new SocialPolicyBeneficiariesStudent();
                s.IdSocialPolicyBeneficiaries = idSocialPolicyBeneficiary;
                s.IdStudent = idStudent;
                this.context.SocialPolicyBeneficiariesStudents.Add(s);
                this.context.SaveChanges();
                return true;
            }
            catch
            {
            }

            return false;
        }
    }
}