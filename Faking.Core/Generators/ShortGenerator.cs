namespace Faking.Core.Generators;

public class ShortGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => (short)context.Random.Next(1, short.MaxValue);
   
    public bool CanGenerate(Type type) => type == typeof(short);
}