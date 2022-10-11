namespace Faking.Core;

public interface IFaker
{
    T Create<T>();
    
    object Create(Type type);
}