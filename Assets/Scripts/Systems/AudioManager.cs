using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEventChannelSO sfxChannel;
    [SerializeField] private IntEventChannelSO scoreChannel;
    [SerializeField] private AudioClip cheerClip;

    private void Start()
    {
        sfxChannel.OnAudioPlayRequested += Play3DSound;
        scoreChannel.OnEventRaised += PlayCheer; 
    }

    private void Play3DSound(AudioClip clip, Vector3 position)
    {
        if (clip != null) AudioSource.PlayClipAtPoint(clip, position);
    }

    private void PlayCheer(int score)
    {
        Play3DSound(cheerClip, Camera.main.transform.position);
    }

    private void OnDestroy()
    {
        if (sfxChannel != null) sfxChannel.OnAudioPlayRequested -= Play3DSound;
        if (scoreChannel != null) scoreChannel.OnEventRaised -= PlayCheer;
    }
}