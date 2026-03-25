using FluentValidation;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Features.Skills.Commands.CreateSkill;

public sealed class CreateSkillCommandValidator : AbstractValidator<CreateSkillCommand>
{
    public CreateSkillCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(SkillConstraints.NameMinLength).WithMessage($"Name must be at least {SkillConstraints.NameMinLength} characters long")
            .MaximumLength(SkillConstraints.NameMaxLength).WithMessage($"Name must be at most {SkillConstraints.NameMaxLength} characters long");
    }
}