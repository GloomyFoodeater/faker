using System.Diagnostics.CodeAnalysis;

namespace Faking.Tests.UserDefinedClasses;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
[SuppressMessage("ReSharper", "UnassignedField.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "NotAccessedField.Global")]
public class MultipleCtorStruct
{
#pragma warning disable CS8618
    public MultipleCtorStruct()
#pragma warning restore CS8618
    {
    }

    public MultipleCtorStruct(int intProperty) : this() => IntProperty = intProperty;

    public MultipleCtorStruct(int intProperty, double doubleProperty) : this(intProperty)
    {
        DoubleProperty = doubleProperty;
    }

    public MultipleCtorStruct(int intProperty, double doubleProperty, string stringField) :
        this(intProperty, doubleProperty)
    {
        StringField = stringField;
        _wasCorrectConstructor = true;
    }

    private readonly bool _wasCorrectConstructor;

    public bool WasCorrectConstructorCalled() => _wasCorrectConstructor;
    
    public int IntProperty { get; set; }

    public double DoubleProperty { get; set; }

    public string StringField;

    public DateTime DateTimeField;
}