using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameJamStarterKit.FXSystem
{
    /// <summary>
    /// A generic mechanism to yield/await a 'signal'. This is a generic way to bridge different systems, but is most
    /// useful allowing code to await animation. A common workflow is:
    ///   * Code plays an animation
    ///   * Code calls yield return signaller.WaitForSignal("BeginPhase");
    ///   * The animation plays, then calls RaiseSignal("BeginPhase");
    ///   * The code recieves the signal and execution resumes.
    ///
    /// </summary>
    public class Signaller : MonoBehaviour
    {
        /// <summary>
        /// A useful default for signals.
        /// </summary>
        public const string Complete = "Complete";
        
        /// <summary>
        /// A simple adapter to allow for cross-coroutine communication and debugging.
        /// </summary>
        private class SignalObserver
        {
            public string Signal;
            public object Listener;
            public bool Raised;

            public override string ToString()
            {
                return $"[Signal={Signal}, Listener={Listener}]";
            }
        }

        private List<SignalObserver> _observers = new List<SignalObserver>();
        
        // A list of signals that have been received this frame.
        private HashSet<string> _activeSignals = new HashSet<string>();

        
        /// <summary>
        /// Called to raise a signal on this animator.
        /// </summary>
        /// <param name="key">A string that defines the agreed upon signal between systems.</param>
        public void RaiseSignal(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            string signal = key.ToLowerInvariant();
            _activeSignals.Add(signal);
            for (int i = _observers.Count - 1; i >= 0; i--)
            {
                var signalObserver = _observers[i];
                if (signalObserver.Signal == signal)
                {
                    signalObserver.Raised = true;
                    _observers.Remove(signalObserver);
                }
            }

            //Debug.Log($"Raising signal {key} - {gameObject.name}", gameObject);
        }

        /// <summary>
        /// Call this to wait for the signal requested to be raised.
        /// </summary>
        /// <param name="signal">The signal we are interested in.</param>
        /// <param name="listener">A listener value solely used to provide debugging info.</param>
        /// <returns></returns>
        public IEnumerator WaitForSignal(string signal, object listener=null)
        {
            if (string.IsNullOrEmpty(signal))
            {
                yield break;
            }

            var observer = new SignalObserver
            {
                Signal = signal.ToLowerInvariant(),
                Listener = listener
            };
            if (_activeSignals.Contains(observer.Signal))
            {
                observer.Raised = true;
            }
            else
            {
                _observers.Add(observer);
            }
            
            while (!observer.Raised)
            {
                yield return null;
            }

            //Debug.Log($"Found signal {signal} on frame {Time.frameCount} on {gameObject.name}", gameObject);
        }

        private void LateUpdate()
        {
            _activeSignals.Clear();
        }

        /// <summary>
        /// Shown in the inspector.
        /// </summary>
        /// <returns></returns>
        public string DebugInfo()
        {
            StringBuilder sb = new StringBuilder();
            var listeners = string.Join(",", _observers);
            sb.AppendLine($"Listeners: {listeners}");
            return sb.ToString();
        }
    }
}