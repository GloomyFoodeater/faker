﻿using System.Reflection;

namespace Faking.Core;

public class ObjectMaker
{
    public ObjectMaker(Faker faker) => _faker = faker;

    private readonly IFaker _faker;

    public object MakeObject(Type type)
    {
        object obj = Create(type);
        FillMembers(type, obj);
        return obj;
    }

    private object? GetDefault(Type type) => type.IsValueType ? Activator.CreateInstance(type) : null;

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
                    .Select(p => p.GetType())
                    .Select(t => _faker.Create(t))
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
            switch (member)
            {
                case FieldInfo info:
                {
                    Type typeOfField = info.FieldType;

                    // Get values to compare.
                    object? fieldValue = info.GetValue(obj);
                    object? fieldDefault = GetDefault(typeOfField);

                    if (Equals(fieldValue, fieldDefault))
                        info.SetValue(obj, _faker.Create(typeOfField));
                    break;
                }
                case PropertyInfo info:
                {
                    Type typeOfProperty = info.PropertyType;

                    // Get values to compare.
                    object? propertyValue = info.GetValue(obj);
                    object? propertyDefault = GetDefault(typeOfProperty);

                    if (Equals(propertyValue, propertyDefault))
                        info.SetValue(obj, _faker.Create(typeOfProperty));
                    break;
                }
            }
        }
    }
}