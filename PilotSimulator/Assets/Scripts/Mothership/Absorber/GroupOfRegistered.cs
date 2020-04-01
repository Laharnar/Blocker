using System;
using System.Collections.Generic;
using UnityEngine;

public class GroupOfRegistered : MonoBehaviour {

    public GroupType group;

    [SerializeField] List<Register> registred = new List<Register>();
    public  List<Register> Registred { get => registred; }

    protected bool ignoreRegisterOnSelf = true;
    private Register selfRegister;

    protected void Awake()
    {
        selfRegister = GetComponent<Register>();
    }

    internal void Register(Register item)
    {
        if(ignoreRegisterOnSelf)
        {
            if (selfRegister != item)
            {
                registred.Add(item);
            }
        }
        else
        {
            registred.Add(item);
        }
    }

    public static GroupOfRegistered PickManager(List<GroupOfRegistered> group)
    {
        for (int i = 0; i < group.Count; i++)
        {
            if (group[i].group == GroupType.Manager)
            {
                return group[i];
            }
        }
        throw new NullReferenceException("No manager in given group.");
    }

    internal bool Unregister(Register item)
    {
        if (ignoreRegisterOnSelf)
        {
            if (selfRegister != item)
            {
                return registred.Remove(item);
            }
            return false;
        }
        else
        {
            return registred.Remove(item);
        }
    }

    internal T GetScript<T>(int i) where T: MonoBehaviour
    {
        if (Registred[i].script == null)
        {
            throw new System.NullReferenceException("Script isn't assigned to register " + i);
        }
        return (T)Registred[i].script;
    }
}
