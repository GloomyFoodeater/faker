using System.Diagnostics.CodeAnalysis;

namespace Faking.Tests.UserDefinedClasses.ImplicitCircularReferenceClasses;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class Second
{
    public Second(Third? third) => Third = third;

    public Third? Third { get; set; }
}