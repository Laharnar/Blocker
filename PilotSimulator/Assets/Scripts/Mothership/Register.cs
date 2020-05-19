using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Obsolete("Legacy/turn based.")]
public class Register : MonoBehaviour {

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
            TryRegisterThis(FindManager());
        }
    }

    private void OnDestroy()
    {
        if (registred)
        {
            TryUnregisterThis(FindManager());
        }
    }

    private GroupOfRegistered FindManager()
    {
        List<GroupOfRegistered> group = finder.SearchByAlliance<GroupOfRegistered>();
        GroupOfRegistered manager = PickFirstById(group, belongsTo);
        return manager;
    }

    private GroupOfRegistered PickFirstById(List<GroupOfRegistered> group, IntVar groupId)
    {
        if (group.Count == 0) {
            Debug.Log("NullError:Group for this type isn't in scene. Allowed to happen when quitting game. Can trigger followup error[R1]." + groupId.value);
            return null;
        }
        for (int i = 0; i < group.Count; i++)
        {
            if (group[i].groupId == null)
            {
                Debug.LogError("Group id isn't assigned on the target object." + group[i]+". This object will be excluded and script will continue.", group[i]);
            }
            else
            {
                if (group[i].groupId.value == groupId.value)
                {
                    return group[i];
                }
            }
        }
        
        throw new NullReferenceException("No manager of given group between existing groups. "+ groupId.value);
    }

    private void TryRegisterThis(GroupOfRegistered group)
    {
        if (group == null)
        {
            Debug.LogError("[R1]Group is null, can't register to null group.");
        }else if (!registred)
        {
            group.Register(this);
            registred = true;
        }
    }

    private void TryUnregisterThis(GroupOfRegistered group)
    {
        if(group == null) 
        {
            Debug.LogError("[R1]Group is null, can't unregister from null group.");
        }else if (registred)
        {
            bool unregistred = group.Unregister(this);
            if (unregistred)
            {
                registred = false;
            }
        }
    }
}
