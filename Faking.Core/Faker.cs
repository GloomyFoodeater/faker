﻿namespace Faking.Core;

public class Faker: IFaker
{
    private readonly GeneratorContext _context;
    private readonly List<IValueGenerator> _generators;
    private readonly ObjectMaker _maker;

    
    public Faker()
    {
        _context = new GeneratorContext(new Random(), this);
        _generators = new List<IValueGenerator>();
        _maker = new(this);

        // Get all types which implement IValueGenerator.
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(t => t
                .GetInterfaces()
                .Contains(typeof(IValueGenerator)));
        
        // Instantiate all generator implementations inside assembly.
        foreach (var type in types)
        {
            IValueGenerator? generator = (IValueGenerator?)Activator.CreateInstance(type);
            if (generator != null) 
                _generators.Add(generator);
        }
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