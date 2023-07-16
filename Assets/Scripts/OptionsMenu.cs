using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] public GameObject optionsMenuUI;
    [SerializeField] public GameObject pauseMenuUI;

    public void SetVolume(float vol) {
        audioMixer.SetFloat("Volume", vol);
    }

    public void SetQuality(int qualityIndex) {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen) {
        Screen.fullScreen = isFullscreen;
    }

    public void Accept() {
        optionsMenuUI.SetActive(false);
        pauseMenuUI.SetActive(true);
    }
}
