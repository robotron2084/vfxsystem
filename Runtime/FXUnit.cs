using UnityEngine;
using UnityEngine.Events;

namespace GameJamStarterKit.FXSystem
{
    public class FXUnit : MonoBehaviour
    {
        public DespawnType DespawnType = DespawnType.Timeout;

        public float DespawnTimeout = 5f;
        public float DespawnTimeoutMinimum = 1f;
        public float DespawnTimeoutMaximum = 5f;
        public SpawnScale SpawnScale;
        public SpawnType SpawnType;

        [HideInInspector]
        public UnityEvent OnDestroyed;


        private TimeSince _timeSinceSpawned;

        protected virtual void Start()
        {
            _timeSinceSpawned = 0f;

            if (DespawnType == DespawnType.TimeoutRange)
            {
                DespawnTimeout = Random.Range(DespawnTimeoutMinimum, DespawnTimeoutMaximum);
            }
        }

        protected virtual void Update()
        {
            if (DespawnType != DespawnType.Timeout && DespawnType != DespawnType.TimeoutRange)
                return;

            if (_timeSinceSpawned > DespawnTimeout)
            {
                Despawn();
            }
        }

        public virtual void Despawn()
        {
            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            OnDestroyed.Invoke();
        }

        public virtual void Animation_Despawn()
        {
            Despawn();
        }

        /// <summary>
        /// Detaches this FXUnit from its parent, keeping it's world position.
        /// </summary>
        public void Detach()
        {
            if (transform.parent != null)
            {
                transform.SetParent(null, true);
            }
        }
    }
}