using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Has some items and gives a random one when requested.
// Once enough have been taken, returns them all to the bag.
public class RandomBag<T>
{

    private int _countWhenReset;
    private List<int> _remaining = new();
    private T[] _items;

    public RandomBag(T[] items, int countWhenReset)
    {
        _countWhenReset = countWhenReset;
        _items = items;
        if (countWhenReset > items.Length)
            throw new System.ArgumentException("countWhenReset > items.Length");
        Refill();
    }

    public T Next()
    {
        int indexInRemaining = Random.Range(0, _remaining.Count);
        int indexInItems = _remaining[indexInRemaining];
        _remaining.RemoveAt(indexInRemaining);
        T result = _items[indexInItems];
        if (_remaining.Count == _countWhenReset)
            Refill();
        return result;
    }

    private void Refill()
    {
        _remaining.Clear();
        for (int i = 0; i < _items.Length; i++)
            _remaining.Add(i);
    }
}
