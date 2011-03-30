using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace GiveCampLondon
{
    public class SiteDataContext : DbContext
    {
        public SiteDataContext() : base("SiteDataContext")
        {

        }

        public SiteDataContext(string connectiongStringName)
            : base(connectiongStringName)
        {
        }

        public DbSet<Content> Content { get; set; }
        public DbSet<Charity> Charities { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Volunteer> Volunteers { get; set; }
        public DbSet<JobRole> JobRoles { get; set; }
        public DbSet<VolunteerJobRole> VolunteerJobRoles { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<VolunteerTechnology> VolunteerTechnologies { get; set; }
        public DbSet<ExperienceLevel> ExperienceLevels { get; set; }
        public DbSet<Document> Documents { get; set; }
        
        protected override void OnModelCreating(System.Data.Entity.ModelConfiguration.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>().Property(cp => cp.Id)
                .HasColumnName("ContentId");
            modelBuilder.Entity<Content>().HasKey(cp => cp.Id);
            modelBuilder.Entity<Content>().ToTable("dbo.Content");

            modelBuilder.Entity<Charity>().Property(c => c.Id)
                .HasColumnName("CharityId");
            modelBuilder.Entity<Charity>().Property(c => c.Name)
                .HasColumnName("CharityName");
            modelBuilder.Entity<Charity>().Property(c => c.MembershipId)
                .HasColumnName("MembershipId");
            modelBuilder.Entity<Charity>().HasKey(c => c.Id);
            modelBuilder.Entity<Charity>().ToTable("dbo.Charity");

            modelBuilder.Entity<Volunteer>().Property(v => v.Id)
                .HasColumnName("VolunteerId");
            modelBuilder.Entity<Volunteer>().Property(v => v.MembershipId)
                .HasColumnName("MembershipId");
            modelBuilder.Entity<Volunteer>().HasKey(v => v.Id);
            modelBuilder.Entity<Volunteer>().ToTable("dbo.Volunteer");
            modelBuilder.Entity<Volunteer>().Ignore(v => v.JobRoles);
            modelBuilder.Entity<Volunteer>().Ignore(v => v.Technologies);

            modelBuilder.Entity<JobRole>().Property(j => j.Id).HasColumnName("JobRoleId");
            modelBuilder.Entity<JobRole>().HasKey(j => j.Id);
            modelBuilder.Entity<JobRole>().ToTable("dbo.JobRole");

            modelBuilder.Entity<VolunteerJobRole>().HasKey(v => new { v.JobRoleId, v.VolunteerId });
            modelBuilder.Entity<VolunteerJobRole>().ToTable("dbo.VolunteerJobRoles");

            modelBuilder.Entity<Technology>().Property(t => t.Id).HasColumnName("TechnologyId");
            modelBuilder.Entity<Technology>().HasKey(t => t.Id);
            modelBuilder.Entity<Technology>().ToTable("dbo.Technology");

            modelBuilder.Entity<VolunteerTechnology>().HasKey(vt => new { vt.TechnologyId, vt.VolunteerId });
            modelBuilder.Entity<VolunteerTechnology>().ToTable("dbo.VolunteerTechnologies");

            modelBuilder.Entity<Team>().Property(t => t.Id)
                .HasColumnName("TeamId");
            modelBuilder.Entity<Team>().Property(t => t.Name)
                .HasColumnName("TeamName");
            modelBuilder.Entity<Team>().HasKey(t => t.Id);
            modelBuilder.Entity<Team>().ToTable("dbo.Team");
            
            modelBuilder.Entity<Setting>().Property(s => s.Id).HasColumnName("SettingId");
            modelBuilder.Entity<Setting>().HasKey(s => s.Id);
            modelBuilder.Entity<Setting>().ToTable("dbo.Setting");

            modelBuilder.Entity<ExperienceLevel>().Property(e => e.Id).HasColumnName("ExperienceLevelId");
            modelBuilder.Entity<ExperienceLevel>().HasKey(s => s.Id);
            modelBuilder.Entity<ExperienceLevel>().ToTable("dbo.ExperienceLevel");

            modelBuilder.Entity<Document>().Property(e => e.DocumentId).HasColumnName("DocumentId");
            modelBuilder.Entity<Document>().HasKey(s => s.DocumentId);
            modelBuilder.Entity<Document>().ToTable("dbo.Documents");


            base.OnModelCreating(modelBuilder);
        }
    }
}
