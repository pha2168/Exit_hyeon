using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public List<SceneAudioSettings> sceneAudioSettings;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var setting in sceneAudioSettings)
        {
            if (setting.sceneName == scene.name)
            {
                bgmAudioSource.volume = setting.bgmVolume;
                sfxAudioSource.volume = setting.sfxVolume;
                break;
            }
        }
    }
}
