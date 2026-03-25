using FluentValidation;

namespace JobLink.Application.Features.JobSeekers.Skills.Commands.AddJobSeekerSkill;

public class AddJobSeekerSkillCommandValidator : AbstractValidator<AddJobSeekerSkillCommand>
{
    public AddJobSeekerSkillCommandValidator()
    {
        RuleFor(x => x.SkillId).NotEmpty();

        RuleFor(x => x.SkillLevel).IsInEnum();
    }
}