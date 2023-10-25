using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A class to help with programmatic removal of all objects in a list.
/// </summary>
public class UIListManager
{
    public readonly List<GameObject> _visibleObjects = new();

    public void AddListObject(GameObject obj)
    {
        _visibleObjects.Add(obj);
    }

    public void DestroyListObjects()
    {
        foreach (var item in _visibleObjects)
            Object.Destroy(item);
        _visibleObjects.Clear();
    }
}