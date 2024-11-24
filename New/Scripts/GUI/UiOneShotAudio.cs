using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class UiOneShotAudio : MonoBehaviour
{

    public List<AudioClip> audioClips = new List<AudioClip>();

    AudioSource audioSource;

    public AudioMixerGroup audioMixer;

    private void Start()
    {

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.outputAudioMixerGroup = audioMixer;
        audioSource.volume = .5f;
        audioSource.pitch = Random.Range(.75f, 1.25f);

    }

    public void PlaySound(int index)
    {

        if (index > audioClips.Count - 1 || audioClips[index] == null)
            return;

        audioSource.pitch = Random.Range(.75f, 1.25f);
        audioSource.PlayOneShot(audioClips[index]);

    }

}