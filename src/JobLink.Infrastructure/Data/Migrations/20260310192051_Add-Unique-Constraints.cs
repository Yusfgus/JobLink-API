using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobLink.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SavedJobs_JobSeekerProfileId",
                table: "SavedJobs");

            migrationBuilder.DropIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekerSkills_JobSeekerProfileId",
                table: "JobSeekerSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobSeekerProfileId",
                table: "JobApplications");

            migrationBuilder.CreateIndex(
                name: "IX_SavedJobs_JobSeekerProfileId_JobId",
                table: "SavedJobs",
                columns: new[] { "JobSeekerProfileId", "JobId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId_SkillId",
                table: "JobSkills",
                columns: new[] { "JobId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_JobSeekerProfileId_SkillId",
                table: "JobSeekerSkills",
                columns: new[] { "JobSeekerProfileId", "SkillId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobSeekerProfileId_JobId",
                table: "JobApplications",
                columns: new[] { "JobSeekerProfileId", "JobId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SavedJobs_JobSeekerProfileId_JobId",
                table: "SavedJobs");

            migrationBuilder.DropIndex(
                name: "IX_JobSkills_JobId_SkillId",
                table: "JobSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobSeekerSkills_JobSeekerProfileId_SkillId",
                table: "JobSeekerSkills");

            migrationBuilder.DropIndex(
                name: "IX_JobApplications_JobSeekerProfileId_JobId",
                table: "JobApplications");

            migrationBuilder.CreateIndex(
                name: "IX_SavedJobs_JobSeekerProfileId",
                table: "SavedJobs",
                column: "JobSeekerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_JobId",
                table: "JobSkills",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSeekerSkills_JobSeekerProfileId",
                table: "JobSeekerSkills",
                column: "JobSeekerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_JobSeekerProfileId",
                table: "JobApplications",
                column: "JobSeekerProfileId");
        }
    }
}
