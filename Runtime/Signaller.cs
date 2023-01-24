using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameJamStarterKit.FXSystem
{
    public class Signaller : MonoBehaviour
    {
        public const string Complete = "Complete";
        
        // A signal might be raised in animation or a coroutine and then the frame will end before the listening coroutine
        // gets an opportunity to respond.
        private bool _endSignalThisFrame = false;

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

        private HashSet<string> _listeners = new HashSet<string>();
        private List<SignalObserver> _observers = new List<SignalObserver>();
        
        private HashSet<string> _activeSignals = new HashSet<string>();

        public void RaiseSignal(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;

            string signal = key.ToLowerInvariant();
            _activeSignals.Add(signal);
            foreach (var signalObserver in _observers)
            {
                if (signalObserver.Signal == signal)
                {
                    signalObserver.Raised = true;
                }
            }

            Debug.Log($"Raising signal {key} - {gameObject.name}", gameObject);
        }

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

            Debug.Log($"Found signal {signal} on frame {Time.frameCount} on {gameObject.name}", gameObject);
            _observers.Remove(observer);
            _endSignalThisFrame = true;

        }

        private void LateUpdate()
        {
            _activeSignals.Clear();
        }

        public string DebugInfo()
        {
            StringBuilder sb = new StringBuilder();
            var listeners = string.Join(",", _observers);
            sb.AppendLine($"Listeners: {listeners}");
            var signals = string.Join(",", _activeSignals);
            sb.AppendLine($"Signals: {signals}");

            return sb.ToString();
        }
    }
}