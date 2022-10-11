namespace Faking.Core.Generators;

public class DoubleGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => context.Random.NextDouble() + 0.001;
    
    public bool CanGenerate(Type type) => type == typeof(double);
}