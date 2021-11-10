// This code is part of the Fungus library (http://fungusgames.com) maintained by Chris Gregan (http://twitter.com/gofungus).
// It is released for free under the MIT open source license (https://github.com/snozbot/fungus/blob/master/LICENSE)

using UnityEngine;

namespace Fungus
{
    /// <summary>
    /// Music manager which provides basic music and sound effect functionality.
    /// Music playback persists across scene loads.
    /// </summary>
    //[RequireComponent(typeof(AudioSource))]
    public class MusicManager : MonoBehaviour
    {
        protected AudioSource audioSourceMusic;
        protected AudioSource audioSourceAmbiance;
        protected AudioSource audioSourceSoundEffect;
        protected AudioSource audioSourceVoice;

        void Reset()
        {
            int audioSourceCount = this.GetComponents<AudioSource>().Length;
            for (int i = 0; i < 4 - audioSourceCount; i++)
                gameObject.AddComponent<AudioSource>();

        }

        protected virtual void Awake()
        {
            Reset();
            AudioSource[] audioSources = GetComponents<AudioSource>();
            audioSourceMusic = audioSources[0];
            audioSourceAmbiance = audioSources[1];
            audioSourceSoundEffect = audioSources[2];
            audioSourceVoice = audioSources[3];
        }

        protected virtual void Start()
        {
            audioSourceMusic.playOnAwake = false;
            audioSourceMusic.loop = true;
        }

        #region Public members

        /// <summary>
        /// Plays game music using an audio clip.
        /// One music clip may be played at a time.
        /// </summary>
        public void PlayMusic(AudioClip musicClip, bool loop, float fadeDuration, float atTime)
        {
            if (audioSourceMusic == null || audioSourceMusic.clip == musicClip)
            {
                return;
            }

            if (Mathf.Approximately(fadeDuration, 0f))
            {
                audioSourceMusic.clip = musicClip;
                audioSourceMusic.loop = loop;
                audioSourceMusic.time = atTime;  // May be inaccurate if the audio source is compressed http://docs.unity3d.com/ScriptReference/AudioSource-time.html BK
                audioSourceMusic.Play();
            }
            else
            {
                float startVolume = audioSourceMusic.volume;

                LeanTween.value(gameObject, startVolume, 0f, fadeDuration)
                    .setOnUpdate((v) => {
                        // Fade out current music
                        audioSourceMusic.volume = v;
                    }).setOnComplete(() => {
                        // Play new music
                        audioSourceMusic.volume = startVolume;
                        audioSourceMusic.clip = musicClip;
                        audioSourceMusic.loop = loop;
                        audioSourceMusic.time = atTime;  // May be inaccurate if the audio source is compressed http://docs.unity3d.com/ScriptReference/AudioSource-time.html BK
                        audioSourceMusic.Play();
                    });
            }
        }

        /// <summary>
        /// Plays a sound effect once, at the specified volume.
        /// </summary>
        /// <param name="soundClip">The sound effect clip to play.</param>
        /// <param name="volume">The volume level of the sound effect.</param>
        public virtual void PlaySound(AudioClip soundClip, float volume)
        {
            audioSourceSoundEffect.PlayOneShot(soundClip, volume);
        }

        /// <summary>
        /// Plays a sound effect with optional looping values, at the specified volume.
        /// ���q�]�w�L�ġA�Ъ�����setting���վ�
        /// </summary>
        /// <param name="soundClip">The sound effect clip to play.</param>
        /// <param name="loop">If the audioclip should loop or not.</param>
        /// <param name="volume">The volume level of the sound effect.</param>
        public virtual void PlayAmbianceSound(AudioClip soundClip, bool loop, float volume)
        {
            audioSourceAmbiance.loop = loop;
            audioSourceAmbiance.clip = soundClip;
            //audioSourceAmbiance.volume = volume;
            audioSourceAmbiance.Play();
        }

        /// <summary>
        /// Plays a sound effect with optional looping values, at the specified volume.
        /// ���q�]�w�L�ġA�Ъ�����setting���վ�
        /// </summary>
        /// <param name="soundClip">The sound effect clip to play.</param>
        /// <param name="loop">If the audioclip should loop or not.</param>
        /// <param name="volume">The volume level of the sound effect.</param>
        public virtual void PlayVoiceSound(AudioClip soundClip, bool loop, float volume)
        {
            audioSourceVoice.loop = loop;
            audioSourceVoice.clip = soundClip;
            //audioSourceAmbiance.volume = volume;
            audioSourceVoice.Play();
        }

        /// <summary>
        /// Shifts the game music pitch to required value over a period of time.
        /// </summary>
        /// <param name="pitch">The new music pitch value.</param>
        /// <param name="duration">The length of time in seconds needed to complete the pitch change.</param>
        /// <param name="onComplete">A delegate method to call when the pitch shift has completed.</param>
        public virtual void SetAudioPitch(float pitch, float duration, System.Action onComplete)
        {
            if (Mathf.Approximately(duration, 0f))
            {
                audioSourceMusic.pitch = pitch;
                audioSourceAmbiance.pitch = pitch;
                if (onComplete != null)
                {
                    onComplete();
                }
                return;
            }

            LeanTween.value(gameObject,
                audioSourceMusic.pitch,
                pitch,
                duration).setOnUpdate((p) =>
                {
                    audioSourceMusic.pitch = p;
                    audioSourceAmbiance.pitch = p;
                }).setOnComplete(() =>
                {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
        }

        /// <summary>
        /// Fades the game music volume to required level over a period of time.
        /// </summary>
        /// <param name="volume">The new music volume value [0..1]</param>
        /// <param name="duration">The length of time in seconds needed to complete the volume change.</param>
        /// <param name="onComplete">Delegate function to call when fade completes.</param>
        public virtual void SetAudioVolume(float volume, float duration, System.Action onComplete)
        {
            if (Mathf.Approximately(duration, 0f))
            {
                if (onComplete != null)
                {
                    onComplete();
                }
                audioSourceMusic.volume = volume;
                return;
            }

            LeanTween.value(gameObject,
                audioSourceMusic.volume,
                volume,
                duration).setOnUpdate((v) => {
                    audioSourceMusic.volume = v;
                }).setOnComplete(() => {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
        }

        /// <summary>
        /// Fades the game music volume to required level over a period of time.
        /// </summary>
        /// <param name="volume">The new music volume value [0..1]</param>
        /// <param name="duration">The length of time in seconds needed to complete the volume change.</param>
        /// <param name="onComplete">Delegate function to call when fade completes.</param>
        public virtual void SetAudioAmbianceVolume(float volume, float duration, System.Action onComplete)
        {
            if (Mathf.Approximately(duration, 0f))
            {
                if (onComplete != null)
                {
                    onComplete();
                }
                audioSourceAmbiance.volume = volume;
                return;
            }

            LeanTween.value(gameObject,
                audioSourceAmbiance.volume,
                volume,
                duration).setOnUpdate((v) => {
                    audioSourceAmbiance.volume = v;
                }).setOnComplete(() => {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
        }

        public virtual void SetAudioVoiceVolume(float volume, float duration, System.Action onComplete)
        {
            if (Mathf.Approximately(duration, 0f))
            {
                if (onComplete != null)
                {
                    onComplete();
                }
                audioSourceVoice.volume = volume;
                return;
            }

            LeanTween.value(gameObject,
                audioSourceVoice.volume,
                volume,
                duration).setOnUpdate((v) => {
                    audioSourceVoice.volume = v;
                }).setOnComplete(() => {
                    if (onComplete != null)
                    {
                        onComplete();
                    }
                });
        }

        /// <summary>
        /// Stops playing game music.
        /// </summary>
        public virtual void StopMusic()
        {
            audioSourceMusic.Stop();
            audioSourceMusic.clip = null;
        }


        /// <summary>
        /// Pauce playing game music.
        /// </summary>
        public virtual void PauceMusic()
        {
            audioSourceMusic.Pause();
        }

        /// <summary>
        /// UnPause playing game music.
        /// </summary>
        public virtual void resumeMusic()
        {
            audioSourceMusic.UnPause();
        }

        /// <summary>
        /// Stops playing game ambiance.
        /// </summary>
        public virtual void StopAmbiance()
        {
            audioSourceAmbiance.Stop();
            audioSourceAmbiance.clip = null;
        }

        /// <summary>
        /// Pauce playing game ambiance.
        /// </summary>
        public virtual void PauceAmbiance()
        {
            audioSourceAmbiance.Pause();
        }

        /// <summary>
        /// UnPause playing game ambiance.
        /// </summary>
        public virtual void resumeAmbiance()
        {
            audioSourceAmbiance.UnPause();
        }

        /// <summary>
        /// Stops playing game voice.
        /// </summary>
        public virtual void StopVoice()
        {
            audioSourceVoice.Stop();
            audioSourceVoice.clip = null;
        }

        /// <summary>
        /// Pauce playing game voice.
        /// </summary>
        public virtual void PauceVoice()
        {
            audioSourceVoice.Pause();
        }

        /// <summary>
        /// UnPause playing game voice.
        /// </summary>
        public virtual void resumeVoice()
        {
            audioSourceVoice.UnPause();
        }

        #endregion
    }
}