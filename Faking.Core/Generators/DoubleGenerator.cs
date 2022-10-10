namespace Faking.Core.Generators;

public class DoubleGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => context.Random.NextDouble();
    
    public Type GetGeneratedType() => typeof(double);
}