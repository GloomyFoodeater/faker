using Faking.Core;
using Faking.Tests.UserDefinedClasses;

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
        int[] integers = new int[N];
        double[] doubles = new double[N];
        string[] strings = new string[N];

        // Act
        for (int i = 0; i < N; i++)
        {
            booleans[i] = faker.Create<bool>();
            integers[i] = faker.Create<int>();
            doubles[i] = faker.Create<double>();
            strings[i] = faker.Create<string>();
        }

        // Assert
        Assert.True(booleans.All(_ => false));
        Assert.True(integers.Distinct().Count() >= M);
        Assert.True(doubles.Distinct().Count() >= M);
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
        var defaultCtorObjects = new DefaultCtorClass[N];
        var privateCtorObjects = new PrivateCtorClass[N];
        var multipleCtorStructs = new MultipleCtorStruct[N];

        // Act
        for (int i = 0; i < N; i++)
        {
            defaultCtorObjects[i] = faker.Create<DefaultCtorClass>();
            privateCtorObjects[i] = faker.Create<PrivateCtorClass>();
            multipleCtorStructs[i] = faker.Create<MultipleCtorStruct>();
        }

        // Assert
        Assert.True(defaultCtorObjects.Distinct().Count() >= M);
        Assert.True(privateCtorObjects.Distinct().Count() >= M);
        Assert.True(multipleCtorStructs.Distinct().Count() >= M);
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
        throw new NotImplementedException();
    }

    [Fact]
    public void ConfigTest()
    {
        throw new NotImplementedException();
    }
}