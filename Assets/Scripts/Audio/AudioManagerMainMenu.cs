using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMainMenu : MonoBehaviour
{
    
    [SerializeField] private AudioSource _audioButtonPref;
    [SerializeField] public AudioSource _bgMusic;
    public static AudioSource audioButton;
    public static AudioSource bgMusic;
    public static void PlayButton()
    {
        if (!AudioSaveController.GetIsAudioPlay()) return;
        audioButton.Play();
    }
    public static void SetVolume()
    {
        audioButton.volume = AudioSaveController.GetAudioVolume();
        bgMusic.volume = AudioSaveController.GetAudioVolume() / 2;
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

        SetVolume();
        SetBgMUsic();
    }
}
