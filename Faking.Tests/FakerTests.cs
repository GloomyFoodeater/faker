using Faking.Core;
using Faking.Tests.TestGenerators;
using Faking.Tests.UserDefinedClasses;
using Faking.Tests.UserDefinedClasses.ImplicitCircularReferenceClasses;
using static Faking.Core.Faker;

namespace Faking.Tests;

public class FakerTests
{
    [Fact]
    public void BuiltinTest()
    {
        // Arrange
        Faker faker = new();

        // Act
        bool @bool = faker.Create<bool>();
        byte @byte = faker.Create<byte>();
        char @char = faker.Create<char>();
        decimal @decimal = faker.Create<decimal>();
        double @double = faker.Create<double>();
        float @float = faker.Create<float>();
        int @int = faker.Create<int>();
        long @long = faker.Create<long>();
        short @short = faker.Create<short>();
        string @string = faker.Create<string>();

        // Assert
        Assert.NotEqual(GetDefault<bool>(), @bool);
        Assert.NotEqual(GetDefault<byte>(), @byte);
        Assert.NotEqual(GetDefault<char>(), @char);
        Assert.NotEqual(GetDefault<decimal>(), @decimal);
        Assert.NotEqual(GetDefault<double>(), @double);
        Assert.NotEqual(GetDefault<float>(), @float);
        Assert.NotEqual(GetDefault<int>(), @int);
        Assert.NotEqual(GetDefault<long>(), @long);
        Assert.NotEqual(GetDefault<short>(), @short);
        Assert.NotEqual(GetDefault<string>(), @string);
    }

    [Fact]
    public void DateTimeTest()
    {
        // Arrange
        Faker faker = new();

        // Act
        DateTime dateTime = faker.Create<DateTime>();

        // Assert
        Assert.NotEqual(GetDefault<DateTime>(), dateTime);
    }

    [Fact]
    public void ConstructorTest()
    {
        // Arrange
        Faker faker = new();

        // Act
        bool? nullable = faker.Create<bool?>();
        var defaultCtorObj = faker.Create<DefaultCtorClass>();
        var multipleCtorStruct = faker.Create<MultipleCtorStruct>();

        // Assert
        Assert.NotEqual(GetDefault<bool?>(), nullable);

        Assert.NotEqual(GetDefault<DefaultCtorClass>(), defaultCtorObj);
        Assert.NotEqual(GetDefault<int>(), defaultCtorObj.IntProperty);
        Assert.NotEqual(GetDefault<DateTime>(), defaultCtorObj.DateTimeField);

        Assert.NotEqual(GetDefault<MultipleCtorStruct>(), multipleCtorStruct);
        Assert.NotEqual(GetDefault<int>(), multipleCtorStruct.IntProperty);
        Assert.NotEqual(GetDefault<double>(), multipleCtorStruct.DoubleProperty);
        Assert.NotEqual(GetDefault<string>(), multipleCtorStruct.StringField);
        Assert.NotEqual(GetDefault<DateTime>(), multipleCtorStruct.DateTimeField);
        Assert.True(multipleCtorStruct.WasCorrectConstructorCalled());

        Assert.Throws<FakerException>(() => faker.Create<PrivateCtorClass>());
    }

    [Fact]
    public void CollectionTest()
    {
        // Arrange
        Faker faker = new();

        // Act
        var builtinObjects = faker.Create<List<int>>();
        var userDefinedObjects = faker.Create<List<DefaultCtorClass>>();
        var multiDimensionalList = faker.Create<List<List<string>>>();
        
        
        
        // Assert
        Assert.NotEmpty(builtinObjects);
        foreach (var obj in builtinObjects)
            Assert.NotEqual(GetDefault<int>(), obj);

        Assert.NotEmpty(userDefinedObjects);
        foreach (var obj in userDefinedObjects)
            Assert.NotEqual(GetDefault<DefaultCtorClass>(), obj);

        Assert.NotEmpty(multiDimensionalList);
        foreach (var list in multiDimensionalList)
        {
            Assert.NotEmpty(list);
            foreach (var obj in list)
                Assert.NotEqual(GetDefault<string>(), obj);
        }
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
    }

    [Fact]
    public void ConfigTest()
    {
        // Arrange
        FakerConfig config = new FakerConfig();
        config.Add<DefaultCtorClass, int, OnlyMaxGenerator>(x=>x.IntProperty);
        config.Add<DefaultCtorClass, DateTime, NowTimeGenerator>(x=>x.DateTimeField);
        Faker faker = new Faker(config);

        // Act
        var obj = faker.Create<DefaultCtorClass>();

        // Assert
        Assert.Equal(int.MaxValue, obj.IntProperty);
        Assert.Equal(DateTime.Today.Day, obj.DateTimeField.Day);
    }

    [Fact]
    public void EmptyCtorStructTest()
    {
        // Arrange
        Faker faker = new();
        
        // Act
        var defaultCtorStruct = faker.Create<DefaultCtorStruct>();
     
        // Assert
        Assert.NotEqual(GetDefault<DefaultCtorStruct>(), defaultCtorStruct);
        Assert.NotEqual(GetDefault<int>(), defaultCtorStruct.Number);
    }
}