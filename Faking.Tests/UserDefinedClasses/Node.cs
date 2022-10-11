using System.Diagnostics.CodeAnalysis;

namespace Faking.Tests.UserDefinedClasses;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global")]
[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
public class Node<T>
{
    public Node(T data, Node<T>? parent, List<Node<T>> children)
    {
        Data = data;
        Parent = parent;
        Children = children;
    }

    public T Data { get; set; }
    
    public Node<T>? Parent { get; set; }
    
    public List<Node<T>> Children { get; set; }
}