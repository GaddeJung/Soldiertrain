using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CharSound : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource charSound;
    public AudioClip[] charlist;
    public static CharSound instance;

    public static float CharSoundVolume = 1f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void CharSoundPlay(string charName, AudioClip clip)
    {
        GameObject go = new GameObject(charName + "Sound");
        AudioSource audiosource = go.AddComponent<AudioSource>();
        audiosource.outputAudioMixerGroup = mixer.FindMatchingGroups("CharSound")[0];
        audiosource.clip = clip;
        audiosource.volume = CharSoundVolume;
        audiosource.Play();

        Destroy(go, clip.length);
    }
    public void SetCharVolume(float volume)
    {
        mixer.SetFloat("CharSound", Mathf.Log10(volume) * 20);
    }

}
