using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Music
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager instance { get; private set; }
        private AudioSource musicSource;
        

        private void Awake()
        {
            instance = this;

            // Create a new GameObject to play the music
            GameObject musicPlayer = new GameObject("MusicPlayer");
            musicPlayer.transform.SetParent(gameObject.transform);
            musicSource = musicPlayer.AddComponent<AudioSource>();
        }

        // Function to play a sound by name
        public void PlaySound(string soundName)
        {
           
            // Find the AudioClip with the given name
            AudioClip soundClip = Resources.Load<AudioClip>($"sfx general/{soundName}");

            if (soundClip != null)
            {
                // Create a new GameObject to play the sound
                GameObject soundObject = new GameObject("SoundPlayer");
                soundObject.transform.SetParent(gameObject.transform);
                AudioSource audioSource = soundObject.AddComponent<AudioSource>();

                // Set the AudioClip and play the sound
                audioSource.clip = soundClip;
                audioSource.Play();
                if (audioSource.isPlaying)
                    Debug.Log("The music is playing");

                // Destroy the GameObject after the sound has finished playing
                Destroy(soundObject, soundClip.length);
            }
            else
            {
                Debug.LogError("Sound not found: " + soundName);
            }
        }

        public void StartMusic(string musicName)
        {
            // Find the AudioClip with the given name
            AudioClip soundClip = Resources.Load<AudioClip>($"Soundtrack/{musicName}");

            if (soundClip != null)
            {
                if (musicSource.isPlaying)
                    musicSource.Stop();

                // Set the AudioClip and play the sound
                musicSource.clip = soundClip;
                musicSource.loop = true;
                musicSource.Play();

            }
            else
            {
                Debug.LogError("Sound not found: " + musicName);
            }
        }

        public void EndMusic()
        {
            if (musicSource.isPlaying)
                musicSource.Stop();
            else
                Debug.LogWarning("Music is not playing");
        }



        
    }
}

