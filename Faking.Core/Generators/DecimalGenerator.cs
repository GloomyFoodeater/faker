namespace Faking.Core.Generators;

public class DecimalGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => (decimal)context.Random.NextDouble();

    public Type GetGeneratedType() => typeof(decimal);
}