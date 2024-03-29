using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField] public AudioMixer audioMixer;
    [SerializeField] public GameObject optionsMenuUI;
    [SerializeField] public GameObject returnToMenu;
    [SerializeField] public Button resumeButton;

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
        returnToMenu.SetActive(true);
        resumeButton.Select();
    }
}
