using FluentValidation;

namespace JobLink.Application.Features.Skills.Commands.CreateSkill;

public sealed class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required");
    }
}