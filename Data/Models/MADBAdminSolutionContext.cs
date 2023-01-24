using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace MADBHR_Data.Models
{
    public partial class MADBAdminSolutionContext : DbContext
    {
        public MADBAdminSolutionContext()
        {
        }

        public MADBAdminSolutionContext(DbContextOptions<MADBAdminSolutionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbAge60Full> TbAge60Full { get; set; }
        public virtual DbSet<TbAge60Full1> TbAge60Full1 { get; set; }
        public virtual DbSet<TbAward> TbAward { get; set; }
        public virtual DbSet<TbAwardType> TbAwardType { get; set; }
        public virtual DbSet<TbBranch> TbBranch { get; set; }
        public virtual DbSet<TbCurrentJobTownship> TbCurrentJobTownship { get; set; }
        public virtual DbSet<TbDepartment> TbDepartment { get; set; }
        public virtual DbSet<TbDisposal> TbDisposal { get; set; }
        public virtual DbSet<TbDisposalType> TbDisposalType { get; set; }
        public virtual DbSet<TbEducation> TbEducation { get; set; }
        public virtual DbSet<TbEducationType> TbEducationType { get; set; }
        public virtual DbSet<TbEmployee> TbEmployee { get; set; }
        public virtual DbSet<TbEmployeeRankType> TbEmployeeRankType { get; set; }
        public virtual DbSet<TbExamResult> TbExamResult { get; set; }
        public virtual DbSet<TbHonour> TbHonour { get; set; }
        public virtual DbSet<TbHonourType> TbHonourType { get; set; }
        public virtual DbSet<TbIntKnowledge> TbIntKnowledge { get; set; }
        public virtual DbSet<TbJobExperience> TbJobExperience { get; set; }
        public virtual DbSet<TbJobHistory> TbJobHistory { get; set; }
        public virtual DbSet<TbJobPosting> TbJobPosting { get; set; }
        public virtual DbSet<TbLeaveApplication> TbLeaveApplication { get; set; }
        public virtual DbSet<TbLeaveEntitlement> TbLeaveEntitlement { get; set; }
        public virtual DbSet<TbLeaveType> TbLeaveType { get; set; }
        public virtual DbSet<TbNrc> TbNrc { get; set; }
        public virtual DbSet<TbPension> TbPension { get; set; }
        public virtual DbSet<TbPensionType> TbPensionType { get; set; }
        public virtual DbSet<TbPlaceOfBirth> TbPlaceOfBirth { get; set; }
        public virtual DbSet<TbPunishment> TbPunishment { get; set; }
        public virtual DbSet<TbPunishmentType> TbPunishmentType { get; set; }
        public virtual DbSet<TbRank> TbRank { get; set; }
        public virtual DbSet<TbRankType> TbRankType { get; set; }
        public virtual DbSet<TbRegion> TbRegion { get; set; }
        public virtual DbSet<TbRegionSetUp> TbRegionSetUp { get; set; }
        public virtual DbSet<TbRelationship> TbRelationship { get; set; }
        public virtual DbSet<TbSalary> TbSalary { get; set; }
        public virtual DbSet<TbSkills> TbSkills { get; set; }
        public virtual DbSet<TbSonAndDaughter> TbSonAndDaughter { get; set; }
        public virtual DbSet<TbStateDivision> TbStateDivision { get; set; }
        public virtual DbSet<TbStateDivisionSetUp> TbStateDivisionSetUp { get; set; }
        public virtual DbSet<TbSubjects> TbSubjects { get; set; }
        public virtual DbSet<TbTownshipSetup> TbTownshipSetup { get; set; }
        public virtual DbSet<TbTrainingHistory> TbTrainingHistory { get; set; }
        public virtual DbSet<TbTrainingType> TbTrainingType { get; set; }
        public virtual DbSet<TbUser> TbUser { get; set; }
        public virtual DbSet<TbUserLogin> TbUserLogin { get; set; }
        public virtual DbSet<TbYearlyBonus> TbYearlyBonus { get; set; }
        public virtual DbSet<TbYearlyPunishmentType> TbYearlyPunishmentType { get; set; }
        public virtual DbSet<VwAge60Full> VwAge60Full { get; set; }
        public virtual DbSet<VwAward> VwAward { get; set; }
        public virtual DbSet<VwAwardInformationOnlineCheck> VwAwardInformationOnlineCheck { get; set; }
        public virtual DbSet<VwAwardList> VwAwardList { get; set; }
        public virtual DbSet<VwAwardType> VwAwardType { get; set; }
        public virtual DbSet<VwBranch> VwBranch { get; set; }
        public virtual DbSet<VwCurrentJobTownship> VwCurrentJobTownship { get; set; }
        public virtual DbSet<VwCurrentJobTownshipInformationOnlineCheck> VwCurrentJobTownshipInformationOnlineCheck { get; set; }
        public virtual DbSet<VwDeadEmployee> VwDeadEmployee { get; set; }
        public virtual DbSet<VwDepartment> VwDepartment { get; set; }
        public virtual DbSet<VwDisposalInformationOnlineCheck> VwDisposalInformationOnlineCheck { get; set; }
        public virtual DbSet<VwDisposalType> VwDisposalType { get; set; }
        public virtual DbSet<VwDisposalTypeInformationOnlineCheck> VwDisposalTypeInformationOnlineCheck { get; set; }
        public virtual DbSet<VwEducationType> VwEducationType { get; set; }
        public virtual DbSet<VwEducationTypeInformationOnlineCheck> VwEducationTypeInformationOnlineCheck { get; set; }
        public virtual DbSet<VwEmployee> VwEmployee { get; set; }
        public virtual DbSet<VwEmployeeDecreaseList> VwEmployeeDecreaseList { get; set; }
        public virtual DbSet<VwEmployeeInformationOnlineCheck> VwEmployeeInformationOnlineCheck { get; set; }
        public virtual DbSet<VwIntKnowledgeInformationOnlineCheck> VwIntKnowledgeInformationOnlineCheck { get; set; }
        public virtual DbSet<VwJobExperience> VwJobExperience { get; set; }
        public virtual DbSet<VwJobExperienceList> VwJobExperienceList { get; set; }
        public virtual DbSet<VwJobExperienceListSelectByCurrentRank> VwJobExperienceListSelectByCurrentRank { get; set; }
        public virtual DbSet<VwJobHistoryInformationOnlineCheck> VwJobHistoryInformationOnlineCheck { get; set; }
        public virtual DbSet<VwJobPosting> VwJobPosting { get; set; }
        public virtual DbSet<VwLeaveEntitlement> VwLeaveEntitlement { get; set; }
        public virtual DbSet<VwLeaveEntitlementOnlineCheck> VwLeaveEntitlementOnlineCheck { get; set; }
        public virtual DbSet<VwLeaveEntitlementSelectList> VwLeaveEntitlementSelectList { get; set; }
        public virtual DbSet<VwLeaveType> VwLeaveType { get; set; }
        public virtual DbSet<VwNrcname> VwNrcname { get; set; }
        public virtual DbSet<VwPension> VwPension { get; set; }
        public virtual DbSet<VwPensionInformationOnlineCheck> VwPensionInformationOnlineCheck { get; set; }
        public virtual DbSet<VwPensionOnlineCheck> VwPensionOnlineCheck { get; set; }
        public virtual DbSet<VwPensionType> VwPensionType { get; set; }
        public virtual DbSet<VwPensionTypeInformationOnlineCheck> VwPensionTypeInformationOnlineCheck { get; set; }
        public virtual DbSet<VwPlaceOfBirth> VwPlaceOfBirth { get; set; }
        public virtual DbSet<VwPlaceOfBirthInformationOnlineCheck> VwPlaceOfBirthInformationOnlineCheck { get; set; }
        public virtual DbSet<VwProfileSelect> VwProfileSelect { get; set; }
        public virtual DbSet<VwPunishment> VwPunishment { get; set; }
        public virtual DbSet<VwPunishmentInformationOnlineCheck> VwPunishmentInformationOnlineCheck { get; set; }
        public virtual DbSet<VwPunishmentType> VwPunishmentType { get; set; }
        public virtual DbSet<VwPunishmentTypeInformationOnlineCheck> VwPunishmentTypeInformationOnlineCheck { get; set; }
        public virtual DbSet<VwRankType> VwRankType { get; set; }
        public virtual DbSet<VwRankTypeInformationOnlineCheck> VwRankTypeInformationOnlineCheck { get; set; }
        public virtual DbSet<VwRankTypeSelect> VwRankTypeSelect { get; set; }
        public virtual DbSet<VwRelationInformationOnlineCheck> VwRelationInformationOnlineCheck { get; set; }
        public virtual DbSet<VwSonAndDaughterInformationOnlineCheck> VwSonAndDaughterInformationOnlineCheck { get; set; }
        public virtual DbSet<VwStateDivision> VwStateDivision { get; set; }
        public virtual DbSet<VwStateDivisionName> VwStateDivisionName { get; set; }
        public virtual DbSet<VwTrainingHistory> VwTrainingHistory { get; set; }
        public virtual DbSet<VwTrainingHistoryInformationOnlineCheck> VwTrainingHistoryInformationOnlineCheck { get; set; }
        public virtual DbSet<VwUser> VwUser { get; set; }
        public virtual DbSet<VwYearlyBonus> VwYearlyBonus { get; set; }
        public virtual DbSet<VwYearlyBonusInformationOnlineCheck> VwYearlyBonusInformationOnlineCheck { get; set; }
        public virtual DbSet<VwYearlyPunishment> VwYearlyPunishment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=MKTMGR\\SQLEXPRESS; Database=MADBAdminSolution; User Id=sa; Password=admin@123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbAge60Full>(entity =>
            {
                entity.HasKey(e => e.Age60FullPkid)
                    .HasName("PK_Age60Full");

                entity.ToTable("TB_Age60Full");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentRankDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.DepartmentPlace).HasMaxLength(500);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.LatestSalary).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcno)
                    .HasColumnName("NRCNo")
                    .HasMaxLength(50);

                entity.Property(e => e.PermanentDate).HasColumnType("datetime");

                entity.Property(e => e.RaceReligion).HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(50);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);
            });

            modelBuilder.Entity<TbAge60Full1>(entity =>
            {
                entity.HasKey(e => e.Age60FullPkid);

                entity.ToTable("TB_Age60Full1");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentRankDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.DepartmentPlace).HasMaxLength(500);

                entity.Property(e => e.Dob)
                    .HasColumnName("DOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.LatestSalary).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcno)
                    .HasColumnName("NRCNo")
                    .HasMaxLength(50);

                entity.Property(e => e.PermanentDate).HasColumnType("datetime");

                entity.Property(e => e.RaceReligion).HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(50);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);
            });

            modelBuilder.Entity<TbAward>(entity =>
            {
                entity.HasKey(e => e.AwardPkid);

                entity.ToTable("TB_Award");

                entity.Property(e => e.AwardDate).HasColumnType("datetime");

                entity.Property(e => e.AwardTypeCode).HasMaxLength(50);

                entity.Property(e => e.AwardYear).HasMaxLength(50);

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(500);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbAwardType>(entity =>
            {
                entity.HasKey(e => e.AwardTypePkid);

                entity.ToTable("TB_AwardType");

                entity.Property(e => e.AwardType).HasMaxLength(500);

                entity.Property(e => e.AwardTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbBranch>(entity =>
            {
                entity.HasKey(e => e.BranchPkid);

                entity.ToTable("TB_Branch");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.BranchName).HasMaxLength(200);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbCurrentJobTownship>(entity =>
            {
                entity.HasKey(e => e.CurrentJobTownshipPkid);

                entity.ToTable("TB_CurrentJob_Township");

                entity.Property(e => e.StateDivisionId)
                    .HasColumnName("StateDivisionID")
                    .HasMaxLength(50);

                entity.Property(e => e.Township).HasMaxLength(500);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbDepartment>(entity =>
            {
                entity.HasKey(e => e.DepartmentPkid);

                entity.ToTable("TB_Department");

                entity.Property(e => e.Department).HasMaxLength(500);

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbDisposal>(entity =>
            {
                entity.HasKey(e => e.DisposalPkid);

                entity.ToTable("TB_Disposal");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.DisposalDate).HasColumnType("datetime");

                entity.Property(e => e.DisposalTypeCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbDisposalType>(entity =>
            {
                entity.HasKey(e => e.DisposalTypePkid);

                entity.ToTable("TB_DisposalType");

                entity.Property(e => e.DisposalType).HasMaxLength(500);

                entity.Property(e => e.DisposalTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbEducation>(entity =>
            {
                entity.HasKey(e => e.EducationPkid);

                entity.ToTable("TB_Education");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EndYear).HasMaxLength(50);

                entity.Property(e => e.MainSubject).HasMaxLength(200);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.SchoolName).HasMaxLength(200);

                entity.Property(e => e.StartYear).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbEducationType>(entity =>
            {
                entity.HasKey(e => e.EducationTypePkid);

                entity.ToTable("TB_EducationType");

                entity.Property(e => e.EducationType).HasMaxLength(500);

                entity.Property(e => e.EducationTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbEmployee>(entity =>
            {
                entity.HasKey(e => e.EmployeePkid)
                    .HasName("PK_TB_Employee_1");

                entity.ToTable("TB_Employee");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Ancestor).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DearestPerson).HasMaxLength(500);

                entity.Property(e => e.EducationTypeCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EyeColor).HasMaxLength(50);

                entity.Property(e => e.FatherName)
                    .HasColumnName("Father_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Height).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasMaxLength(50);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.Mark).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MotherName)
                    .HasColumnName("Mother_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcnumber)
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.Nrcpic).HasColumnName("NRCPic");

                entity.Property(e => e.Occupation).HasMaxLength(50);

                entity.Property(e => e.OtherName).HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);

                entity.Property(e => e.Race).HasMaxLength(50);

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbEmployeeRankType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TB_EmployeeRankType");

                entity.Property(e => e.RankPkId)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RankType)
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.RankTypeCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbExamResult>(entity =>
            {
                entity.HasKey(e => e.ExamResultPkid);

                entity.ToTable("TB_ExamResult");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ExamDate).HasColumnType("datetime");

                entity.Property(e => e.ExamNumber).HasMaxLength(50);

                entity.Property(e => e.ExamType).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.SubjectCode).HasMaxLength(50);

                entity.Property(e => e.TotalMark).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbHonour>(entity =>
            {
                entity.HasKey(e => e.HonourPkid);

                entity.ToTable("TB_Honour");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.HonourDate).HasColumnType("datetime");

                entity.Property(e => e.HonourTitle).HasMaxLength(200);

                entity.Property(e => e.LetterNo).HasMaxLength(200);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbHonourType>(entity =>
            {
                entity.HasKey(e => e.HonourTypePkid);

                entity.ToTable("TB_HonourType");

                entity.Property(e => e.HonourType).HasMaxLength(500);

                entity.Property(e => e.HonourTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbIntKnowledge>(entity =>
            {
                entity.HasKey(e => e.IntKnowledgePkid);

                entity.ToTable("TB_IntKnowledge");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbJobExperience>(entity =>
            {
                entity.HasKey(e => e.JobExperiencePkid);

                entity.ToTable("TB_JobExperience");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.TotalPoint).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbJobHistory>(entity =>
            {
                entity.HasKey(e => e.JobHistoryPkid)
                    .HasName("PK_TB_JobHistory_1");

                entity.ToTable("TB_JobHistory");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.DepartmentName).HasMaxLength(100);

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.JobDay)
                    .HasColumnName("Job_Day")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JobMonth)
                    .HasColumnName("Job_Month")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JobYear)
                    .HasColumnName("Job_Year")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.RankTypeCode1)
                    .HasColumnName("RankType_Code")
                    .HasMaxLength(50);

                entity.Property(e => e.RankTypeCode11)
                    .HasColumnName("RankType_Code1")
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbJobPosting>(entity =>
            {
                entity.HasKey(e => e.JobPostingPkid);

                entity.ToTable("TB_JobPosting");

                entity.Property(e => e.Authority).HasMaxLength(100);

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankType).HasMaxLength(50);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.ToDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbLeaveApplication>(entity =>
            {
                entity.HasKey(e => e.LeaveApplicationPkid);

                entity.ToTable("TB_LeaveApplication");

                entity.Property(e => e.AppliedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedBy).HasMaxLength(50);

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.LeaveDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveStatus).HasMaxLength(50);

                entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbLeaveEntitlement>(entity =>
            {
                entity.HasKey(e => e.LeaveEntitlementPkid);

                entity.ToTable("TB_LeaveEntitlement");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedNo).HasMaxLength(50);

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Period).HasMaxLength(50);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbLeaveType>(entity =>
            {
                entity.HasKey(e => e.LeaveTypePkid);

                entity.ToTable("TB_LeaveType");

                entity.Property(e => e.LeaveType).HasMaxLength(500);

                entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);
            });

            modelBuilder.Entity<TbNrc>(entity =>
            {
                entity.HasKey(e => e.Nrcpkid);

                entity.ToTable("TB_NRC");

                entity.Property(e => e.Nrcpkid).HasColumnName("NRCPkid");

                entity.Property(e => e.Nrccode)
                    .HasColumnName("NRCCode")
                    .HasMaxLength(50);

                entity.Property(e => e.NrcenglishCode)
                    .HasColumnName("NRCEnglishCode")
                    .HasMaxLength(50);

                entity.Property(e => e.NrcmyanmarCode)
                    .HasColumnName("NRCMyanmarCode")
                    .HasMaxLength(50);

                entity.Property(e => e.Nrcnumber).HasColumnName("NRCNumber");
            });

            modelBuilder.Entity<TbPension>(entity =>
            {
                entity.HasKey(e => e.PensionPkid);

                entity.ToTable("TB_Pension");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(100);

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.LatestSalary).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MonthlyPension).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PensionBank).HasMaxLength(50);

                entity.Property(e => e.PensionDate).HasColumnType("datetime");

                entity.Property(e => e.PensionReportNo).HasMaxLength(50);

                entity.Property(e => e.PensionStartDate).HasColumnType("datetime");

                entity.Property(e => e.PensionTypeCode).HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(50);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.Saving).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbPensionType>(entity =>
            {
                entity.HasKey(e => e.PensionTypePkid);

                entity.ToTable("TB_PensionType");

                entity.Property(e => e.PensionType).HasMaxLength(500);

                entity.Property(e => e.PensionTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbPlaceOfBirth>(entity =>
            {
                entity.HasKey(e => e.TownshipPkid);

                entity.ToTable("TB_PlaceOfBirth");

                entity.Property(e => e.Township).HasMaxLength(500);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbPunishment>(entity =>
            {
                entity.HasKey(e => e.PunishmentPkid);

                entity.ToTable("TB_Punishment");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CrimeYear).HasMaxLength(50);

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNo).HasMaxLength(50);

                entity.Property(e => e.PunishmentTypeCode).HasMaxLength(500);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbPunishmentType>(entity =>
            {
                entity.HasKey(e => e.PunishmentTypePkid);

                entity.ToTable("TB_PunishmentType");

                entity.Property(e => e.PunishmentType).HasMaxLength(500);

                entity.Property(e => e.PunishmentTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);

                entity.Property(e => e.YearlyPunishmentTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbRank>(entity =>
            {
                entity.HasKey(e => e.RankPkid);

                entity.ToTable("TB_Rank");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderTitle).HasMaxLength(500);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbRankType>(entity =>
            {
                entity.HasKey(e => e.RankTypePkid);

                entity.ToTable("TB_RankType");

                entity.Property(e => e.EmployeeRankTypeCode).HasMaxLength(50);

                entity.Property(e => e.RankDescription).HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(500);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbRegion>(entity =>
            {
                entity.HasKey(e => e.RegionPkid);

                entity.ToTable("TB_Region");

                entity.Property(e => e.Region).HasMaxLength(500);

                entity.Property(e => e.RegionCode).HasMaxLength(50);

                entity.Property(e => e.TownshipId)
                    .HasColumnName("TownshipID")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TbRegionSetUp>(entity =>
            {
                entity.HasKey(e => e.RegionPkid);

                entity.ToTable("TB_Region_SetUp");

                entity.Property(e => e.Region).HasMaxLength(500);

                entity.Property(e => e.RegionCode).HasMaxLength(50);

                entity.Property(e => e.TownshipId)
                    .HasColumnName("TownshipID")
                    .HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbRelationship>(entity =>
            {
                entity.HasKey(e => e.RelationshipPkid);

                entity.ToTable("TB_Relationship");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.RelationAddress).HasMaxLength(200);

                entity.Property(e => e.RelationDob)
                    .HasColumnName("RelationDOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.RelationFatherName).HasMaxLength(50);

                entity.Property(e => e.RelationMotherName).HasMaxLength(50);

                entity.Property(e => e.RelationName).HasMaxLength(50);

                entity.Property(e => e.RelationOccupation).HasMaxLength(50);

                entity.Property(e => e.RelationType).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbSalary>(entity =>
            {
                entity.HasKey(e => e.SalaryPkid);

                entity.ToTable("TB_Salary");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.IncreaseTimes).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.SalaryAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ToDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<TbSkills>(entity =>
            {
                entity.HasKey(e => e.SkillsPkid);

                entity.ToTable("TB_Skills");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Skill).HasMaxLength(200);

                entity.Property(e => e.SkillLevel).HasMaxLength(50);
            });

            modelBuilder.Entity<TbSonAndDaughter>(entity =>
            {
                entity.HasKey(e => e.SonAndDaughterPkid);

                entity.ToTable("TB_SonAndDaughter");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.SonAndDaughterAddress).HasMaxLength(200);

                entity.Property(e => e.SonAndDaughterDob)
                    .HasColumnName("SonAndDaughterDOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.SonAndDaughterName).HasMaxLength(50);

                entity.Property(e => e.SonAndDaughterOccupation).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbStateDivision>(entity =>
            {
                entity.HasKey(e => e.StateDivisionPkid);

                entity.ToTable("TB_StateDivision");

                entity.Property(e => e.StateDivision).HasMaxLength(50);

                entity.Property(e => e.StateDivisionCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbStateDivisionSetUp>(entity =>
            {
                entity.HasKey(e => e.StateDivisionPkid);

                entity.ToTable("TB_StateDivision_SetUp");

                entity.Property(e => e.StateDivision).HasMaxLength(50);

                entity.Property(e => e.StateDivisionCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbSubjects>(entity =>
            {
                entity.HasKey(e => e.SubjectPkid);

                entity.ToTable("TB_Subjects");

                entity.Property(e => e.Subject).HasMaxLength(50);

                entity.Property(e => e.SubjectCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbTownshipSetup>(entity =>
            {
                entity.HasKey(e => e.TownshipPkid)
                    .HasName("PK_TB_Township_SetUp");

                entity.ToTable("TB_Township_Setup");

                entity.Property(e => e.StateDivisionId)
                    .HasColumnName("StateDivisionID")
                    .HasMaxLength(50);

                entity.Property(e => e.Township).HasMaxLength(500);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbTrainingHistory>(entity =>
            {
                entity.HasKey(e => e.TrainingHistoryPkid);

                entity.ToTable("TB_TrainingHistory");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.SchoolName).HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TrainingTitle).HasMaxLength(200);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<TbTrainingType>(entity =>
            {
                entity.HasKey(e => e.TrainingTypePkid);

                entity.ToTable("TB_TrainingType");

                entity.Property(e => e.TrainingType).HasMaxLength(500);

                entity.Property(e => e.TrainingTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<TbUser>(entity =>
            {
                entity.HasKey(e => e.UserPkid);

                entity.ToTable("TB_User");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.Property(e => e.LogoutTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<TbUserLogin>(entity =>
            {
                entity.HasKey(e => e.UserPkid);

                entity.ToTable("TB_UserLogin");

                entity.Property(e => e.AccountType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Office)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StateDivisionId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.TownshipId)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UsernameOrEmail)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TbYearlyBonus>(entity =>
            {
                entity.HasKey(e => e.YearlyBonusPkid);

                entity.ToTable("TB_YearlyBonus");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedNo).HasMaxLength(50);

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);

                entity.Property(e => e.YearlyBonusCount).HasMaxLength(50);

                entity.Property(e => e.YearlyBonusDate).HasColumnType("datetime");

                entity.Property(e => e.YearlyBonusSalary).HasMaxLength(50);
            });

            modelBuilder.Entity<TbYearlyPunishmentType>(entity =>
            {
                entity.HasKey(e => e.YearlyPunishmentPkid);

                entity.ToTable("TB_YearlyPunishmentType");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);

                entity.Property(e => e.YearlyPunishmentType).HasMaxLength(50);

                entity.Property(e => e.YearlyPunishmentTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<VwAge60Full>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Age60Full");

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentRankDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Dob)
                    .IsRequired()
                    .HasColumnName("DOB")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.Expr1)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Expr2)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Expr3)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcno)
                    .IsRequired()
                    .HasColumnName("NRCNo")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfBirth)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Race).HasMaxLength(50);

                entity.Property(e => e.RankTypeDescription)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<VwAward>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwAward");

                entity.Property(e => e.AwardDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AwardPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.AwardType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AwardTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AwardYear)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Reason).HasMaxLength(500);
            });

            modelBuilder.Entity<VwAwardInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwAwardInformationOnlineCheck");

                entity.Property(e => e.AwardDate).HasColumnType("datetime");

                entity.Property(e => e.AwardPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.AwardTypeCode).HasMaxLength(50);

                entity.Property(e => e.AwardYear).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(500);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwAwardList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_AwardList");

                entity.Property(e => e.AwardType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.AwardYear)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.JobAddress)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcnumber)
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);
            });

            modelBuilder.Entity<VwAwardType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwAwardType");

                entity.Property(e => e.AwardType).HasMaxLength(500);

                entity.Property(e => e.AwardTypeCode).HasMaxLength(50);

                entity.Property(e => e.AwardTypePkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwBranch>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwBranch");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.BranchName).HasMaxLength(200);

                entity.Property(e => e.BranchPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.TownshipCode).HasMaxLength(50);
            });

            modelBuilder.Entity<VwCurrentJobTownship>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwCurrentJobTownship");

                entity.Property(e => e.CurrentJobTownshipPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.Township).HasMaxLength(500);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);
            });

            modelBuilder.Entity<VwCurrentJobTownshipInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_CurrentJobTownshipInformationOnlineCheck");

                entity.Property(e => e.CurrentJobTownshipPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.StateDivisionId)
                    .HasColumnName("StateDivisionID")
                    .HasMaxLength(50);

                entity.Property(e => e.Township).HasMaxLength(500);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwDeadEmployee>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwDeadEmployee");

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Department)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FatherName)
                    .IsRequired()
                    .HasColumnName("Father_Name")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LatestSalary)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwDepartment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwDepartment");

                entity.Property(e => e.Department).HasMaxLength(500);

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.DepartmentPkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwDisposalInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwDisposalInformationOnlineCheck");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DisposalDate).HasColumnType("datetime");

                entity.Property(e => e.DisposalPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.DisposalTypeCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwDisposalType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_DisposalType");

                entity.Property(e => e.DisposalType).HasMaxLength(500);

                entity.Property(e => e.DisposalTypeCode).HasMaxLength(50);

                entity.Property(e => e.DisposalTypePkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwDisposalTypeInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwDisposalTypeInformationOnlineCheck");

                entity.Property(e => e.DisposalType).HasMaxLength(500);

                entity.Property(e => e.DisposalTypeCode).HasMaxLength(50);

                entity.Property(e => e.DisposalTypePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwEducationType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_EducationType");

                entity.Property(e => e.EducationType).HasMaxLength(500);

                entity.Property(e => e.EducationTypeCode).HasMaxLength(50);

                entity.Property(e => e.EducationTypePkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwEducationTypeInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwEducationTypeInformationOnlineCheck");

                entity.Property(e => e.EducationType).HasMaxLength(500);

                entity.Property(e => e.EducationTypeCode).HasMaxLength(50);

                entity.Property(e => e.EducationTypePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwEmployee>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwEmployee");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Age)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Ancestor).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentRankDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DearestPerson).HasMaxLength(500);

                entity.Property(e => e.EducationType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EducationTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EmployeePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.EyeColor).HasMaxLength(50);

                entity.Property(e => e.FatherName)
                    .HasColumnName("Father_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Height).HasMaxLength(50);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Mark).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MotherName)
                    .HasColumnName("Mother_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcnumber)
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.Occupation).HasMaxLength(50);

                entity.Property(e => e.OtherName).HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirthCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Race).HasMaxLength(50);

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.SerialNumberMyan)
                    .IsRequired()
                    .HasColumnName("SerialNumber_Myan")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Township)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);
            });

            modelBuilder.Entity<VwEmployeeDecreaseList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_EmployeeDecreaseList");

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentRankDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeDecreaseDateForArrive)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeDecreaseDateForDead)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeDecreaseDateForDismiss)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeDecreaseDateForPension)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeDecreaseDateForRemove)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeDecreaseDateForResign)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeDecreaseDateForTransfer)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JobAddress)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .IsRequired()
                    .HasColumnName("remark")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumberMyan)
                    .IsRequired()
                    .HasColumnName("SerialNumber_Myan")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwEmployeeInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwEmployeeInformationOnlineCheck");

                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.Ancestor).HasMaxLength(500);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.DearestPerson).HasMaxLength(500);

                entity.Property(e => e.EducationTypeCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EmployeePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.EyeColor).HasMaxLength(50);

                entity.Property(e => e.FatherName)
                    .HasColumnName("Father_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Gender).HasMaxLength(50);

                entity.Property(e => e.Height).HasMaxLength(50);

                entity.Property(e => e.IsActive).HasMaxLength(50);

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.Mark).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MotherName)
                    .HasColumnName("Mother_Name")
                    .HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcnumber)
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.Occupation).HasMaxLength(50);

                entity.Property(e => e.OtherName).HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);

                entity.Property(e => e.Race).HasMaxLength(50);

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwIntKnowledgeInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_IntKnowledgeInformationOnlineCheck");

                entity.Property(e => e.CountryName).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.IntKnowledgePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwJobExperience>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwJobExperience");

                entity.Property(e => e.CurrentJobYear)
                    .HasColumnName("Current_JobYear")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.JobDay)
                    .HasColumnName("Job_Day")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JobMonth)
                    .HasColumnName("Job_Month")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JobYear)
                    .HasColumnName("Job_Year")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JoinDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcnumber)
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(500);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.SerialNumberMyan)
                    .IsRequired()
                    .HasColumnName("SerialNumber_Myan")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TotalJobDay).HasColumnName("Total_JobDay");

                entity.Property(e => e.TotalJobMonth).HasColumnName("Total_JobMonth");

                entity.Property(e => e.TotalJobYear).HasColumnName("Total_JobYear");
            });

            modelBuilder.Entity<VwJobExperienceList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_JobExperienceList");

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.FromDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JobDay)
                    .IsRequired()
                    .HasColumnName("Job_Day")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JobMonth)
                    .IsRequired()
                    .HasColumnName("Job_Month")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JobYear)
                    .IsRequired()
                    .HasColumnName("Job_Year")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcnumber)
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.SerialNumberMyan)
                    .IsRequired()
                    .HasColumnName("SerialNumber_Myan")
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwJobExperienceListSelectByCurrentRank>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwJobExperienceListSelectByCurrentRank");

                entity.Property(e => e.AllTrc)
                    .IsRequired()
                    .HasColumnName("AllTRC")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.JobDay)
                    .HasColumnName("Job_Day")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JobMonth)
                    .HasColumnName("Job_Month")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JobYear)
                    .HasColumnName("Job_Year")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JoinDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcnumber)
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.Township)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwJobHistoryInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwJobHistoryInformationOnlineCheck");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Duration).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FromDate).HasColumnType("datetime");

                entity.Property(e => e.JobDay)
                    .HasColumnName("Job_Day")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JobHistoryPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.JobMonth)
                    .HasColumnName("Job_Month")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.JobYear)
                    .HasColumnName("Job_Year")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode)
                    .HasColumnName("RankType_Code")
                    .HasMaxLength(50);

                entity.Property(e => e.RankTypeCode1)
                    .HasColumnName("RankType_Code1")
                    .HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.ToDate).HasColumnType("datetime");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwJobPosting>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_JobPosting");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(100);

                entity.Property(e => e.DepartmentName1)
                    .IsRequired()
                    .HasColumnName("Department_Name")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FromDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JobDay)
                    .IsRequired()
                    .HasColumnName("Job_Day")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JobHistoryPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.JobMonth)
                    .IsRequired()
                    .HasColumnName("Job_Month")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JobYear)
                    .IsRequired()
                    .HasColumnName("Job_Year")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankTypeCode)
                    .IsRequired()
                    .HasColumnName("RankType_Code")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.ToDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwLeaveEntitlement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_LeaveEntitlement");

                entity.Property(e => e.ApprovedDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ApprovedNo).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveEntitlementPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Period).HasMaxLength(50);

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwLeaveEntitlementOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwLeaveEntitlementOnlineCheck");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedNo).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.LeaveEntitlementPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Period).HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwLeaveEntitlementSelectList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwLeaveEntitlement_Select_List");

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Period).HasMaxLength(50);

                entity.Property(e => e.SerialNumber).HasMaxLength(50);

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Township)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwLeaveType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_LeaveType");

                entity.Property(e => e.LeaveType).HasMaxLength(500);

                entity.Property(e => e.LeaveTypeCode).HasMaxLength(50);

                entity.Property(e => e.LeaveTypePkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwNrcname>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_NRCName");

                entity.Property(e => e.Nrcname)
                    .IsRequired()
                    .HasColumnName("NRCName")
                    .HasMaxLength(1);

                entity.Property(e => e.Nrcpkid).HasColumnName("NRCPkid");
            });

            modelBuilder.Entity<VwPension>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_Pension");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Department).HasMaxLength(100);

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.LatestSalary).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MonthlyPension).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PensionBank).HasMaxLength(50);

                entity.Property(e => e.PensionDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PensionPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.PensionReportNo).HasMaxLength(50);

                entity.Property(e => e.PensionStartDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PensionType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PensionTypeCode).HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(50);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.Saving).HasMaxLength(50);
            });

            modelBuilder.Entity<VwPensionInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPensionInformationOnlineCheck");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(100);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.LatestSalary).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MonthlyPension).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PensionBank).HasMaxLength(50);

                entity.Property(e => e.PensionDate).HasColumnType("datetime");

                entity.Property(e => e.PensionPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.PensionReportNo).HasMaxLength(50);

                entity.Property(e => e.PensionStartDate).HasColumnType("datetime");

                entity.Property(e => e.PensionTypeCode).HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.Saving).HasMaxLength(50);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwPensionOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPensionOnlineCheck");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Department).HasMaxLength(100);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.LatestSalary).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.MonthlyPension).HasMaxLength(50);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.PensionBank).HasMaxLength(50);

                entity.Property(e => e.PensionDate).HasColumnType("datetime");

                entity.Property(e => e.PensionPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.PensionReportNo).HasMaxLength(50);

                entity.Property(e => e.PensionStartDate).HasColumnType("datetime");

                entity.Property(e => e.PensionTypeCode).HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(50);

                entity.Property(e => e.Saving).HasMaxLength(50);
            });

            modelBuilder.Entity<VwPensionType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_PensionType");

                entity.Property(e => e.PensionType).HasMaxLength(500);

                entity.Property(e => e.PensionTypeCode).HasMaxLength(50);

                entity.Property(e => e.PensionTypePkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwPensionTypeInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPensionTypeInformationOnlineCheck");

                entity.Property(e => e.PensionType).HasMaxLength(500);

                entity.Property(e => e.PensionTypeCode).HasMaxLength(50);

                entity.Property(e => e.PensionTypePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwPlaceOfBirth>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPlaceOfBirth");

                entity.Property(e => e.Township).HasMaxLength(500);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);

                entity.Property(e => e.TownshipPkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwPlaceOfBirthInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_PlaceOfBirthInformationOnlineCheck");

                entity.Property(e => e.Township).HasMaxLength(500);

                entity.Property(e => e.TownshipCode).HasMaxLength(50);

                entity.Property(e => e.TownshipPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwProfileSelect>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_ProfileSelect");

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentRankDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LatestSalary)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Nrcnumber)
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(50);

                entity.Property(e => e.PlaceOfBirth).HasMaxLength(50);

                entity.Property(e => e.Race).HasMaxLength(50);

                entity.Property(e => e.Religion).HasMaxLength(50);
            });

            modelBuilder.Entity<VwPunishment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPunishment");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CrimeYear)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNo)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PunishmentPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.PunishmentType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PunishmentTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<VwPunishmentInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPunishmentInformationOnlineCheck");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CrimeYear).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(800);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.OrderNo).HasMaxLength(50);

                entity.Property(e => e.PunishmentPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.PunishmentTypeCode).HasMaxLength(500);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwPunishmentType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPunishmentType");

                entity.Property(e => e.PunishmentType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PunishmentTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.YearlyPunishmentType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.YearlyPunishmentTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwPunishmentTypeInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwPunishmentTypeInformationOnlineCheck");

                entity.Property(e => e.PunishmentType).HasMaxLength(500);

                entity.Property(e => e.PunishmentTypeCode).HasMaxLength(50);

                entity.Property(e => e.PunishmentTypePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);

                entity.Property(e => e.YearlyPunishmentTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<VwRankType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwRankType");

                entity.Property(e => e.EmployeeRankType)
                    .HasMaxLength(500)
                    .IsFixedLength();

                entity.Property(e => e.EmployeeRankTypeCode)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RankDescription).HasMaxLength(50);

                entity.Property(e => e.RankLevelMyan)
                    .IsRequired()
                    .HasColumnName("RankLevel_Myan")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankType).HasMaxLength(500);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);
            });

            modelBuilder.Entity<VwRankTypeInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwRankTypeInformationOnlineCheck");

                entity.Property(e => e.EmployeeRankTypeCode).HasMaxLength(50);

                entity.Property(e => e.RankDescription).HasMaxLength(50);

                entity.Property(e => e.RankType).HasMaxLength(500);

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.RankTypePkid).ValueGeneratedOnAdd();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwRankTypeSelect>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_RankType_Select");

                entity.Property(e => e.EmployeeRankType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.EmployeeRankTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankDescription)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankLevel)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankLevelMyan)
                    .IsRequired()
                    .HasColumnName("RankLevel_Myan")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankTypeCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.RankTypePkid)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRelationInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwRelationInformationOnlineCheck");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RelationAddress).HasMaxLength(200);

                entity.Property(e => e.RelationDob)
                    .HasColumnName("RelationDOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.RelationFatherName).HasMaxLength(50);

                entity.Property(e => e.RelationMotherName).HasMaxLength(50);

                entity.Property(e => e.RelationName).HasMaxLength(50);

                entity.Property(e => e.RelationOccupation).HasMaxLength(50);

                entity.Property(e => e.RelationType).HasMaxLength(50);

                entity.Property(e => e.RelationshipPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwSonAndDaughterInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwSonAndDaughterInformationOnlineCheck");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.SonAndDaughterAddress).HasMaxLength(200);

                entity.Property(e => e.SonAndDaughterDob)
                    .HasColumnName("SonAndDaughterDOB")
                    .HasColumnType("datetime");

                entity.Property(e => e.SonAndDaughterName).HasMaxLength(50);

                entity.Property(e => e.SonAndDaughterOccupation).HasMaxLength(50);

                entity.Property(e => e.SonAndDaughterPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwStateDivision>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwStateDivision");

                entity.Property(e => e.StateDivision).HasMaxLength(50);

                entity.Property(e => e.StateDivisionCode).HasMaxLength(50);

                entity.Property(e => e.StateDivisionPkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwStateDivisionName>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_StateDivisionName");

                entity.Property(e => e.StateDivision).HasMaxLength(50);

                entity.Property(e => e.StateDivisionCode).HasMaxLength(50);

                entity.Property(e => e.StateDivisionPkid).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwTrainingHistory>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTrainingHistory");

                entity.Property(e => e.BranchCode).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RankTypeCode).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.SchoolName).HasMaxLength(200);

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TrainingHistoryPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.TrainingTitle).HasMaxLength(200);
            });

            modelBuilder.Entity<VwTrainingHistoryInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwTrainingHistoryInformationOnlineCheck");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.SchoolName).HasMaxLength(200);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TrainingHistoryPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.TrainingTitle).HasMaxLength(200);

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);
            });

            modelBuilder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwUser");

                entity.Property(e => e.CreatedDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Department).HasMaxLength(500);

                entity.Property(e => e.DepartmentCode).HasMaxLength(50);

                entity.Property(e => e.LoginTime).HasColumnType("datetime");

                entity.Property(e => e.LogoutTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Password).HasMaxLength(100);

                entity.Property(e => e.Username).HasMaxLength(50);
            });

            modelBuilder.Entity<VwYearlyBonus>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_YearlyBonus");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedNo).HasMaxLength(50);

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.YearlyBonusCount).HasMaxLength(50);

                entity.Property(e => e.YearlyBonusDate).HasColumnType("datetime");

                entity.Property(e => e.YearlyBonusPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.YearlyBonusSalary).HasMaxLength(50);
            });

            modelBuilder.Entity<VwYearlyBonusInformationOnlineCheck>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vwYearlyBonusInformationOnlineCheck");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ApprovedNo).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.EmployeeCode).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.UploadForTownship).HasMaxLength(50);

                entity.Property(e => e.YearlyBonusCount).HasMaxLength(50);

                entity.Property(e => e.YearlyBonusDate).HasColumnType("datetime");

                entity.Property(e => e.YearlyBonusPkid).ValueGeneratedOnAdd();

                entity.Property(e => e.YearlyBonusSalary).HasMaxLength(50);
            });

            modelBuilder.Entity<VwYearlyPunishment>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_YearlyPunishment");

                entity.Property(e => e.CrimeYear)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.CurrentRank)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JobAddress)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.JoinDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Nrcnumber)
                    .IsRequired()
                    .HasColumnName("NRCNumber")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.OrderDate)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.SerialNumber)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.StateDivision)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.YearlyPunishmentType)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
