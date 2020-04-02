using System;
using System.Collections.Generic;
using UnityEngine;
public class GroupOfRegistered : MonoBehaviour {

    public IntVar group;

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
