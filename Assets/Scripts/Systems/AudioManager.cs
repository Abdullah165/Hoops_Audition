using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioEventChannelSO sfxChannel;
    [SerializeField] private IntEventChannelSO scoreChannel;
    [SerializeField] private VoidEventChannelSO ballTouchGroundChannel;
    [SerializeField] private VoidEventChannelSO ballTouchRimChannel;
    [SerializeField] private VoidEventChannelSO ballTouchBackboardChannel;
    [SerializeField] private AudioClip cheerClip;
    [SerializeField] private AudioClip[] ballTriggerGroundClip;
    [SerializeField] private AudioClip[] ballHitRimClip;
    [SerializeField] private AudioClip[] ballHitBackBoardClip;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        sfxChannel.OnAudioPlayRequested += Play3DSound;
        scoreChannel.OnEventRaised += PlayCheer;

        ballTouchGroundChannel.OnEventRaised += PlayBallTriggerGroundSound;
        ballTouchRimChannel.OnEventRaised += PlayRimHitSound;
        ballTouchBackboardChannel.OnEventRaised += PlayBackBoardSound;
    }

    private void PlayBackBoardSound()
    {
        if (ballHitBackBoardClip != null)
        {
            audioSource.PlayOneShot(ballHitBackBoardClip[UnityEngine.Random.Range(0,ballHitBackBoardClip.Length)]);
        }
    }

    private void PlayRimHitSound()
    {
        if (ballHitRimClip != null)
        {
            audioSource.PlayOneShot(ballHitRimClip[UnityEngine.Random.Range(0, ballHitRimClip.Length)]);
        }
    }

    private void PlayBallTriggerGroundSound()
    {
        if (ballTriggerGroundClip != null)
        {
            audioSource.PlayOneShot(ballTriggerGroundClip[UnityEngine.Random.Range(0, ballTriggerGroundClip.Length)]);
        }
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
        ballTouchGroundChannel.OnEventRaised -= PlayBallTriggerGroundSound;
        ballTouchRimChannel.OnEventRaised -= PlayRimHitSound;
    }
}