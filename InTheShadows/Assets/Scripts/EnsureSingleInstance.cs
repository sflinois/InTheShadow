using UnityEngine;
using System;
using System.Collections.Generic;

public class EnsureSingleInstance : MonoBehaviour
{
    private int uniqueId = 0;
    private bool originalOne = false;

    void Awake()
    {
        // Find all objects with this script
        uniqueId = GetInstanceID();
        UnityEngine.Object[] objects = FindObjectsOfType(typeof(EnsureSingleInstance));

        // Find all objects tagged with our uniqueId
        List<EnsureSingleInstance> clones = new List<EnsureSingleInstance>();
        foreach (UnityEngine.Object o in objects)
        {
            EnsureSingleInstance instance = o as EnsureSingleInstance;
            if (instance.uniqueId != uniqueId)
                clones.Add(instance);
        }

        // If there's only one copy, then we are original one, otherwise we have clones
        // and need to clean up
        foreach (EnsureSingleInstance clone in clones)
            Destroy(clone.gameObject);
    }
}