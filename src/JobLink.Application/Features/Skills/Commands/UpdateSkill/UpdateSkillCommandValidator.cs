using FluentValidation;
using JobLink.Domain.Common.Constants;

namespace JobLink.Application.Features.Skills.Commands.UpdateSkill;

public sealed class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MinimumLength(SkillConstraints.NameMinLength).WithMessage($"Name must be at least {SkillConstraints.NameMinLength} characters long")
            .MaximumLength(SkillConstraints.NameMaxLength).WithMessage($"Name must be at most {SkillConstraints.NameMaxLength} characters long");
    }
}