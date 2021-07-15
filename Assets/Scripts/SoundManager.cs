using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour {
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _onMovementSoundClip;
    [SerializeField] private AudioClip _onWinSoundClip;
    [SerializeField] private AudioClip _onLoseSoundClip;
    [SerializeField] private AudioClip _onStarAcquisition;

    [SerializeField] private InteractableItem[] _stars;
    [SerializeField] private InteractableItem[] _boxes;
    [SerializeField] private InteractableItem _finishLine;
    [SerializeField] private RbMovement _player;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _finishLine.CollisionWithPlayer.AddListener(PlayWinSound);
        _player.OnMovementStart.AddListener(PlayMovementSound);
        AddListeners(_stars, PlayStartAcquisitionSound);
        AddListeners(_boxes, PlayLoseSound);
    }

    private void PlayMovementSound()
    {
        
        _audioSource.PlayOneShot(_onMovementSoundClip);
    }
    private void PlayWinSound()
    {
        _audioSource.PlayOneShot(_onWinSoundClip);
    }
    private void PlayLoseSound()
    {
        _audioSource.PlayOneShot(_onLoseSoundClip);
    }
    private void PlayStartAcquisitionSound()
    {
        _audioSource.PlayOneShot(_onStarAcquisition);
    }

    private void AddListeners(InteractableItem[] items, UnityAction action)
    {
        foreach (var item in items)
        {
            item.CollisionWithPlayer.AddListener(action);
        }
    }
}
