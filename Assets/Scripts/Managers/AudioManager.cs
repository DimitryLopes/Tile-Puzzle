using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private const int MAXIMUM_SFX_SOURCES_ALLOWED = 6;
    private const string MIXER_GROUP_VOLUME_PARAMETER = "_Volume";

    public static AudioManager Instance { get; private set; }

    [SerializeField]
    private AudioMixingSettings audioSettings;
    [SerializeField]
    private AudioDataBase audioDataBase;
    [SerializeField]
    private AudioSource audioSourcePrefab;

    private AudioSource bgmSource;
    private float sfxVolume = 0.25f;
    private float bgmVolume = 0.25f;

    public float BGMVolume => bgmVolume;
    public float SFXVolume => sfxVolume;

    private List<AudioSource> sfxSources = new();

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        audioDataBase.Setup();
    }

    public void OnEnable()
    {
        CreateBGMSource();
    }

    public void PlaySFX(AudioKey clipKey, bool canHaveMultiple = true, Vector3 position = new())
    {
        AudioSource source = GetAvailableSFXSource(clipKey, canHaveMultiple);
        if (source != null)
        {
            AudioInfo info = audioDataBase.GetAudioInfo(clipKey);
            source.clip = info.AudioClip;
            source.volume = sfxVolume;
            source.Play();

            if (info.Origin == SoundOrigin.World)
            {
                source.transform.position = position;
            }
            else
            {
                source.transform.position = Camera.main.transform.position;
            }
        }
    }

    public void StopBGM()
    {
        FadeOutBGM();
    }

    public void PlayBGM(AudioKey key)
    {
        AudioInfo info = audioDataBase.GetAudioInfo(key);

        if (bgmSource.clip == info.AudioClip) return;

        bgmSource.clip = info.AudioClip;
        bgmSource.Play();
        FadeInBGM();
    }

    private void FadeOutBGM(Action onFadeOutComplete = null)
    {
        TweenAnimationData data = new TweenAnimationData(bgmSource.gameObject, 0, -80, audioSettings.AudioFadeDuration,
            ChangeBGMMixerVolume, onFadeOutComplete);
        TweenUtils.PlayTween(data);
    }

    private void FadeInBGM(Action onFadeInComplete = null)
    {
        TweenAnimationData data = new TweenAnimationData(bgmSource.gameObject, -80, 0, audioSettings.AudioFadeDuration,
            ChangeBGMMixerVolume, onFadeInComplete);
        TweenUtils.PlayTween(data);
    }

    public void ChangeBGMMixerVolume(float value)
    {
        audioSettings.AudioMixer.SetFloat(audioSettings.BGMGroup + MIXER_GROUP_VOLUME_PARAMETER, value);
    }

    public void ChangeSFXMixerVolume(float val)
    {
        audioSettings.AudioMixer.SetFloat(audioSettings.SFXGroup + MIXER_GROUP_VOLUME_PARAMETER, val);
    }

    private void CreateBGMSource()
    {
        AudioSource bgmSource = GetNewAudioSource();
        bgmSource.transform.position = Camera.main.transform.position;
        this.bgmSource = bgmSource;
        bgmSource.volume = bgmVolume;
        bgmSource.loop = true;
        bgmSource.outputAudioMixerGroup = audioSettings.BGMGroup;
    }

    private AudioSource GetAvailableSFXSource(AudioKey key, bool canHaveMultiple = true)
    {
        AudioSource source = null;
        foreach (var instantiatedSource in sfxSources)
        {
            if (!instantiatedSource.isPlaying)
            {
                source = instantiatedSource;
            }
            else if (!canHaveMultiple)
            {
                if (instantiatedSource.clip == audioDataBase.GetClip(key))
                {
                    return null;
                }
            }
        }

        if (sfxSources.Count < MAXIMUM_SFX_SOURCES_ALLOWED)
        {
            source = GetNewAudioSource();
            sfxSources.Add(source);
            source.outputAudioMixerGroup = audioSettings.SFXGroup;
            source.loop = false;
            return source;
        }
        return source;
    }

    private AudioSource GetNewAudioSource()
    {
        AudioSource newSource = Instantiate(audioSourcePrefab, transform);
        return newSource;
    }

    public void ChangeSFXVolume(float volume)
    {
        sfxVolume = volume;
        foreach (var source in sfxSources)
        {
            source.volume = volume;
        }
    }

    public void ChangeBGMVolume(float volume)
    {
        bgmVolume = volume;
        bgmSource.volume = volume;
    }
}

public enum SoundOrigin
{
    BGM,
    World,
    UI
}

public enum SoundType
{
    SFX,
    BGM
}