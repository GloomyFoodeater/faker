using System.Linq.Expressions;
using System.Reflection;

namespace Faking.Core;

public class FakerConfig
{
    public Dictionary<MemberInfo, IValueGenerator> MembersGenerators { get; } = new();

    public void Add<TSource, TMember, TGenerator>(Expression<Func<TSource, TMember>> expression)
    {
        var memberExpression = (MemberExpression)expression.Body;
        var generator = (IValueGenerator?)Activator.CreateInstance<TGenerator>();
        if(generator != null)
            MembersGenerators[memberExpression.Member] = generator;

    }
}