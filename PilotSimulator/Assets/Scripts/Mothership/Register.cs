using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Register:MonoBehaviour {

    [Tooltip("Register on start if you don't intend to have another way to register this script.")]
    public bool registerOnStart = true;
    public MonoBehaviour script;
    public Finder finder;
    public IntVar belongsTo;

    bool registred = false;

    private void Start()
    {
        if (GetComponent<GroupOfRegistered>())
        {
            throw new InvalidProgramException("Object with register script cannot have GroupOfRegistered and vice-versa. Move it to another object.");
        }

        if (registerOnStart)
        {
            List<GroupOfRegistered> group = finder.SearchByAlliance<GroupOfRegistered>();
            GroupOfRegistered manager = PickTargetByType(group, belongsTo);
            TryRegisterThis(manager);
        }
    }

    public GroupOfRegistered PickTargetByType(List<GroupOfRegistered> group, IntVar target)
    {
        for (int i = 0; i < group.Count; i++)
        {
            if(group[i].group == null)
            {
                Debug.LogError("Group isn't assigned somewhere.", gameObject);
            }
            if (group[i].group.value == target.value)
            {
                return group[i];
            }
        }
        throw new NullReferenceException("No manager in given group.");
    }

    public void TryRegisterThis(GroupOfRegistered group)
    {
        if (!registred)
        {
            group.Register(this);
            registred = true;
        }
    }
    public void TryUnregisterThis(GroupOfRegistered group)
    {
        if (registred)
        {
            bool unregistred = group.Unregister(this);
            if (unregistred)
            {
                registred = false;
            }
        }
    }
}
