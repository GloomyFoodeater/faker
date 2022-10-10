namespace Faking.Core.Generators;

public class DateTimeGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) =>
        new DateTime(context.Random.NextInt64(DateTime.MinValue.Ticks, DateTime.MaxValue.Ticks));

    public Type GetGeneratedType() => typeof(DateTime);
}