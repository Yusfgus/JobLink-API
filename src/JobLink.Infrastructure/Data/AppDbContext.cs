using JobLink.Domain.Identity;
using JobLink.Domain.JobSeekers;
using JobLink.Domain.Companies;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.Skills;
using Microsoft.EntityFrameworkCore;
using JobLink.Application.Common.Interfaces;
using JobLink.Domain.JobApplications;
using JobLink.Domain.SavedJobs;

namespace JobLink.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options), IAppDbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<JobSeekerProfile> JobSeekerProfiles => Set<JobSeekerProfile>();
    public DbSet<JobSeekerSkill> JobSeekerSkills => Set<JobSeekerSkill>();
    public DbSet<Education> Educations => Set<Education>();
    public DbSet<Experience> Experiences => Set<Experience>();
    public DbSet<Resume> Resumes => Set<Resume>();
    public DbSet<CompanyProfile> CompanyProfiles => Set<CompanyProfile>();
    public DbSet<CompanyLocation> Locations => Set<CompanyLocation>();
    public DbSet<Job> Jobs => Set<Job>();
    public DbSet<JobSkill> JobSkills => Set<JobSkill>();
    public DbSet<JobApplication> JobApplications => Set<JobApplication>();
    public DbSet<SavedJob> SavedJobs => Set<SavedJob>();
    public DbSet<Skill> Skills => Set<Skill>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}