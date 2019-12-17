﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : Singletone<ObjectPool>
{
    public List<PooledObject> objectPool = new List<PooledObject>();

    void Awake()
    {
        for (int ix = 0; ix < objectPool.Count; ++ix)
        {
            objectPool[ix].Initialize(transform);
        }
    }

    public bool PushToPool(GameObject item, Transform parent = null)
    {
        PooledObject pool = GetPoolItem(item.name);
        if (pool == null)
            return false;

        pool.PushToPool(item, parent == null ? transform : parent);
        return true;
    }

    public GameObject PopFromPool(string itemName, Transform parent = null) 
    {
        PooledObject pool = GetPoolItem(itemName);
        if (pool == null)
            return null;

        return pool.PopFromPool(parent);
    }

    PooledObject GetPoolItem(string itemName)
    {
        for (int ix = 0; ix < objectPool.Count; ++ix)
        {
            if (objectPool[ix].prefab.name.Equals(itemName))
                return objectPool[ix];
        }

        Debug.LogWarning("There's no matched pool list.");
        return null;
    }
}