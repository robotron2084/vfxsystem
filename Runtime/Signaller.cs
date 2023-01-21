using System.Collections;
using UnityEngine;

namespace GameJamStarterKit.FXSystem
{
    public class Signaller : MonoBehaviour
    {
        public bool ProcessSignalNextFrame;
        public string ActiveSignal;

        private bool _endSignalThisFrame;

        public void RaiseSignal(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            ActiveSignal = key.ToLowerInvariant();
            ProcessSignalNextFrame = true;
        }

        public IEnumerator WaitForSignal(string signal)
        {
            if (string.IsNullOrEmpty(signal))
                yield break;

            signal = signal.ToLowerInvariant();

            while (!ProcessSignalNextFrame || ActiveSignal != signal)
            {
                _endSignalThisFrame = true;
                yield return false;
            }

            _endSignalThisFrame = true;
            yield return true;
        }

        private void LateUpdate()
        {
            if (_endSignalThisFrame)
            {
                ProcessSignalNextFrame = false;
            }
        }
    }
}