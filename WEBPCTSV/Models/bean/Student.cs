namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Student")]
    public partial class Student
    {
        public Student()
        {
            this.AcademicAchievements = new HashSet<AcademicAchievement>();
            this.Borrowings = new HashSet<Borrowing>();
            this.ConductResults = new HashSet<ConductResult>();
            this.LearningOutComes = new HashSet<LearningOutCome>();
            this.MemberClubs = new HashSet<MemberClub>();
            this.RegencyStudents = new HashSet<RegencyStudent>();
            this.Relatives = new HashSet<Relative>();
            this.RewardDiscipLines = new HashSet<RewardDiscipLine>();
            this.SecondLanguageStudents = new HashSet<SecondLanguageStudent>();
            this.SocialPolicyBeneficiariesStudents = new HashSet<SocialPolicyBeneficiariesStudent>();
            this.StudentShipCompanyRegisters = new HashSet<StudentShipCompanyRegister>();
            this.StudentShipCompanyStudents = new HashSet<StudentShipCompanyStudent>();
            this.StudentSocialActivities = new HashSet<StudentSocialActivity>();
            this.StudyingAboads = new HashSet<StudyingAboad>();
            this.TypePaperStudents = new HashSet<TypePaperStudent>();
            this.StudentshipApplications = new HashSet<StudentshipApplication>();
        }

        public virtual ICollection<AcademicAchievement> AcademicAchievements { get; set; }

        public virtual Account Account { get; set; }

        public string AdditionalBirthplace { get; set; }

        public string AdditionalExternAddress { get; set; }

        public string AdditionalNativeLand { get; set; }

        public string AdditionalPermanentResidence { get; set; }

        public virtual Area Area { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }

        public bool? BoardingAddress { get; set; }

        public virtual ICollection<Borrowing> Borrowings { get; set; }

        public virtual Class Class { get; set; }

        public virtual ICollection<ConductResult> ConductResults { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateAdmission { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfIssuanceIdentityCard { get; set; }

        public virtual District DistrictHightSchoolName { get; set; }

        public string EducationalBackground { get; set; }

        public string Email { get; set; }

        public virtual Ethnic Ethnic { get; set; }

        public virtual FamilyComposition FamilyComposition { get; set; }

        public string FirstNameStudent { get; set; }

        [StringLength(20)]
        public string HealthInsuranceNumber { get; set; }

        public string HealthStatus { get; set; }

        public int? Height { get; set; }

        public string HightSchoolName { get; set; }

        public int? IdAccount { get; set; }

        public string IdArea { get; set; }

        public int? IdBloodGroup { get; set; }

        public int? IdClass { get; set; }

        public string IdDistrictHightSchoolName { get; set; }

        [StringLength(12)]
        public string IdentityCard { get; set; }

        public int? IdEthnic { get; set; }

        public int? IdFamilyComposition { get; set; }

        public int? IdMadeOfStudy { get; set; }

        public int? IdPreferentialPolicyBeneficiaries { get; set; }

        public string IdProvinceIdentityCard { get; set; }

        public int? IdReligion { get; set; }

        public int? IdSpecialize { get; set; }

        public string idState { get; set; }

        [Key]
        public int IdStudent { get; set; }

        public int? IdTypeInput { get; set; }

        public string IdWardBirthPlace { get; set; }

        public string IdWardExternAddress { get; set; }

        public string IdWardNativeLand { get; set; }

        public string IdWardPermanentResidence { get; set; }

        public bool? IsExpelled { get; set; }

        public bool? IsGraduated { get; set; }

        public bool? IsOrphan { get; set; }

        public bool? IsPoliticalParty { get; set; }

        public bool? IsReserved { get; set; }

        public bool? IsYouthUnion { get; set; }

        [StringLength(12)]
        public string LandlineNumber { get; set; }

        public string LastNameStudent { get; set; }

        public virtual ICollection<LearningOutCome> LearningOutComes { get; set; }

        public string LinkAvatar { get; set; }

        public virtual MadeOfStudy MadeOfStudy { get; set; }

        public string MedicalExaminationTreatment { get; set; }

        public virtual ICollection<MemberClub> MemberClubs { get; set; }

        [StringLength(12)]
        public string MobilePhoneNumber { get; set; }

        public string NameHouseholder { get; set; }

        public int? NumberBloodDonors { get; set; }

        public int? ObjectTuitionFee { get; set; }

        [StringLength(11)]
        public string PhoneNumberHouseholder { get; set; }

        public float? PointInput { get; set; }

        public virtual PreferentialPolicyBeneficiary PreferentialPolicyBeneficiary { get; set; }

        public virtual Province ProvinceIdentityCard { get; set; }

        public virtual ICollection<RegencyStudent> RegencyStudents { get; set; }

        public string RegisteredMedicalArea { get; set; }

        public string RegisteredNumber { get; set; }

        public DateTime? RegistrationTemporaryResidenceTime { get; set; }

        public virtual ICollection<Relative> Relatives { get; set; }

        public virtual Religion Religion { get; set; }

        public virtual ICollection<RewardDiscipLine> RewardDiscipLines { get; set; }

        public virtual ICollection<SecondLanguageStudent> SecondLanguageStudents { get; set; }

        [StringLength(20)]
        public string Sex { get; set; }

        public virtual ICollection<SocialPolicyBeneficiariesStudent> SocialPolicyBeneficiariesStudents { get; set; }

        public virtual Specialize Specialize { get; set; }

        public virtual State State { get; set; }

        [StringLength(15)]
        [Index(IsUnique = true)]
        public string StudentNumber { get; set; }

        public virtual ICollection<StudentshipApplication> StudentshipApplications { get; set; }

        public virtual ICollection<StudentShipCompanyRegister> StudentShipCompanyRegisters { get; set; }

        public virtual ICollection<StudentShipCompanyStudent> StudentShipCompanyStudents { get; set; }

        public virtual ICollection<StudentSocialActivity> StudentSocialActivities { get; set; }

        public virtual ICollection<StudyingAboad> StudyingAboads { get; set; }

        [StringLength(10)]
        public string TemporaryResidenceNotebookNumber { get; set; }

        public virtual TypeInput TypeInput { get; set; }

        public virtual ICollection<TypePaperStudent> TypePaperStudents { get; set; }

        public virtual Ward WardBirthPlace { get; set; }

        public virtual Ward WardExternAddress { get; set; }

        public virtual Ward WardNativeLand { get; set; }

        public virtual Ward WardPermanentResidence { get; set; }

        public int? Weight { get; set; }
    }
}