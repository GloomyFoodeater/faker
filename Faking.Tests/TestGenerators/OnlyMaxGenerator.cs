using Faking.Core;

namespace Faking.Tests.TestGenerators;

public class OnlyMaxGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => int.MaxValue;

    public bool CanGenerate(Type type) => type == typeof(int);
}