using UnityEngine;
using UnityEngine.UI;  // Untuk menggunakan Slider

public class AudioManager : MonoBehaviour
{
    public AudioClip backgroundMusic;  // Audio untuk musik latar
    private AudioSource audioSource;
    public Slider volumeSlider;  // Slider untuk mengontrol volume

    void Awake()
    {
        // Pastikan AudioManager tidak terduplikasi saat pindah scene
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);  // Jangan hancurkan saat pindah scene
        }

        audioSource = GetComponent<AudioSource>();

        // Mainkan musik saat AudioManager pertama kali dibuat
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;  // Musik akan berulang
        audioSource.Play();

        // Set volume awal sesuai dengan nilai slider (default 1)
        volumeSlider.value = audioSource.volume;

        // Tambahkan listener untuk slider agar mengubah volume saat slider digeser
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    // Fungsi untuk mengubah volume berdasarkan nilai slider
    public void ChangeVolume(float value)
    {
        audioSource.volume = value;
    }

    // Fungsi untuk mengganti musik jika diperlukan
    public void ChangeMusic(AudioClip newMusic)
    {
        audioSource.clip = newMusic;
        audioSource.Play();
    }

    // Fungsi untuk menghentikan musik (misalnya saat pause)
    public void StopMusic()
    {
        audioSource.Stop();
    }

    // Fungsi untuk melanjutkan musik (misalnya saat resume)
    public void ResumeMusic()
    {
        audioSource.Play();
    }
}
