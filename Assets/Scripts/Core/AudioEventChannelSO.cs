using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/AudioEventChannel")]
public class AudioEventChannelSO : ScriptableObject
{
    public Action<AudioClip, Vector3> OnAudioPlayRequested;

    public void RaiseEvent(AudioClip clip, Vector3 position)
    {
        OnAudioPlayRequested?.Invoke(clip, position);
    }
}