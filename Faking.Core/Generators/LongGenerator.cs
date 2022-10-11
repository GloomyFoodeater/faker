namespace Faking.Core.Generators;

public class LongGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => context.Random.NextInt64(1, long.MaxValue);
    public bool CanGenerate(Type type)
    {
        return type == typeof(long);
    }
}