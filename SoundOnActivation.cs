using UnityEngine;

namespace AudioSystem
{
    public class SoundOnActivation : MonoBehaviour
    {
        public RandomizedAudioSource audioEmitter;
        public bool silenceOnDeactivate = false;

        private void OnEnable()
        {
            audioEmitter.PlayRandomizedTrack();
        }

        private void OnDisable()
        {
            if (silenceOnDeactivate)
            {
                audioEmitter.OutputSource.Stop();
            }
        }
    }
}
