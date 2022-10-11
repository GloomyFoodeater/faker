namespace Faking.Core.Generators;

public class CharGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => (char)context.Random.Next(1, char.MaxValue);
    public bool CanGenerate(Type type) => type == typeof(char);
}