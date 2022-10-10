namespace Faking.Core.Generators;

public class StringGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => Guid.NewGuid().ToString();

    public Type GetGeneratedType() => typeof(string);
}