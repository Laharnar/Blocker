using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseClick : MonoBehaviour
{
    public BoolVarValue isLeftMouseDown;
    public BoolVarValue isLeftMouse;
    public BoolVarValue isLeftMouseUp;

    public UnityEvent onLeftDown;
    public UnityEvent onLeftHold;
    public UnityEvent onLeftUp;

    // Update is called once per frame
    void Update()
    {
        isLeftMouseDown.Value = Input.GetKeyDown(KeyCode.Mouse0);
        isLeftMouse.Value = Input.GetKey(KeyCode.Mouse0);
        isLeftMouseUp.Value = Input.GetKeyUp(KeyCode.Mouse0);

        onLeftDown?.Invoke();
        onLeftHold?.Invoke();
        onLeftUp?.Invoke();
    }
}
