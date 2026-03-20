using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Events/IntEventChannel")]
public class IntEventChannelSO : ScriptableObject
{
   public Action<int> OnEventRaised;

   public void RaiseEvent(int value)
   {
      OnEventRaised?.Invoke(value);
   }
}
