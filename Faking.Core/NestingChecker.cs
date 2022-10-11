namespace Faking.Core;

internal class NestingChecker
{
    public NestingChecker(int maxLevel) => _maxLevel = maxLevel;
    
    // Max value that could be in dictionary.
    private readonly int _maxLevel;

    // Dictionary of counters.
    private readonly Dictionary<Type, int> _levels = new();
    
    public void Put(Type type)
    {
        // Increment existing or create new pair.
        if (!_levels.TryAdd(type, 1))
            _levels[type]++;
    }

    public void Remove(Type type)
    {
        // Decrement existing pair.
        if(_levels.ContainsKey(type) && _levels[type] >= 0)
            _levels[type] -= 1;
    }

    // Check if max value was reached.
    public bool IsOverflowed(Type type) => _levels.ContainsKey(type) && _levels[type] >= _maxLevel;
}