using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit
{
    public class AnimatorEventBridge : MonoBehaviour
    {
        public List<AnimatorEventData> EventMap = null;

        /// <summary>
        /// Calls the event mapped to the key given. Does nothing if the key is not found.
        /// </summary>
        /// <param name="key"></param>
        public void CallEvent(string key)
        {
            var unityEvent = EventMap.FirstOrDefault(data => data.EventKey == key);
            unityEvent?.Event.Invoke();
        }

        /// <summary>
        /// Calls the event mapped to the key given. Logs a warning if the event is not found.
        /// </summary>
        /// <param name="key"></param>
        public void CallEventWithLogWarning(string key)
        {
            var unityEvent = EventMap.FirstOrDefault(data => data.EventKey == key);
            if (unityEvent == null)
            {
                Debug.LogWarning("[" + gameObject.name + ":AnimatorEventBridge] Tried to call event " + key +
                                 " but the event was not found.");
                return;
            }

            unityEvent.Event.Invoke();
        }

        /// <summary>
        /// Calls the event mapped to the key given. logs a debug message when called. 
        /// </summary>
        /// <param name="key"></param>
        public void CallEventWithDebugLog(string key)
        {
            var unityEvent = EventMap.FirstOrDefault(data => data.EventKey == key);

            if (unityEvent == null)
            {
                Debug.Log("[" + gameObject.name + ":AnimatorEventBridge] Could not find key " + key);
                return;
            }

            Debug.Log("[" + gameObject.name + ":AnimatorEventBridge] Called event for " + key);
            unityEvent.Event.Invoke();
        }
    }

    [Serializable]
    public class AnimatorEventData
    {
        public string EventKey;
        public UnityEvent Event;
    }
}