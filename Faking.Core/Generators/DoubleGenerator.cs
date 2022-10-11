namespace Faking.Core.Generators;

public class DoubleGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => context.Random.NextDouble();
    public bool CanGenerate(Type type) => type == typeof(double);
}