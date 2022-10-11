using System.Diagnostics.CodeAnalysis;

namespace Faking.Tests.UserDefinedClasses;


[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
public class PrivateCtorClass
{
    private PrivateCtorClass(int intProperty) => IntProperty = intProperty;

    public int IntProperty { get; set; }
}