using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    [SerializeField] private AudioSource _audioButtonPref;
    [SerializeField] private AudioSource _vibration;
    [SerializeField] public AudioSource _bgMusic;
    public static AudioSource audioButton;
    public static AudioSource bgMusic;
    public static AudioSource vibration;
    public static void PlayButton()
    {
        if (!AudioSaveController.GetIsAudioPlay()) return;
        audioButton.Play();
    }
    public static void PlayVibration()
    {
        if (!AudioSaveController.GetIsAudioPlay()) return;
        vibration.Play();
    }
    public static void SetVolume()
    {
        audioButton.volume = AudioSaveController.GetAudioVolume();
        bgMusic.volume = AudioSaveController.GetAudioVolume() / 2;
        vibration.volume = AudioSaveController.GetAudioVolume();
    }
    public static void SetBgMUsic()
    {
        if (!AudioSaveController.GetIsAudioPlay()) bgMusic.Stop();
        else bgMusic.Play();
    }
    private void Start()
    {
        audioButton = _audioButtonPref;
        bgMusic = _bgMusic;
        vibration = _vibration;
        SetVolume();
        SetBgMUsic();
    }
}
