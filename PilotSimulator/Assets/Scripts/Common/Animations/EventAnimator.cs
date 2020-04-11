using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimator : MonoBehaviour {
    public Animator target;

    string aname = "";

    public void SetBoolName(string bname)
    {
        this.aname = bname;
    }

    public void SetBool(bool value)
    {
        target.SetBool(aname, value);
    }

    public void SetFloatName(string fname)
    {
        this.aname = fname;
    }

    public void SetFloat(float value)
    {
        target.SetFloat(aname, value);
    }
}
