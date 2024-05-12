using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxPlayer : MonoBehaviour
{
    [SerializeField] GameObject audioPrefab;
    public SoundEffect[] soundEffects;

    // For playing 2d sounds
    public void PlaySound(string soundEffectName)
    {
        for (int i = soundEffects.Length - 1; i > -1; i--)
        {
            if(soundEffectName == soundEffects[i].soundEffectName)
            {
                AudioClip clip = soundEffects[i].soundClips[Random.Range(0, soundEffects[i].soundClips.Length)];
                AudioSource audioSource = Instantiate(audioPrefab, Vector3.zero, Quaternion.identity).GetComponent<AudioSource>();
                audioSource.volume = soundEffects[i].volume;
                audioSource.clip = clip;
                audioSource.Play();

                Destroy(audioSource.gameObject, clip.length);
            }
        }
    }

    // Singleton
    public static SfxPlayer instance;
    public SfxPlayer() { instance = this;  }
}

[System.Serializable]
public class SoundEffect 
{
    public string soundEffectName = "";
    public float volume = 1;
    public AudioClip[] soundClips = null;
}