namespace Faking.Core.Generators;

public class FloatGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => context.Random.NextSingle();
    public bool CanGenerate(Type type) => type == typeof(float);
}