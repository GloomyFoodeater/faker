using System.Diagnostics.CodeAnalysis;

namespace Faking.Tests.UserDefinedClasses;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class PrivateCtorClass
{
    private PrivateCtorClass(int intProperty, double doubleProperty, string stringField, DateTime dateTimeField)
    {
        IntProperty = intProperty;
        DoubleProperty = doubleProperty;
        StringField = stringField;
        DateTimeField = dateTimeField;

        _id = _objectsCounter;
        _objectsCounter++;
    }

    private static int _objectsCounter;

    private readonly int _id;

    public int IntProperty { get; set; }

    public double DoubleProperty { get; set; }

    public string? StringField;

    public DateTime? DateTimeField;

    public override bool Equals(object? obj)
    {
        if (obj is null || obj is not DefaultCtorClass other)
            throw new ArgumentException();

        return IntProperty == other.IntProperty &&
               Math.Abs(DoubleProperty - other.DoubleProperty) < 0.0001 &&
               (StringField is null && other.StringField is null || StringField == other.StringField) &&
               (DateTimeField is null && other.DateTimeField is null || DateTimeField.Equals(other.DateTimeField));
    }

    public override int GetHashCode() => _id;
}