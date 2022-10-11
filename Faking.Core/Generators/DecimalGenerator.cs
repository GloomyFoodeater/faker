namespace Faking.Core.Generators;

public class DecimalGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => 
        (decimal)(context.Random.NextDouble() + 0.001);

    public bool CanGenerate(Type type) => type == typeof(decimal);
}