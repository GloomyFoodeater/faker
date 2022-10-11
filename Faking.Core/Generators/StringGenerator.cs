namespace Faking.Core.Generators;

public class StringGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => Guid.NewGuid().ToString();

    public bool CanGenerate(Type type) => type == typeof(string);
}