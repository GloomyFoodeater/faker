using Faking.Core;
using Faking.Tests.UserDefinedClasses;
using Faking.Tests.UserDefinedClasses.ImplicitCircularReferenceClasses;

namespace Faking.Tests;

public class FakerTests
{
    const int M = 30; // Min number of unique objects in each collection
    const int N = 50; // Number of test objects in each collection

    [Fact]
    public void BuiltinTest()
    {
        // Arrange
        Faker faker = new();
        bool[] booleans = new bool[N];
        byte[] bytes = new byte[N];
        char[] chars = new char[N];
        decimal[] decimals = new decimal[N];
        double[] doubles = new double[N];
        float[] floats = new float[N];
        int[] integers = new int[N];
        long[] longs = new long[N];
        short[] shorts = new short[N];
        string[] strings = new string[N];

        // Act
        for (int i = 0; i < N; i++)
        {
            booleans[i] = faker.Create<bool>();
            bytes[i] = faker.Create<byte>();
            chars[i] = faker.Create<char>();
            decimals[i] = faker.Create<decimal>();
            doubles[i] = faker.Create<double>();
            floats[i] = faker.Create<float>();
            integers[i] = faker.Create<int>();
            longs[i] = faker.Create<long>();
            shorts[i] = faker.Create<short>();
            strings[i] = faker.Create<string>();
        }

        // Assert
        Assert.True(booleans.All(_ => true));
        Assert.True(bytes.Distinct().Count() >= M);
        Assert.True(chars.Distinct().Count() >= M);
        Assert.True(decimals.Distinct().Count() >= M);
        Assert.True(doubles.Distinct().Count() >= M);
        Assert.True(floats.Distinct().Count() >= M);
        Assert.True(integers.Distinct().Count() >= M);
        Assert.True(longs.Distinct().Count() >= M);
        Assert.True(shorts.Distinct().Count() >= M);
        Assert.True(strings.Distinct().Count() >= M);
    }

    [Fact]
    public void DateTimeTest()
    {
        // Arrange
        Faker faker = new();
        var dateTimes = new DateTime[N];

        // Act
        for (int i = 0; i < N; i++)
        {
            dateTimes[i] = faker.Create<DateTime>();
        }

        // Assert
        Assert.True(dateTimes.Distinct().Count() >= M);
    }

    [Fact]
    public void ConstructorTest()
    {
        // Arrange
        Faker faker = new();
        var defaultCtorObjects = new DefaultCtorClass[N]; ;
        var multipleCtorStructs = new MultipleCtorStruct[N];

        // Act
        for (int i = 0; i < N; i++)
        {
            defaultCtorObjects[i] = faker.Create<DefaultCtorClass>();
            multipleCtorStructs[i] = faker.Create<MultipleCtorStruct>();
        }
        
        // Assert
        Assert.True(defaultCtorObjects.Distinct().Count() >= M);
        Assert.True(multipleCtorStructs.Distinct().Count() >= M);
        Assert.Throws<FakerException>(()=>faker.Create<PrivateCtorClass>());
    }

    [Fact]
    public void CollectionTest()
    {
        // Arrange
        Faker faker = new();

        // Act
        var builtinObjects = new[]
        {
            faker.Create<IEnumerable<int>>(),
            faker.Create<IEnumerable<int>>()
        };
        var userDefinedObjects = new[]
        {
            faker.Create<IEnumerable<DefaultCtorClass>>(),
            faker.Create<IEnumerable<DefaultCtorClass>>()
        };

        // Assert
        Assert.True(builtinObjects[0].Intersect(builtinObjects[1]).Distinct().Count() >= M);
        Assert.True(userDefinedObjects[0].Intersect(userDefinedObjects[1]).Distinct().Count() >= M);
    }

    [Fact]
    public void CircularReferenceTest()
    {
        // Arrange
        Faker faker = new();

        // Act
        var node = faker.Create<Node<int>>();
        var first = faker.Create<First>();

        // Assert
        Assert.NotNull(node.Parent);
        Assert.NotEmpty(node.Children);

        Assert.NotNull(first.Second);
        Assert.NotNull(first.Second!.Third);
        Assert.NotNull(first.Second!.Third!.First);
    }
}