using System.Reflection;
using static Faking.Core.Faker;

namespace Faking.Core;

internal class ObjectMaker
{
    public ObjectMaker(IFaker faker) => _faker = faker;

    public ObjectMaker(IFaker faker, FakerConfig config, GeneratorContext context) : this(faker)
    {
        _config = config;
        _context = context;
    }

    private readonly IFaker _faker;

    private readonly NestingChecker _nestingChecker = new(3);

    private readonly FakerConfig _config = new();

    private readonly GeneratorContext? _context;

    public object MakeObject(Type type)
    {
        object? obj;
        if (!_nestingChecker.IsOverflowed(type))
        {
            _nestingChecker.Put(type);

            // Making object
            obj = Create(type);
            FillMembers(type, obj);

            _nestingChecker.Remove(type);
        }
        else
            obj = null;

        // Outermost call will return non-null, null is returned only inside function.
        // Therefore suppressing without changing signature.
        return obj!;
    }

    private object Create(Type type)
    {
        // Get constructors and sort by parameter list length.
        var constructors = type
            .GetConstructors()
            .OrderByDescending(c => c.GetParameters().Length);

        // Try to create object with public constructors.
        foreach (var constructor in constructors)
        {
            try
            {
                // Create argument list with faker.
                var arguments = constructor.GetParameters()
                    .Select(p => _faker.Create(p.ParameterType))
                    .ToArray();

                var obj = constructor.Invoke(arguments);

                // Object was created.
                return obj;
            }
            catch (Exception e)
            {
                // Ignore.
            }
        }

        // Failed to create object with public constructors.
        throw new FakerException("Could not find suitable public constructor to create an object");
    }

    private void FillMembers(Type type, object obj)
    {
        // Get metadata of property and field members of given type.
        var dataMembers = type.GetMembers()
            .Where(m => (m.MemberType & (MemberTypes.Field | MemberTypes.Property)) != 0);

        // Fill all properties and fields if they are not default.
        foreach (var member in dataMembers)
        {
            IValueGenerator? priorityGenerator = null;
            if (_config.MembersGenerators.ContainsKey(member))
            {
                priorityGenerator = _config.MembersGenerators[member];
            }

            switch (member)
            {
                case FieldInfo info:
                {
                    Type typeOfField = info.FieldType;

                    // Get values to compare.
                    object? fieldValue = info.GetValue(obj);
                    object? fieldDefault = GetDefault(typeOfField);

                    if (Equals(fieldValue, fieldDefault))
                    {
                        if (priorityGenerator != null)
                        {
                            try
                            {
                                info.SetValue(obj, priorityGenerator.Generate(typeOfField, _context!));
                            }
                            catch
                            {
                                // Ignore.
                            }
                    }
                    else
                            info.SetValue(obj, _faker.Create(typeOfField));
                    }
                }
                    break;
                case PropertyInfo info:
                {
                    Type typeOfProperty = info.PropertyType;

                    // Get values to compare.
                    object? propertyValue = info.GetValue(obj);
                    object? propertyDefault = GetDefault(typeOfProperty);

                    if (Equals(propertyValue, propertyDefault))
                    {
                        if (priorityGenerator != null)
                        {
                            try
                            {
                                info.SetValue(obj, priorityGenerator.Generate(typeOfProperty, _context!));
                            }
                            catch
                            {
                                // Ignore.
                            }
                        }
                        else
                            info.SetValue(obj, _faker.Create(typeOfProperty));
                    }

                    break;
                }
            }
        }
    }
}