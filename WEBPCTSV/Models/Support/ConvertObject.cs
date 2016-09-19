namespace WEBPCTSV.Models.Support
{
    using System.Linq;
    using System.Text;

    using WEBPCTSV.Models.bean;

    public class ConvertObject
    {
        private static readonly string[] VietnameseSigns = new[]
                                                               {
                                                                   "aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ",
                                                                   "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ",
                                                                   "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ",
                                                                   "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ",
                                                                   "ÝỲỴỶỸ"
                                                               };

        DSAContext context = new DSAContext();

        public static string ConvertCurrency(string money)
        {
            try
            {
                int length = money.Length;
                int du = length % 3;
                for (int i = length - 1; i > 0; i--)
                {
                    if ((i % 3) == du) money = money.Insert(i, ".");
                }

                return money;
            }
            catch
            {
            }

            return null;
        }

        public static string ConvertCurrencyToString(string currency)
        {
            try
            {
                return currency.Replace(".", string.Empty);
            }
            catch
            {
            }

            return null;
        }

        public static string GetPersonNamebyAccount(Account account)
        {
            try
            {
                if (account.TypeAccount.Equals("SV"))
                {
                    Student student = account.Students.First();
                    return student.LastNameStudent + " " + student.FirstNameStudent;
                }

                if (account.TypeAccount.Equals("NV"))
                {
                    Staff staff = account.Staffs.First();
                    return staff.Name;
                }

                if (account.TypeAccount.Equals("GV"))
                {
                    Lecturer lecturer = account.Lecturers.First();
                    return lecturer.LastName + " " + lecturer.FirstName;
                }
            }
            catch
            {
            }

            return string.Empty;
        }

        public static string RemoveHtmlTags(string text)
        {
            bool isRemove = false;
            var builder = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '<') isRemove = true;
                if (text[i] == '>')
                {
                    isRemove = false;
                    continue;
                }

                if (!isRemove) builder.Append(text[i]);
            }

            builder.Append("...");
            text = builder.ToString();
            return text;
        }

        public static string ToEnglish(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++) str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }

            return str;
        }
    }
}