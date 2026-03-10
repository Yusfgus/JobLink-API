using JobLink.Domain.Users;
using JobLink.Domain.JobSeekers;
using JobLink.Domain.JobSeekers.Educations;
using JobLink.Domain.JobSeekers.Experiences;
using JobLink.Domain.Companies;
using JobLink.Domain.Companies.Jobs;
using JobLink.Domain.JobApplications;
using JobLink.Domain.Skills;
using JobLink.Domain.SavedJobs;
using Microsoft.EntityFrameworkCore;
using JobLink.Domain.JobSeekers.Resumes;

namespace JobLink.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<User> Users { get; }
    DbSet<JobSeekerProfile> JobSeekerProfiles { get; }
    DbSet<JobSeekerSkill> JobSeekerSkills { get; }
    DbSet<Education> Educations { get; }
    DbSet<Experience> Experiences { get; }
    DbSet<Resume> Resumes { get; }
    DbSet<CompanyProfile> CompanyProfiles { get; }
    DbSet<CompanyLocation> Locations { get; }
    DbSet<Job> Jobs { get; }
    DbSet<JobSkill> JobSkills { get; }
    DbSet<JobApplication> JobApplications { get; }
    DbSet<SavedJob> SavedJobs { get; }
    DbSet<Skill> Skills { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
