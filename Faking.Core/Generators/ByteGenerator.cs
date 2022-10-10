namespace Faking.Core.Generators;

public class ByteGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => (byte)context.Random.Next(1, byte.MaxValue);

    public Type GetGeneratedType() => typeof(byte);
}