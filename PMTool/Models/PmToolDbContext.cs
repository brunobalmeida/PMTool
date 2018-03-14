using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PMTool.Models
{
    public partial class PmToolDbContext : DbContext
    {
        public virtual DbSet<Client> Client { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<ModelProject> ModelProject { get; set; }
        public virtual DbSet<ModelProjectTask> ModelProjectTask { get; set; }
        public virtual DbSet<ModelTask> ModelTask { get; set; }
        public virtual DbSet<OwnersLicense> OwnersLicense { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskInfo> TaskInfo { get; set; }
        public virtual DbSet<TaskList> TaskList { get; set; }

        public PmToolDbContext(DbContextOptions<PmToolDbContext>options):base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.BingWemasterToolsPassword)
                    .HasColumnName("bingWemasterToolsPassword")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BingWemasterToolsUrl)
                    .HasColumnName("bingWemasterToolsUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BingWemasterToolsUsername)
                    .HasColumnName("bingWemasterToolsUsername")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BusinessDescription)
                    .IsRequired()
                    .HasColumnName("businessDescription")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompetitorsUrl)
                    .HasColumnName("competitorsUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DomainLoginUrl)
                    .HasColumnName("domainLoginUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DomainPassword)
                    .HasColumnName("domainPassword")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DomainUsername)
                    .HasColumnName("domainUsername")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExpandPlaning).HasColumnName("expandPlaning"); 

                entity.Property(e => e.GoogleAnalyticsPassword)
                    .HasColumnName("googleAnalyticsPassword")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleAnalyticsUrl)
                    .HasColumnName("googleAnalyticsUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleAnalyticsUsername)
                    .HasColumnName("googleAnalyticsUsername")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleMyBusinessPassword)
                    .HasColumnName("googleMyBusinessPassword")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleMyBusinessUrl)
                    .HasColumnName("googleMyBusinessUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleMyBusinessUsername)
                    .HasColumnName("googleMyBusinessUsername")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleSearchConsolePassword)
                    .HasColumnName("googleSearchConsolePassword")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleSearchConsoleUrl)
                    .HasColumnName("googleSearchConsoleUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.GoogleSearchConsoleUsername)
                    .HasColumnName("googleSearchConsoleUsername")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HostingLoginUrl)
                    .HasColumnName("hostingLoginUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HostingPassword)
                    .HasColumnName("hostingPassword")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.HostingUserName)
                    .HasColumnName("hostingUserName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.KeyWords)
                    .HasColumnName("keyWords")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MarketingGoals)
                    .HasColumnName("marketingGoals")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MonthlyBudget).HasColumnName("monthlyBudget");

                entity.Property(e => e.MonthlyClientTarget).HasColumnName("monthlyClientTarget");

                entity.Property(e => e.OtherMarketingTypes)
                    .HasColumnName("otherMarketingTypes")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PersonId).HasColumnName("personId");

                entity.Property(e => e.ClientActiveFlag).HasColumnName("clientActiveFlag");

                entity.Property(e => e.SocialMedia)
                    .HasColumnName("socialMedia")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SocialMedia2)
                    .HasColumnName("socialMedia2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SocialMedia3)
                    .HasColumnName("socialMedia3")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SocialMedia4)
                    .HasColumnName("socialMedia4")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TargetAreas)
                    .IsRequired()
                    .HasColumnName("targetAreas")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TargetKeyPhases)
                    .IsRequired()
                    .HasColumnName("targetKeyPhases")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WebAddress)
                    .IsRequired()
                    .HasColumnName("webAddress")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WpLoginUrl)
                    .HasColumnName("wpLoginUrl")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WpPassword)
                    .HasColumnName("wpPassword")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.WpUserName)
                    .HasColumnName("wpUserName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Client)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKclient103479");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.Property(e => e.CountryId).HasColumnName("countryId");

                entity.Property(e => e.CountryName)
                    .IsRequired()
                    .HasColumnName("countryName")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.EmployeeNumber).HasColumnName("employeeNumber");

                entity.Property(e => e.PersonId).HasColumnName("personId");

                entity.Property(e => e.EmployeeActiveFlag).HasColumnName("employeeActiveFlag");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Employee)
                    .HasForeignKey(d => d.PersonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKemployee81123");
            });

            modelBuilder.Entity<ModelProject>(entity =>
            {
                entity.ToTable("modelProject");

                entity.Property(e => e.ModelProjectId).HasColumnName("modelProjectId");

                entity.Property(e => e.ModelDescription)
                    .HasColumnName("modelDescription")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasColumnName("modelName")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ModelProjectTask>(entity =>
            {
                entity.ToTable("modelProjectTask");

                entity.Property(e => e.ModelProjectTaskId).HasColumnName("modelProjectTaskId");

                entity.Property(e => e.ModelProjectId).HasColumnName("modelProjectId");

                entity.Property(e => e.ModelTaskId).HasColumnName("modelTaskId");

                entity.HasOne(d => d.ModelProject)
                    .WithMany(p => p.ModelProjectTask)
                    .HasForeignKey(d => d.ModelProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKmodelProje471037");

                entity.HasOne(d => d.ModelTask)
                    .WithMany(p => p.ModelProjectTask)
                    .HasForeignKey(d => d.ModelTaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKmodelProje677680");
            });

            modelBuilder.Entity<ModelTask>(entity =>
            {
                entity.ToTable("modelTask");

                entity.Property(e => e.ModelTaskId).HasColumnName("modelTaskId");

                entity.Property(e => e.ModelProjectId).HasColumnName("modelProjectId");

                entity.Property(e => e.ModelTaskDescription)
                    .IsRequired()
                    .HasColumnName("modelTaskDescription")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModelTaskDuration).HasColumnName("modelTaskDuration");

                entity.Property(e => e.ModelTaskName)
                    .IsRequired()
                    .HasColumnName("modelTaskName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ModelTaskWeight)
                    .HasColumnName("modelTaskWeight")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OwnersLicense>(entity =>
            {
                entity.ToTable("ownersLicense");

                entity.Property(e => e.OwnersLicenseId).HasColumnName("ownersLicenseId");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.LicenseEmail)
                    .IsRequired()
                    .HasColumnName("licenseEmail")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .IsRequired()
                    .HasColumnName("companyName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExpireDate)
                    .HasColumnName("expireDate")
                    .HasColumnType("date");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.PersonId).HasColumnName("personId");

                entity.Property(e => e.Address)
                    .HasColumnName("address")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Address2)
                    .HasColumnName("address2")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property((System.Linq.Expressions.Expression<Func<Person, byte[]>>)(e => (byte[])e.PersonImage))
                   .IsRequired()
                   .HasColumnName("personImage")
                   .HasColumnType("image");

                entity.Property(e => e.ImageContentType)
                   .HasColumnName("imageContentType")
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasColumnName("firstName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasColumnName("lastName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MiddleName)
                    .HasColumnName("middleName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.OwnersLicenseId).HasColumnName("ownersLicenseId");

                entity.Property(e => e.PersonImage)
                .IsRequired()
                .HasColumnName("personImage")
                .HasColumnType("image");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasColumnName("postalCode")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceId).HasColumnName("provinceId");

                entity.HasOne(d => d.OwnersLicense)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.OwnersLicenseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKperson361647");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKperson577331");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.ClientId).HasColumnName("clientId");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.EndDate)
                    .HasColumnName("endDate")
                    .HasColumnType("date");

                entity.Property(e => e.ProjectDescription)
                    .IsRequired()
                    .HasColumnName("projectDescription")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProjectName)
                .HasColumnName("projectName")
                .HasMaxLength(255)
                .IsUnicode(false);

                entity.Property(e => e.ProjectOpen).HasColumnName("projectOpen");

                entity.Property(e => e.StartDate)
                    .HasColumnName("startDate")
                    .HasColumnType("date");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKproject793797");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKproject226909");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("province");

                entity.Property(e => e.ProvinceId).HasColumnName("provinceId");

                entity.Property(e => e.CountryId).HasColumnName("countryId");

                entity.Property(e => e.ProvinceCode)
                    .IsRequired()
                    .HasColumnName("provinceCode")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceName)
                    .IsRequired()
                    .HasColumnName("provinceName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Province)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKprovince320043");
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.ToTable("task");

                entity.Property(e => e.TaskId).HasColumnName("taskId");

                entity.Property(e => e.EmployeeId).HasColumnName("employeeId");

                entity.Property(e => e.ExpectedDate)
                    .HasColumnName("expectedDate")
                    .HasColumnType("date");

                entity.Property(e => e.TaskDescription)
                    .IsRequired()
                    .HasColumnName("taskDescription")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TaskDuration).HasColumnName("taskDuration");

                entity.Property(e => e.TaskListId).HasColumnName("taskListId");

                entity.Property(e => e.TaskName)
                    .IsRequired()
                    .HasColumnName("taskName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TaskWeight).HasColumnName("taskWeight");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FKtask90562");

                entity.HasOne(d => d.TaskList)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.TaskListId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKtask191516");

                entity.Property(e => e.TaskActiveFlag).HasColumnName("taskActiveFlag");
            });

            modelBuilder.Entity<TaskInfo>(entity =>
            {
                entity.ToTable("taskInfo");

                entity.Property(e => e.TaskInfoId).HasColumnName("taskInfoId");

                entity.Property(e => e.TaskId).HasColumnName("taskId");

                entity.Property(e => e.TaskNote)
                    .HasColumnName("taskNote")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Task)
                    .WithMany(p => p.TaskInfo)
                    .HasForeignKey(d => d.TaskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKtaskInfo308518");
            });

            modelBuilder.Entity<TaskList>(entity =>
            {
                entity.ToTable("taskList");

                entity.Property(e => e.TaskListId).HasColumnName("taskListId");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.TaskListName)
                    .HasColumnName("taskListName")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TaskListOpen).HasColumnName("taskListOpen");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.TaskList)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKtaskList611773");
            });
        }
    }
}
