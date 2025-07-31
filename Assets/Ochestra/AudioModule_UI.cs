using UnityEngine;
using UnityEngine.UI;
using Capsule.Core;
using Capsule.UI;
using System.Collections;

namespace Capsule.Ochestra
{
    public class AudioModule_UI : GameplayModule_UIBase
    {
        [Header("Audio Sources")]
        [SerializeField] private AudioSource musicSource1;
        [SerializeField] private AudioSource musicSource2;
        [SerializeField] private AudioSource ambientSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("UI Sliders")]
        [SerializeField] private Slider musicSlider;
        [SerializeField] private Slider ambientSlider;
        [SerializeField] private Slider sfxSlider;

        [Header("Volume Settings")]
        private float musicVolume;
        private float ambientVolume;
        private float sfxVolume;

        private bool isPlayingMusic1 = true;

        private const string MusicKey = "volume_music";
        private const string AmbientKey = "volume_ambient";
        private const string SFXKey = "volume_sfx";

        public override void InitializeUI(int order)
        {
            LoadVolumeSettings();

            // Hook sliders
            if (musicSlider != null) musicSlider.onValueChanged.AddListener(SetMusicVolume);
            if (ambientSlider != null) ambientSlider.onValueChanged.AddListener(SetAmbientVolume);
            if (sfxSlider != null) sfxSlider.onValueChanged.AddListener(SetSFXVolume);

            ApplyVolumeSettings();
        }

        public override void OnEnter()
        {
            rootCanvas.enabled = true;
        }

        public override void OnExit()
        {
            rootCanvas.enabled = false;
            SaveVolumeSettings();
        }

        #region Volume Controls
        public void SetMusicVolume(float volume)
        {
            musicVolume = volume;
            musicSource1.volume = volume;
            musicSource2.volume = volume;
            PlayerPrefs.SetFloat(MusicKey, volume); // ✅ use correct key
        }

        public void SetSFXVolume(float volume)
        {
            sfxVolume = volume;
            sfxSource.volume = volume;
            PlayerPrefs.SetFloat(SFXKey, volume); // ✅
        }

        public void SetAmbientVolume(float volume)
        {
            ambientVolume = volume;
            ambientSource.volume = volume;
            PlayerPrefs.SetFloat(AmbientKey, volume); // ✅
        }



        private void ApplyVolumeSettings()
        {
            SetMusicVolume(musicVolume);
            SetAmbientVolume(ambientVolume);
            SetSFXVolume(sfxVolume);

            // Set UI sliders if not null
            if (musicSlider != null) musicSlider.value = musicVolume;
            if (ambientSlider != null) ambientSlider.value = ambientVolume;
            if (sfxSlider != null) sfxSlider.value = sfxVolume;
        }

        private void LoadVolumeSettings()
        {
            musicVolume = PlayerPrefs.GetFloat(MusicKey, 1f);
            ambientVolume = PlayerPrefs.GetFloat(AmbientKey, 1f);
            sfxVolume = PlayerPrefs.GetFloat(SFXKey, 1f);
        }

        private void SaveVolumeSettings()
        {
            PlayerPrefs.SetFloat(MusicKey, musicVolume);
            PlayerPrefs.SetFloat(AmbientKey, ambientVolume);
            PlayerPrefs.SetFloat(SFXKey, sfxVolume);
            PlayerPrefs.Save();
        }
        #endregion

        #region Playback
        public void PlayMusic(AudioClip clip, float fade = 1f)
        {
            StartCoroutine(Crossfade(clip, fade));
        }

        public void PlayAmbient(AudioClip clip, bool loop = true)
        {
            ambientSource.clip = clip;
            ambientSource.loop = loop;
            ambientSource.Play();
        }

        public void StopAmbient() => ambientSource.Stop();

        public void PlaySFX(AudioClip clip) => sfxSource.PlayOneShot(clip);
        #endregion

        private IEnumerator Crossfade(AudioClip newTrack, float duration)
        {
            AudioSource active = isPlayingMusic1 ? musicSource1 : musicSource2;
            AudioSource next = isPlayingMusic1 ? musicSource2 : musicSource1;

            next.clip = newTrack;
            next.volume = 0f;
            next.loop = true;
            next.Play();

            float timer = 0f;
            while (timer < duration)
            {
                float t = timer / duration;
                active.volume = Mathf.Lerp(musicVolume, 0f, t);
                next.volume = Mathf.Lerp(0f, musicVolume, t);
                timer += Time.deltaTime;
                yield return null;
            }

            active.Stop();                      // ✅ Ensure old track is stopped
            active.clip = null;                // (Optional: cleanup)
            active.volume = musicVolume;       // Reset for future use
            isPlayingMusic1 = !isPlayingMusic1;
        }

    }
}

