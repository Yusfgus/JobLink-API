using JobLink.Domain.Common;
using JobLink.Domain.Common.Results;

namespace JobLink.Domain.Skills;

public sealed class Skill : Entity
{
    public string Name { get; private set; } = default!;

    private Skill() { }

    private Skill(string name)
    {
        Name = name;
    }

    public static Result<Skill> Create(string name)
    {
        List<Error> errors = [];

        if (string.IsNullOrWhiteSpace(name))
        {
            errors.Add(SkillError.NameRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Skill(name);
    }

}
public static class SkillError
{
    public static Error NameRequired => Error.Validation("Skill_Name_Required", "Name is required");
}