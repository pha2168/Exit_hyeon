using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingControl : MonoBehaviour
{
    public Slider bgmSlider;
    public Slider sfxSlider;
    public AudioSource bgmAudioSource; // BGM 오디오 소스 추가
    public AudioSource sfxAudioSource; // SFX 오디오 소스 추가

    private Data gameData;

    private void Start()
    {
        Datamanager.Instance.LoadGameData();

        // 슬라이더 초기값 설정
        bgmSlider.value = gameData.BGMVolume;
        sfxSlider.value = gameData.SFXVolume;

        // 슬라이더 값 변경 이벤트 등록
        bgmSlider.onValueChanged.AddListener((value) => {
            gameData.BGMVolume = value;
            bgmAudioSource.volume = value; // BGM 오디오 소스 볼륨 조정
        });
        sfxSlider.onValueChanged.AddListener((value) => {
            gameData.SFXVolume = value;
            sfxAudioSource.volume = value; // SFX 오디오 소스 볼륨 조정
        });

        // 오디오 소스 초기 볼륨 설정
        bgmAudioSource.volume = gameData.BGMVolume;
        sfxAudioSource.volume = gameData.SFXVolume;
    }

    public void SaveSettings()
    {
        Datamanager.Instance.SaveGameData();
    }
}
