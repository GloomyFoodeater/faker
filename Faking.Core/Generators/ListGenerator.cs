using System.Collections;

namespace Faking.Core.Generators;

public class ListGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        int length = context.Random.Next(1, 10);
        var list = (IList?)Activator.CreateInstance(typeToGenerate, length);
        if (list != null)
            for (int i = 0; i < length; i++)
                list.Add(context.Faker.Create(typeToGenerate.GetGenericArguments()[0]));
        else
            throw new FakerException("Failed to generate list with given type");

        return list;
    }

    public Type GetGeneratedType() => typeof(List<>);

    public bool CanGenerate(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
}