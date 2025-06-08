using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Cần thiết để lắng nghe sự kiện tải scene

public class AudioSettings : MonoBehaviour
{
    public AudioMixer audioMixer; // Kéo AudioMixer vào đây từ Inspector

    private Slider musicSlider; 
    private Slider fxSlider;

    private const string MUSIC_VOLUME_KEY = "Volume_Music";
    private const string FX_VOLUME_KEY = "Volume_Fx";


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
        Debug.Log($"AudioSettings: Scene {scene.name} loaded. Re-initializing UI references.");
        InitializeSliders();
    }

    private void Start()
    {
        LoadAndApplyVolumes();
    }

    private void InitializeSliders()
    {
        GameObject musicSliderObj = GameObject.FindWithTag("MusicSlider"); 
        GameObject fxSliderObj = GameObject.FindWithTag("FxSlider"); 

        if (musicSliderObj != null)
        {
            musicSlider = musicSliderObj.GetComponent<Slider>();
            if (musicSlider != null)
            {

                musicSlider.onValueChanged.RemoveAllListeners(); 

                musicSlider.onValueChanged.AddListener(SetMusicVolume);
                Debug.Log("AudioSettings: Music Slider found and assigned.");
            }
        }
        else
        {
            Debug.LogWarning("AudioSettings: Music Slider GameObject with tag 'MusicSlider' not found in current scene!");
        }

        if (fxSliderObj != null)
        {
            fxSlider = fxSliderObj.GetComponent<Slider>();
            if (fxSlider != null)
            {
                fxSlider.onValueChanged.RemoveAllListeners();
                fxSlider.onValueChanged.AddListener(SetFXVolume);
                Debug.Log("AudioSettings: FX Slider found and assigned.");
            }
        }
        else
        {
            Debug.LogWarning("AudioSettings: FX Slider GameObject with tag 'FxSlider' not found in current scene!");
        }

        LoadAndApplyVolumes();
    }

    private void LoadAndApplyVolumes()
    {
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 0.5f);
        float fxVolume = PlayerPrefs.GetFloat(FX_VOLUME_KEY, 0.5f);

        if (musicSlider != null)
        {
            musicSlider.value = musicVolume;
        }
        if (fxSlider != null)
        {
            fxSlider.value = fxVolume;
        }

        SetMusicVolume(musicVolume); 
        SetFXVolume(fxVolume);
    }

    public void SetMusicVolume(float value)
    {

        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, value); 
        audioMixer.SetFloat("Volume_Music", Mathf.Log10(value) * 20); 
    }

    public void SetFXVolume(float value)
    {
        PlayerPrefs.SetFloat(FX_VOLUME_KEY, value); 
        audioMixer.SetFloat("Volume_Fx", Mathf.Log10(value) * 20);

    }
}