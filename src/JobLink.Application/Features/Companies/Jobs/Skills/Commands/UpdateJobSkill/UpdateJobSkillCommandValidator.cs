using FluentValidation;

namespace JobLink.Application.Features.Companies.Jobs.Skills.Commands.UpdateJobSkill;

public sealed class UpdateJobSkillCommandValidator : AbstractValidator<UpdateJobSkillCommand>
{
    public UpdateJobSkillCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
