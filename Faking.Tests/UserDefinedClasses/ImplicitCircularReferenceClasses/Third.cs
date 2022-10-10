using System.Diagnostics.CodeAnalysis;

namespace Faking.Tests.UserDefinedClasses.ImplicitCircularReferenceClasses;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class Third
{
    public Third(First? first) => First = first;

    public First? First { get; set; }
}