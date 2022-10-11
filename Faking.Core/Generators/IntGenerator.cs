namespace Faking.Core.Generators;

public class IntGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => context.Random.Next(1, int.MaxValue);
    
    public bool CanGenerate(Type type) => type == typeof(int);
}