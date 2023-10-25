
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// A dictionary that is able to be edited by the Unity inspector.
/// </summary>
[Serializable]
public class PseudoDictionary<TKey, TValue>
{
    [SerializeField]
    private List<KeyValuePair> entries = new();

    private List<Action> _changeListeners = new();

    [Serializable]
    public class KeyValuePair
    {
        public TKey key;
        public TValue value;

        public KeyValuePair(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }
    }

    public Dictionary<TKey, TValue> AsDictionary()
    {
        return entries
            .Where(entry => entry.key != null)
            .GroupBy(entry => entry.key)
            .Select(group=>group.First())
            .ToDictionary(entry => entry.key, entry => entry.value);
    }

    public void FromDictionary(Dictionary<TKey, TValue> dict)
    {
        entries = dict
            .Select(entry => new KeyValuePair(entry.Key, entry.Value))
            .ToList();
        CallChangeListeners();
    }

    public void CallChangeListeners()
    {
        foreach (var listener in _changeListeners)
            listener();
    }

    /// <summary>
    /// Use this to add a callback to the dictionary to be called whenever the dictionary changes.
    /// </summary>
    public void AddChangeListener(Action listener)
    {
        _changeListeners.Add(listener);
    }
    
    /// <summary>
    /// Use this to re-render the given menu whenever the dictionary changes.
    /// </summary>
    public void AddChangeListener(UIMenu uiMenu)
    {
        _changeListeners.Add(() =>
        {
            if (uiMenu != null)
                uiMenu.Render();
        });
    }
}