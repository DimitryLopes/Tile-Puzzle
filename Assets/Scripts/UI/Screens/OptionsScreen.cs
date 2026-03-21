using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : UIScreen<OptionsScreenController>
{
    [SerializeField]
    private Slider sfxSlider;
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Button backButton;

    private AudioManager audioManager;

    private void Start()
    {
        backButton.onClick.AddListener(OnButtonClicked);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
        bgmSlider.onValueChanged.AddListener(ChangeBGMVolume);
    }
    protected override void OnBeforeShow()
    {
        base.OnBeforeShow();
        audioManager = AudioManager.Instance;
        sfxSlider.value = audioManager.SFXVolume;
        bgmSlider.value = audioManager.BGMVolume;
    }

    public void ChangeBGMVolume(float volume)
    {
        audioManager.ChangeBGMVolume(volume);
    }

    public void ChangeSFXVolume(float volume)
    {
        audioManager.ChangeSFXVolume(volume);
    }

    public void OnButtonClicked()
    {
        Controller.OnBackButtonClicked.Invoke();
        PlayReturnButtonSFX();
    }
}
