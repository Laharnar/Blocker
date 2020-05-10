using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Refactor
{
    public static void ValueToWrapper(object instance, ref object value, IWrapperRefactoring wrapper)
    {
        // expand single value -> wrapper
        value = TransitionToWrapper(value, wrapper);

        RealtimeTester.Assert(instance != null, instance, "Instance in test is null.");
        RealtimeTester.Assert(value == null, instance, "Refactoring failed. Old object isn't null.");
        RealtimeTester.Assert(wrapper != null, instance, "Refactoring failed. New object is null.");
        RealtimeTester.Assert(wrapper.Value == value, instance, "Refactoring failed");
    }

    private static object TransitionToWrapper(object value, IWrapperRefactoring wrapper)
    {
        if (value != null)
        {
            wrapper.SetValue(value);
            value = null;
        }

        return value;
    }
    public interface IWrapperRefactoring
    {
        object Value { get; }
        void SetValue(object value);
    }
}
