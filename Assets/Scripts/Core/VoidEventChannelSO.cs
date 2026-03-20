using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/VoidEventChannel")]
public class VoidEventChannelSO : ScriptableObject
{
    public Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}