using JobLink.Domain.Common.Results;

namespace JobLink.Domain.ValueObjects;

public record SalaryRange
{
    public decimal Min { get; }
    public decimal Max { get; }

    private SalaryRange() {}

    private SalaryRange(decimal min, decimal max)
    {
        Min = min;
        Max = max;
    }

    public static Result<SalaryRange> Create(decimal min, decimal max)
    {
        List<Error> errors = [];

        if (min < 0 || max < 0)
        {
            errors.Add(SalaryRangeError.MustBePositive);
        }

        if (min > max)
        {
            errors.Add(SalaryRangeError.MustBeValid);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new SalaryRange(min, max);
    }
}

public static class SalaryRangeError
{
    public static Error MustBePositive => Error.Validation("SalaryRange_MustBePositive", "Salary range must be positive");
    public static Error MustBeValid => Error.Validation("SalaryRange_MustBeValid", "Salary range must be valid");
}