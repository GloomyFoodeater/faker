using System.Diagnostics.CodeAnalysis;

namespace Faking.Tests.UserDefinedClasses;

[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
public struct DefaultCtorStruct
{
    public int Number { get; set; }
}