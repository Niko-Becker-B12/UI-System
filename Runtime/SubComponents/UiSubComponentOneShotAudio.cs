using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


namespace GPUI.SubComponents
{
    [RequireComponent(typeof(AudioSource))]
    [AddComponentMenu("GPUI/Sub Components/One Shot Audio")]
    public class UiSubComponentOneShotAudio : MonoBehaviour
    {
        
        [ShowInInspector]
        protected UiElement Element => this.GetComponent<UiElement>();
        AudioSource audioSource => this.GetComponent<AudioSource>();

        public List<AudioClip> clips = new List<AudioClip>();

        
        public void PlayOneShot(AudioClip clip)
        {
            
            audioSource.PlayOneShot(clip);
            
        }

        public void PlayOneShot(int index)
        {
            
            if(index < 0 || index >= this.clips.Count)
                return;
            
            PlayOneShot(clips[index]);
            
        }
        
    }
}