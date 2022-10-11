﻿using Faking.Core.Generators;

namespace Faking.Core;

public class Faker: IFaker
{
    private readonly GeneratorContext _context;
    private readonly List<IValueGenerator> _generators;
    private readonly ObjectMaker _maker;

    
    public Faker()
    {
        _context = new (new Random(), this);
        _maker = new(this);
        
        _generators = new List<IValueGenerator>
        {
            new BoolGenerator(),
            new ByteGenerator(),
            new CharGenerator(),
            new DateTimeGenerator(),
            new DecimalGenerator(),
            new DoubleGenerator(),
            new FloatGenerator(),
            new IntGenerator(),
            new ListGenerator(),
            new LongGenerator(),
            new ShortGenerator(),
            new StringGenerator()
        };
    }

    public T Create<T>() => (T)Create(typeof(T));

    public object Create(Type type)
    {
        // Builtins, DateTime and other types, which can be generated by generators in assembly.
        foreach (var generator in _generators)
            if (generator.CanGenerate(type))
                return generator.Generate(type, _context);

        // User-defined classes.
        object obj = _maker.MakeObject(type);
        
        return obj;
    }

    public static object? GetDefault(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;
    
    public static object? GetDefault<T>() => default(T);
}