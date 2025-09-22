using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public AudioSource player;
    public AudioClip[] audios;
    public AudioMixer mixer;

    public Slider VolSlider;

    public void PlayerAudio(int i)
    {
        player.clip = audios[i];
        player.Play();
    }
    public void MudarVolume(int i)
    {
        if (i == 0)
        {
            mixer.SetFloat("SFXvol", VolSlider.value);
        }

    }
    public void SalvarVolume()
    {
        PlayerPrefs.SetFloat("SFXvol", VolSlider.value);
        PlayerPrefs.Save();
    }
}
