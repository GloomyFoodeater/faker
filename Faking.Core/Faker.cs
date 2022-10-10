namespace Faking.Core;

public class Faker: IFaker
{
    private readonly GeneratorContext _context;
    private readonly Dictionary<Type, IValueGenerator> _generators;
    private readonly ObjectMaker _maker;
    
    public Faker()
    {
        _context = new GeneratorContext(new Random(), this);
        _generators = new Dictionary<Type, IValueGenerator>();
        _maker = new(this);

        // Get all types which implement IValueGenerator.
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .Where(i => typeof(IValueGenerator).IsAssignableFrom(i) && i.IsClass);
        
        foreach (var type in types)
        {
            IValueGenerator? generator = (IValueGenerator?)Activator.CreateInstance(type);
            if (generator is not null) 
                _generators[generator.GetGeneratedType()] = generator;
        }
    }

    public T Create<T>() => (T)Create(typeof(T));

    public object Create(Type type)
    {
        // Builtins and DateTime types.
        if (_generators.ContainsKey(type))
            return _generators[type].Generate(type, _context);

        // User-defined classes.
        object obj = _maker.MakeObject(type);
        return obj;
    }

}