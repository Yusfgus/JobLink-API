using FluentValidation;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.UpdateJobSeekerSkill;

public class UpdateJobSeekerSkillCommandValidator : AbstractValidator<UpdateJobSeekerSkillCommand>
{
    public UpdateJobSeekerSkillCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();

        RuleFor(x => x.SkillId).NotEmpty();

        RuleFor(x => x.SkillLevel).IsInEnum();
    }
}