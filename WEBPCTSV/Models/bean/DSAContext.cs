namespace WEBPCTSV.Models.bean
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DSAContext : DbContext
    {
        public DSAContext()
            : base("name=DSAContext")
        {
        }

        public virtual DbSet<AcademicAchievement> AcademicAchievements { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<ActivityClub> ActivityClubs { get; set; }
        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<BloodGroup> BloodGroups { get; set; }
        public virtual DbSet<Borrowing> Borrowings { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Club> Clubs { get; set; }
        public virtual DbSet<ConditionStudentShipSchool> ConditionStudentShipSchools { get; set; }
        public virtual DbSet<ConductEvent> ConductEvents { get; set; }
        public virtual DbSet<ConductForm> ConductForms { get; set; }
        public virtual DbSet<ConductItemGroup> ConductItemGroups { get; set; }
        public virtual DbSet<ConductItemGroupRole> ConductItemGroupRoles { get; set; }
        public virtual DbSet<ConductItem> ConductItems { get; set; }
        public virtual DbSet<ConductResult> ConductResults { get; set; }
        public virtual DbSet<ConductSchedule> ConductSchedules { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Decentralization> Decentralizations { get; set; }
        public virtual DbSet<DecentralizationGroup> DecentralizationGroups { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<EmailSubscription> EmailSubscriptions { get; set; }
        public virtual DbSet<Ethnic> Ethnics { get; set; }
        public virtual DbSet<Faculty> Faculties { get; set; }
        public virtual DbSet<FamilyComposition> FamilyCompositions { get; set; }
        public virtual DbSet<Function> Functions { get; set; }
        public virtual DbSet<LearningOutCome> LearningOutComes { get; set; }
        public virtual DbSet<Lecturer> Lecturers { get; set; }
        public virtual DbSet<LecturerClass> LecturerClasses { get; set; }
        public virtual DbSet<LecturerClassDocument> LecturerClassDocuments { get; set; }
        public virtual DbSet<LecturerDocument> LecturerDocuments { get; set; }
        public virtual DbSet<LecturerDocumentSemester> LecturerDocumentSemesters { get; set; }
        public virtual DbSet<MadeOfStudy> MadeOfStudies { get; set; }
        public virtual DbSet<MemberClub> MemberClubs { get; set; }
        public virtual DbSet<MemberPossition> MemberPossitions { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<NewsEvent> NewsEvents { get; set; }
        public virtual DbSet<NewsJob> NewsJobs { get; set; }
        public virtual DbSet<NewsScholarship> NewsScholarships { get; set; }
        public virtual DbSet<OrganizationLevel> OrganizationLevels { get; set; }
        public virtual DbSet<Paper> Papers { get; set; }
        public virtual DbSet<PreferentialPolicyBeneficiary> PreferentialPolicyBeneficiaries { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<RankingAcademic> RankingAcademics { get; set; }
        public virtual DbSet<ReasonRequest> ReasonRequests { get; set; }
        public virtual DbSet<Regency> Regencies { get; set; }
        public virtual DbSet<RegencyStudent> RegencyStudents { get; set; }
        public virtual DbSet<Relative> Relatives { get; set; }
        public virtual DbSet<Religion> Religions { get; set; }
        public virtual DbSet<RequestPaper> RequestPapers { get; set; }
        public virtual DbSet<RequestStatus> RequestStatus { get; set; }
        public virtual DbSet<Resource> Resources { get; set; }
        public virtual DbSet<RewardDiscipLine> RewardDiscipLines { get; set; }
        public virtual DbSet<SecondLanguage> SecondLanguages { get; set; }
        public virtual DbSet<SecondLanguageStudent> SecondLanguageStudents { get; set; }
        public virtual DbSet<Semester> Semesters { get; set; }
        public virtual DbSet<SemesterYear> SemesterYears { get; set; }
        public virtual DbSet<SocialActivity> SocialActivities { get; set; }
        public virtual DbSet<SocialPolicyBeneficiariesStudent> SocialPolicyBeneficiariesStudents { get; set; }
        public virtual DbSet<SocialPolicyBeneficiary> SocialPolicyBeneficiaries { get; set; }
        public virtual DbSet<Specialize> Specializes { get; set; }
        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<StaffDepartmentOfStudentAffair> StaffDepartmentOfStudentAffairs { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentShipCLC> StudentShipCLCs { get; set; }
        public virtual DbSet<StudentShipCompany> StudentShipCompanies { get; set; }
        public virtual DbSet<StudentShipCompanyRegister> StudentShipCompanyRegisters { get; set; }
        public virtual DbSet<StudentShipCompanyStudent> StudentShipCompanyStudents { get; set; }
        public virtual DbSet<StudentShipSchool> StudentShipSchools { get; set; }
        public virtual DbSet<StudentShipSchoolFaculty> StudentShipSchoolFaculties { get; set; }
        public virtual DbSet<StudentShipSchoolStudent> StudentShipSchoolStudents { get; set; }

        public virtual DbSet<StudentshipApplication> StudentshipApplications { get; set; }
        public virtual DbSet<StudentSocialActivity> StudentSocialActivities { get; set; }
        public virtual DbSet<StudyingAboad> StudyingAboads { get; set; }
        public virtual DbSet<TypeInput> TypeInputs { get; set; }
        public virtual DbSet<TypePaper> TypePapers { get; set; }
        public virtual DbSet<TypePaperStudent> TypePaperStudents { get; set; }
        public virtual DbSet<Ward> Wards { get; set; }
        public virtual DbSet<Year> Years { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .HasMany(e => e.Lecturers)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.AccountReceiver)
                .HasForeignKey(e => e.IdAccountReceiver)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.Messages1)
                .WithRequired(e => e.AccountSender)
                .HasForeignKey(e => e.IdAccountSender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.RequestPapers)
                .WithOptional(e => e.AccountProcess)
                .HasForeignKey(e => e.IdAccountProcess);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.RequestPapers1)
                .WithRequired(e => e.AccountRequest)
                .HasForeignKey(e => e.IdAccountRequest)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Account>()
                .HasMany(e => e.StaffDepartmentOfStudentAffairs)
                .WithRequired(e => e.Account)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Class>()
                .HasMany(e => e.LecturerClasses)
                .WithRequired(e => e.Class)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Club>()
                .HasMany(e => e.ActivityClubs)
                .WithRequired(e => e.Club)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Club>()
                .HasMany(e => e.MemberClubs)
                .WithRequired(e => e.Club)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConductEvent>()
                .HasMany(e => e.ConductResults)
                .WithRequired(e => e.ConductEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConductEvent>()
                .HasMany(e => e.ConductSchedules)
                .WithRequired(e => e.ConductEvent)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConductForm>()
                .HasMany(e => e.ConductEvents)
                .WithRequired(e => e.ConductForm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConductForm>()
                .HasMany(e => e.ConductItems)
                .WithRequired(e => e.ConductForm)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConductItemGroup>()
                .HasMany(e => e.ConductItemGroupRoles)
                .WithRequired(e => e.ConductItemGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConductItemGroup>()
                .HasMany(e => e.ConductItems)
                .WithRequired(e => e.ConductItemGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ConductSchedule>()
                .HasMany(e => e.ConductResults)
                .WithRequired(e => e.ConductSchedule)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Course>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.Course)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DecentralizationGroup>()
                .HasMany(e => e.Accounts)
                .WithRequired(e => e.DecentralizationGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DecentralizationGroup>()
                .HasMany(e => e.ConductItemGroupRoles)
                .WithRequired(e => e.DecentralizationGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DecentralizationGroup>()
                .HasMany(e => e.ConductSchedules)
                .WithRequired(e => e.DecentralizationGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DecentralizationGroup>()
                .HasMany(e => e.Decentralizations)
                .WithRequired(e => e.DecentralizationGroup)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.DistrictHightSchoolName)
                .HasForeignKey(e => e.IdDistrictHightSchoolName);

            modelBuilder.Entity<District>()
                .HasMany(e => e.Wards)
                .WithRequired(e => e.District)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Classes)
                .WithRequired(e => e.Faculty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Lecturers)
                .WithRequired(e => e.Faculty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Faculty>()
                .HasMany(e => e.Specializes)
                .WithRequired(e => e.Faculty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Function>()
                .HasMany(e => e.Decentralizations)
                .WithRequired(e => e.Function)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LearningOutCome>()
                .HasMany(e => e.StudentShipSchoolStudents)
                .WithRequired(e => e.LearningOutCome)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Lecturer>()
                .HasMany(e => e.LecturerClasses)
                .WithRequired(e => e.Lecturer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<LecturerDocument>()
                .HasMany(e => e.LecturerDocumentSemesters)
                .WithRequired(e => e.LecturerDocument)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<MemberPossition>()
                .HasMany(e => e.MemberClubs)
                .WithRequired(e => e.MemberPossition)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .HasMany(e => e.NewsEvents)
                .WithRequired(e => e.News)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .HasMany(e => e.NewsJobs)
                .WithRequired(e => e.News)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<News>()
                .HasMany(e => e.NewsScholarships)
                .WithRequired(e => e.News)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<OrganizationLevel>()
                .HasMany(e => e.AcademicAchievements)
                .WithRequired(e => e.OrganizationLevel)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Paper>()
                .HasMany(e => e.ReasonRequests)
                .WithRequired(e => e.Paper)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Districts)
                .WithRequired(e => e.Province)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Province>()
                .HasMany(e => e.Students)
                .WithOptional(e => e.ProvinceIdentityCard)
                .HasForeignKey(e => e.IdProvinceIdentityCard);

            modelBuilder.Entity<RankingAcademic>()
                .HasMany(e => e.LearningOutComes)
                .WithRequired(e => e.RankingAcademic)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Regency>()
                .HasMany(e => e.RegencyStudents)
                .WithRequired(e => e.Regency)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Relative>()
                .Property(e => e.LandlineNumberRelatives)
                .HasPrecision(12, 0);

            modelBuilder.Entity<Relative>()
                .Property(e => e.CellphoneNumberRelatives)
                .HasPrecision(12, 0);

            modelBuilder.Entity<RequestStatus>()
                .HasMany(e => e.RequestPapers)
                .WithRequired(e => e.Requeststatus)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SecondLanguage>()
                .HasMany(e => e.SecondLanguageStudents)
                .WithRequired(e => e.SecondLanguage)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.AcademicAchievements)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.Borrowings)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.ConductEvents)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.LearningOutComes)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.StudentshipApplications)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.LecturerClasses)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.LecturerDocumentSemesters)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.RewardDiscipLines)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.StudentShipCompanies)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.StudentShipSchools)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Semester>()
                .HasMany(e => e.StudyingAboads)
                .WithRequired(e => e.Semester)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SemesterYear>()
                .HasMany(e => e.Semesters)
                .WithRequired(e => e.SemesterYear)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SocialActivity>()
                .HasMany(e => e.StudentSocialActivities)
                .WithRequired(e => e.Socialactivity)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SocialPolicyBeneficiary>()
                .HasMany(e => e.SocialPolicyBeneficiariesStudents)
                .WithRequired(e => e.SocialPolicyBeneficiary)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StaffDepartmentOfStudentAffair>()
                .Property(e => e.IdentityCardNumber)
                .HasPrecision(12, 0);

            modelBuilder.Entity<StaffDepartmentOfStudentAffair>()
                .Property(e => e.PhoneNumber)
                .HasPrecision(12, 0);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Ethnics)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<State>()
                .HasMany(e => e.Provinces)
                .WithRequired(e => e.State)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.AcademicAchievements)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Borrowings)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.ConductResults)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.LearningOutComes)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentshipApplications)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.MemberClubs)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.RegencyStudents)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Relatives)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.RewardDiscipLines)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SecondLanguageStudents)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.SocialPolicyBeneficiariesStudents)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentShipCompanyRegisters)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentShipCompanyStudents)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudentSocialActivities)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.StudyingAboads)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.TypePaperStudents)
                .WithRequired(e => e.Student)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentShipCompany>()
                .Property(e => e.Money)
                .HasPrecision(15, 0);

            modelBuilder.Entity<StudentShipCompany>()
                .HasMany(e => e.StudentShipCompanyRegisters)
                .WithRequired(e => e.StudentShipCompany)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentShipCompany>()
                .HasMany(e => e.StudentShipCompanyStudents)
                .WithRequired(e => e.StudentShipCompany)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentShipSchool>()
                .HasMany(e => e.ConditionStudentShipSchools)
                .WithRequired(e => e.StudentShipSchool)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentShipSchool>()
                .HasMany(e => e.StudentShipSchoolFaculties)
                .WithRequired(e => e.StudentShipSchool)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<StudentShipSchoolFaculty>()
                .HasMany(e => e.StudentShipSchoolStudents)
                .WithRequired(e => e.StudentShipSchoolFaculty)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TypePaper>()
                .HasMany(e => e.Typepaperstudents)
                .WithRequired(e => e.Typepaper)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.Relatives)
                .WithRequired(e => e.Ward)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.StudentsHaveSameBirthPlace)
                .WithOptional(e => e.WardBirthPlace)
                .HasForeignKey(e => e.IdWardBirthPlace);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.StudentsHaveSameExternAddress)
                .WithOptional(e => e.WardExternAddress)
                .HasForeignKey(e => e.IdWardExternAddress);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.StudentsHaveSameNativeLand)
                .WithOptional(e => e.WardNativeLand)
                .HasForeignKey(e => e.IdWardNativeLand);

            modelBuilder.Entity<Ward>()
                .HasMany(e => e.StudentsHaveSamePermanentResidence)
                .WithOptional(e => e.WardPermanentResidence)
                .HasForeignKey(e => e.IdWardPermanentResidence);

            modelBuilder.Entity<Year>()
                .HasMany(e => e.Semesters)
                .WithRequired(e => e.Year)
                .WillCascadeOnDelete(false);
        }
    }
}
