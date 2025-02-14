using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    private float bgmVolume;
    private float sfxVolume;

    public Slider bgmSlider;  // BGM 슬라이더
    public Slider sfxSlider;  // SFX 슬라이더

    public GameObject AudioSetting;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 인스턴스가 있으면 파괴
            return;
        }

        // AudioSource가 없다면 찾기
        if (bgmAudioSource == null) bgmAudioSource = GetComponent<AudioSource>();
        if (sfxAudioSource == null) sfxAudioSource = GetComponent<AudioSource>();

        // 게임 데이터를 로드 (PlayerPrefs에서 직접 불러오기)
        LoadVolumeSettings();
    }

    private void Start()
    {
        // 씬이 로드될 때마다 BGM 설정
        SceneManager.sceneLoaded += (scene, mode) => PlaySceneBGM(scene.name);

        // 게임 시작 시 볼륨을 설정
        ApplyVolumeSettings();

        // 슬라이더 값 초기화
        if (bgmSlider != null) bgmSlider.value = bgmVolume;
        if (sfxSlider != null) sfxSlider.value = sfxVolume;

        // 슬라이더 값이 바뀔 때마다 저장되도록 설정
        if (bgmSlider != null)
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        if (sfxSlider != null)
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void LoadVolumeSettings()
    {
        // PlayerPrefs에서 직접 값 불러오기
        bgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.5f); // 기본값 0.5
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f); // 기본값 0.5
    }

    private void ApplyVolumeSettings()
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = bgmVolume;
        }

        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = sfxVolume;
        }
    }

    public void SetBGMVolume(float volume)
    {
        bgmVolume = volume;
        if (bgmAudioSource != null)
        {
            bgmAudioSource.volume = bgmVolume;
        }

        // PlayerPrefs에 저장
        PlayerPrefs.SetFloat("BGMVolume", bgmVolume);
        PlayerPrefs.Save();  // 변경 사항을 저장
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
        if (sfxAudioSource != null)
        {
            sfxAudioSource.volume = sfxVolume;
        }

        // PlayerPrefs에 저장
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();  // 변경 사항을 저장
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

    public void AudioSettingOn()
    {
        AudioSetting.SetActive(true);
    }

    public void AudioSettingOff()
    {
        AudioSetting.SetActive(false);
    }
}