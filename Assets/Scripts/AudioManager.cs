using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private float volume = 0.5f;
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI volumeTxt;
    [SerializeField] private AudioClip menuMusicClip;
    [SerializeField] private AudioClip gameMusicClip;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
        volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
        volumeTxt = GameObject.Find("VolumeTxt").GetComponent<TextMeshProUGUI>();
    }
    public void ChangeVolume()
    {
        volume = volumeSlider.value;
        volumeTxt.text = string.Format("{0:00}", volume * 100);
        backgroundMusic.volume = volume;

        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void PlayMenuMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = menuMusicClip;
        backgroundMusic.Play();
    }

    public void PlayGameMusic()
    {
        backgroundMusic.Stop();
        backgroundMusic.clip = gameMusicClip;
        backgroundMusic.Play();
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Nouvelle scène chargée : " + scene.name);
        if (scene.name == "MainMenu")
        {
            volumeSlider = GameObject.Find("VolumeSlider").GetComponent<Slider>();
            volumeTxt = GameObject.Find("VolumeTxt").GetComponent<TextMeshProUGUI>();
            if (PlayerPrefs.HasKey("Volume"))
            {
                volume = PlayerPrefs.GetFloat("Volume");
            }
            volumeSlider.value = volume;
            PlayMenuMusic();
            ChangeVolume();
        }
        else
        {
            PlayGameMusic();
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
