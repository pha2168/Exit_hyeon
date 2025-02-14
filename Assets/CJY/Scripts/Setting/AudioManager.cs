using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioMixer audioMixer; // 오디오 믹서 추가
    public AudioSource bgmAudioSource; // BGM만 AudioSource 유지

    public Slider bgmSlider;
    public Slider sfxSlider;
    public GameObject AudioSetting;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadVolumeSettings();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += (scene, mode) => PlaySceneBGM(scene.name);

        ApplyVolumeSettings();

        if (bgmSlider != null) bgmSlider.value = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        if (sfxSlider != null) sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        if (bgmSlider != null) bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void LoadVolumeSettings()
    {
        float bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
        float sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);

        SetBGMVolume(bgmVolume);
        SetSFXVolume(sfxVolume);
    }

    private void ApplyVolumeSettings()
    {
        if (audioMixer != null)
        {
            audioMixer.SetFloat("BGMVolume", Mathf.Log10(PlayerPrefs.GetFloat("BGMVolume", 0.5f)) * 20);
            audioMixer.SetFloat("SFXVolume", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolume", 0.5f)) * 20);
        }
    }

    public void SetBGMVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        audioMixer.SetFloat("BGMVolume", dB);
        PlayerPrefs.SetFloat("BGMVolume", volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        float dB = Mathf.Log10(Mathf.Max(volume, 0.0001f)) * 20;
        audioMixer.SetFloat("SFXVolume", dB);
        PlayerPrefs.SetFloat("SFXVolume", volume);
        PlayerPrefs.Save();
    }

    private void PlaySceneBGM(string sceneName)
    {
        AudioClip newBGM = Resources.Load<AudioClip>($"Audio/BGM/{sceneName}");
        if (newBGM != null)
        {
            bgmAudioSource.clip = newBGM;
            bgmAudioSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, PlayerPrefs.GetFloat("SFXVolume", 0.5f));
        }
    }

    public void AudioSettingOn() => AudioSetting.SetActive(true);
    public void AudioSettingOff() => AudioSetting.SetActive(false);
}
