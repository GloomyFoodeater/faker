using Faking.Core;

namespace Faking.Tests.TestGenerators;

class NowTimeGenerator : IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => DateTime.Now;

    public bool CanGenerate(Type type) => type == typeof(DateTime);
}