using UnityEngine;

// HOTKEYS
// M            : Enable/Disable audio component
// Middle-click : Enable/Disable audio component [mouse]
// Keyapd +     : Increase volume
// Keypad -     : Decrease volume
// Keypad 0     : Play/Pause audio
// Keypad .     : Stop audio

namespace HeroicFantasy.Options
{
    public class Audio : MonoBehaviour
    {
        [Header("AUDIO")]
        [SerializeField] private AudioSource audioSource; // used to play the audio clip
        [SerializeField] private bool paused; // determine if the audio is paused or not
        [SerializeField] private bool stopped; // determine if the audio is stopped or not

        private void Awake()
        {
            audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
            paused = false;
            stopped = false;
        }

        private void Update()
        {
            AudioSettings();
        }

        private void AudioSettings()
        {
            // Press "M" or "middle mouse button" to enable/disable audio
            if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Mouse2))
            {
                audioSource.enabled = !audioSource.enabled;
            }

            // Press "Keypad +" to increase volume
            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                audioSource.volume += 0.01f;
            }

            // Press "Keypad -" to decrease volume
            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                audioSource.volume -= 0.01f;
            }

            // Press "Keypad 0" to play/pause the audio
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                if (paused == true || stopped == true)
                {
                    audioSource.Play();
                    paused = false;
                    stopped = false;
                }
                else if (paused == false)
                {
                    audioSource.Pause();
                    paused = true;
                }
            }

            // Press "Keypad ." to stop the audio
            if (Input.GetKeyDown(KeyCode.KeypadPeriod))
            {
                if (stopped == false)
                {
                    audioSource.Stop();
                    stopped = true;
                }
            }
        }
    }
}