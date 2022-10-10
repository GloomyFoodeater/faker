namespace Faking.Core.Generators;

public class BoolGenerator: IValueGenerator
{
    public object Generate(Type typeToGenerate, GeneratorContext context) => true;
    
    public Type GetGeneratedType() => typeof(bool);
}