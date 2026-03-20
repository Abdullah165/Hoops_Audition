using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/FloatEventChannel")]
public class FloatEventChannelSO : ScriptableObject
{
    public Action<float> OnEventRaised;

    public void RaiseEvent(float value)
    {
        OnEventRaised?.Invoke(value);
    }
}
