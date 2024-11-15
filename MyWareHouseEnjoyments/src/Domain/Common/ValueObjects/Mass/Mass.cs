using System.ComponentModel.DataAnnotations;

namespace MyWarehouse.Domain.Common.ValueObjects.Mass;

/// <summary>
/// Value object that represents mass.
/// </summary>
public record Mass
{
    public float Value { get; init; }

    [Required]
    public MassUnit Unit { get; init; }

    public Mass(float value, MassUnit unit)
    {
        if (value < 0)
            throw new ArgumentException("Value cannot be negative", nameof(value));

        Value = value;
        Unit = unit;
    }

    public Mass ConvertTo(MassUnit newUnit)
    {
        if (newUnit == Unit)
            return this;

        return new Mass(
            value: Value * Unit.ConversionRateToGram / newUnit.ConversionRateToGram,
            unit: newUnit
        );
    }

    public override string ToString()
        => $"{Value:n} {Unit.Symbol}";
}
