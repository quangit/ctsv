namespace WEBPCTSV.Models.Support
{
    using WEBPCTSV.Models.bean;

    public class SelectPlaceSP
    {
        public SelectPlaceSP(string name, string note, Ward ward)
        {
            this.name = name;
            this.note = note;
            this.ward = ward;
        }

        public string name { get; set; }

        public string note { get; set; }

        public Ward ward { get; set; }
    }
}