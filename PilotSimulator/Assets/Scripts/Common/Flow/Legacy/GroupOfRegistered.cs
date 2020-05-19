using System.Collections.Generic;
using UnityEngine;

[System.Obsolete("Legacy/turn based.")]
public class GroupOfRegistered : MonoBehaviour {

    public IntVar groupId;

    [SerializeField] List<Register> registred = new List<Register>();
    public  List<Register> Registred { get => registred; }

    protected bool notAllowedRegisterOnSelf = true;
    private Register selfRegister;

    protected void Awake()
    {
        selfRegister = GetComponent<Register>();
    }

    internal void Register(Register item)
    {
        if(notAllowedRegisterOnSelf)
        {
            if (selfRegister == null)
            {
                registred.Add(item);
            }
            else
            {
                Debug.LogError("Based on settings, Register class isnt allowed on this object.", gameObject);
            }
        }
        else
        {
            registred.Add(item);
        }
    }

    

    internal bool Unregister(Register item)
    {
        if (notAllowedRegisterOnSelf)
        {
            if (selfRegister == null)
            {
                return registred.Remove(item);
            }
            else
            {
                Debug.LogError("Based on settings, Register class isnt allowed on this object.", gameObject);
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
