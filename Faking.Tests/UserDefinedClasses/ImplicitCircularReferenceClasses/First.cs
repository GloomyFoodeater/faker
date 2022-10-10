using System.Diagnostics.CodeAnalysis;

namespace Faking.Tests.UserDefinedClasses.ImplicitCircularReferenceClasses;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class First
{
    public First(Second? second) => Second = second;

    public Second? Second { get; set; }
}