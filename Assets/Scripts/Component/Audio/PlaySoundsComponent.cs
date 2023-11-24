using System;
using UnityEngine;


namespace PixelCrew.Component.Audio{
    public class PlaySoundsComponent : MonoBehaviour
    {
        [SerializeField] private AudioSource _sourse;
        [SerializeField] private AudioData[] _sounds;

        public void Play(string id){
            foreach (var audioData in _sounds){
                if (audioData.Id != id) continue;

                _sourse.PlayOneShot(audioData.Clip);
                break;
            }
        }

        [Serializable]
        public class AudioData{
            [SerializeField] private string _id;
            [SerializeField] private AudioClip _clip;

            public string Id => _id;
            public AudioClip Clip => _clip;
        }
    }

    
}
