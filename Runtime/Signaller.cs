using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameJamStarterKit.FXSystem
{
    public class Signaller : MonoBehaviour
    {
        public const string Complete = "Complete";
        
        private bool _processSignalNextFrame;
        private string _activeSignal;

        private bool _endSignalThisFrame;

        public void RaiseSignal(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            _activeSignal = key.ToLowerInvariant();
            _processSignalNextFrame = true;
        }

        public IEnumerator WaitForSignal(string signal)
        {
            if (string.IsNullOrEmpty(signal))
                yield break;

            signal = signal.ToLowerInvariant();

            while (_activeSignal != signal)
            {
                _endSignalThisFrame = true;
                yield return false;
            }

            _endSignalThisFrame = true;

        }

        private void LateUpdate()
        {
            if (_endSignalThisFrame)
            {
                _processSignalNextFrame = false;
            }
        }
    }
}