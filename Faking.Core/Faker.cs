namespace Faking.Core;

public class Faker: IFaker
{
    public T Create<T>()
    {
        throw new NotImplementedException();
    }

    public object Create(Type type)
    {
        throw new NotImplementedException();
    }
}