namespace Faking.Core.Generators;

public class FloatGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => context.Random.NextSingle() + 0.001f;
    
    public bool CanGenerate(Type type) => type == typeof(float);
}