using Faking.Core;

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
        throw new NotImplementedException();
    }
    
    [Fact]
    public void ConstructorTest()
    {
        throw new NotImplementedException();
    }
    
    [Fact]
    public void CollectionTest()
    {
        throw new NotImplementedException();
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