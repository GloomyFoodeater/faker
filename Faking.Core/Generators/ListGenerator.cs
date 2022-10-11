using System.Collections;

namespace Faking.Core.Generators;

public class ListGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context)
    {
        int length = context.Random.Next(1, 10);
        var list = (IList?)Activator.CreateInstance(typeToGenerate, length);
        if (list != null)
        {
            var genericArgument = typeToGenerate.GetGenericArguments()[0];
            for (int i = 0; i < length; i++)
            {
                list.Add(context.Faker.Create(genericArgument));
            }
        }
        else
            throw new FakerException("Failed to generate list with given type");

        return list;
    }

    public bool CanGenerate(Type type) => type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>);
}