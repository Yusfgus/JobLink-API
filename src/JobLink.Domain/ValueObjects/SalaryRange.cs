namespace JobLink.Domain.ValueObjects;

public record SalaryRange(
    decimal MinSalary,
    decimal MaxSalary
);