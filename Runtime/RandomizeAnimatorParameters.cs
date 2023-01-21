using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameJamStarterKit.FXSystem
{
    public class RandomizeAnimatorParameters : MonoBehaviour
    {
        [Serializable]
        public class RandomizeParameterData
        {
            public string ParameterKey;
            public AnimatorControllerParameterType ParameterType = AnimatorControllerParameterType.Float;
            public bool OverrideRange;
            public float MinimumValue;
            public float MaximumValue;
        }

        public enum RandomizeParameterType
        {
            Awake,
            Start,
            Interval,
            IntervalRange,
            FixedUpdate
        }

        [Tooltip("Find the attached animator component on awake rather than manually setting it.")]
        public bool UseAttachedAnimator;

        public Animator Animator;
        public RandomizeParameterType RandomizeType = RandomizeParameterType.Interval;
        public float Interval = 5f;
        public float MinimumInterval = 1f;
        public float MaximumInterval = 5f;
        public List<RandomizeParameterData> ParameterData;
        public float MinimumValue;
        public float MaximumValue = 1f;

        private TimeSince _timeSinceInterval;

        private void Awake()
        {
            if (UseAttachedAnimator)
            {
                Animator = GetComponent<Animator>();
            }

            if (RandomizeType == RandomizeParameterType.Awake)
            {
                Randomize();
            }
        }

        private void Start()
        {
            if (RandomizeType == RandomizeParameterType.Start)
            {
                Randomize();
            }
            else if (RandomizeType == RandomizeParameterType.IntervalRange)
            {
                RandomizeInterval();
            }
        }

        private void FixedUpdate()
        {
            if (RandomizeType == RandomizeParameterType.FixedUpdate)
            {
                Randomize();
            }
        }

        private void Update()
        {
            if (RandomizeType != RandomizeParameterType.Interval &&
                RandomizeType != RandomizeParameterType.IntervalRange)
                return;

            if (_timeSinceInterval > Interval)
            {
                _timeSinceInterval = 0f;
                Randomize();
                if (RandomizeType == RandomizeParameterType.IntervalRange)
                {
                    RandomizeInterval();
                }
            }
        }

        private void RandomizeInterval()
        {
            Interval = Random.Range(MinimumInterval, MaximumInterval);
        }

        public void Randomize()
        {
            foreach (var data in ParameterData)
            {
                if (Animator.HasParameterWithType(data.ParameterKey, data.ParameterType))
                {
                    float value;
                    if (data.OverrideRange)
                    {
                        value = Random.Range(data.MinimumValue, data.MaximumValue);
                    }
                    else
                    {
                        value = Random.Range(MinimumValue, MaximumValue);
                    }

                    switch (data.ParameterType)
                    {
                        case AnimatorControllerParameterType.Float:
                            Animator.SetFloat(data.ParameterKey, value);
                            break;
                        case AnimatorControllerParameterType.Int:
                            Animator.SetInteger(data.ParameterKey, Mathf.RoundToInt(value));
                            break;
                        case AnimatorControllerParameterType.Bool:
                            Animator.SetBool(data.ParameterKey, CoinToss());
                            break;
                        case AnimatorControllerParameterType.Trigger:
                            var set = CoinToss();
                            if (set)
                            {
                                Animator.SetTrigger(data.ParameterKey);
                            }
                            else
                            {
                                Animator.ResetTrigger(data.ParameterKey);
                            }

                            break;
                    }
                }
            }
        }
        
        
        /// <summary>
        /// returns true or false randomly.
        /// </summary>
        /// <returns></returns>
        public static bool CoinToss()
        {
            return Random.value > 0.5f;
        }

    }
}