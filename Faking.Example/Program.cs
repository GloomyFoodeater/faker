Type Bar(Type type)
{
    return type;
}

Type Foo<T>()
{
    return Bar(typeof(T));
}

Console.WriteLine(Foo<int>());
Console.WriteLine(Bar(typeof(int)));