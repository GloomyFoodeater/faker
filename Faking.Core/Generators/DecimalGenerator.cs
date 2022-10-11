namespace Faking.Core.Generators;

public class DecimalGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => (decimal)context.Random.NextDouble();
    public bool CanGenerate(Type type) => type == typeof(decimal);
}