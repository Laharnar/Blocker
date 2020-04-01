using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Register:MonoBehaviour {

    [Tooltip("Register on start if you don't intend to have another way to register this script.")]
    public bool registerOnStart = true;
    public MonoBehaviour script;
    public Finder finder;

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
            GroupOfRegistered manager = GroupOfRegistered.PickManager(group);
            TryRegisterThis(manager);
        }
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
