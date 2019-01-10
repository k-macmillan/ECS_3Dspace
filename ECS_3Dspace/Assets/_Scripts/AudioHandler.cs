using UnityEngine;
using UnityEngine.Audio;

namespace Ships
{
    public class AudioHandler : MonoBehaviour
    {
        public void Start()
        {
            AudioCommon.SetupAudioSource();
        }

        public void OnEnable()
        {
            AudioCommon.AddAllClips();
        }

        public void Update()
        {
            if (!AudioCommon.MusicPlaying())
            {
                AudioCommon.SetSong();
            }
        }
    }

    public static class AudioCommon
    {
        // The music and sound is just a placeholder, though I am considering
        // purchasing the rights to it.

        // Music
        private static AudioSource music;
        private static readonly string[] strClips =
            {
                "_Audio/GameMusic0",
                "_Audio/GameMusic1",
                "_Audio/GameMusic2",
            };
        private static readonly AudioClip[] songs = new AudioClip[strClips.Length];
        private static int lastSongIndex = -1;
        private static int newIndex = -2;

        // Weapons
        private static AudioSource weapon;
        private static AudioClip weaponDU;
        private static readonly string strWeaponDU = "_Audio/WeaponSound0";


        public static bool MusicPlaying()
        {
            return music.isPlaying;
        }

        public static void SetupAudioSource()
        {
            var sources = GameObject.Find("PlayerCamera").gameObject.GetComponents<AudioSource>();
            var masterMixer = Resources.Load<AudioMixer>("_Audio/Master");

            // Music
            music = sources[0];
            music.outputAudioMixerGroup = masterMixer.FindMatchingGroups("Music")[0];

            // Weapon
            weapon = sources[1];
            weapon.outputAudioMixerGroup = masterMixer.FindMatchingGroups("SFX")[0];
            weapon.clip = weaponDU;
        }

        public static void AddAllClips()
        {
            // Songs first
            for (int i = 0; i < strClips.Length; ++i)
            {
                songs[i] = Resources.Load<AudioClip>(strClips[i]);
            }

            // Weapons
            weaponDU = Resources.Load<AudioClip>(strWeaponDU);
        }

        public static void SetSong()
        {
            // Randomly pick song from list of songs
            do
            {
                newIndex = Random.Range(0, songs.Length);
            } while (newIndex == lastSongIndex);

            lastSongIndex = newIndex;
            music.clip = songs[lastSongIndex];
            music.Play();
        }

        public static void FireWeapon()
        {
            weapon.Play();
        }

    }

}