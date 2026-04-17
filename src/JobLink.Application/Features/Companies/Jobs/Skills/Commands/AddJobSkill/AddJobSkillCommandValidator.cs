using FluentValidation;

namespace JobLink.Application.Features.Companies.Jobs.Skills.Commands.AddJobSkill;

public sealed class AddJobSkillCommandValidator : AbstractValidator<AddJobSkillCommand>
{
    public AddJobSkillCommandValidator()
    {
        RuleFor(x => x.JobId)
            .NotEmpty();

        RuleFor(x => x.SkillId)
            .NotEmpty();
    }
}
