using UnityEngine;
using UnityEngine.UI;

public class OptionsMenuManager : MonoBehaviour
{
    [Header("Set Slider")]
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private GameObject _mainMenuUI;
    [SerializeField] private GameObject _optionsUI;

    private void Start()
    {
        //Adds listener for Volume Slider
        _soundSlider.onValueChanged.AddListener(val => AudioManager.instance.ChangeMasterVolume(val));
    }

    //Toggle Music
    public void ToggleMusic()
    {
        AudioManager.instance.ToggleMusic();
    }

    //Toggle Effect
    public void ToggleEffects()
    {
        AudioManager.instance.ToggleEffects();
    }

    //Go Fullscreen or Windowed
    public void ToggleFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        Debug.Log("Is Fullscreen:" + isFullscreen);
    }

    //Closes Options Menu
    public void CloseOptions()
    {
        _mainMenuUI.SetActive(true);
        _optionsUI.SetActive(false);
    }
}
