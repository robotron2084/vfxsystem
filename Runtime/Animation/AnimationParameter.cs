using System;
using UnityEngine;

namespace GameJamStarterKit
{
    [Serializable]
    public class AnimationParameter
    {
        public string Key;
        public AnimatorControllerParameterType ParameterType = AnimatorControllerParameterType.Bool;
        public bool BoolValue;
        public float FloatValue;
        public int IntValue;

        public void Set(Animator animator)
        {
            // silently fail
            if (!animator.HasParameterWithType(Key, ParameterType))
                return;

            switch (ParameterType)
            {
                case AnimatorControllerParameterType.Float:
                    animator.SetFloat(Key, FloatValue);
                    break;
                case AnimatorControllerParameterType.Int:
                    animator.SetInteger(Key, IntValue);
                    break;
                case AnimatorControllerParameterType.Bool:
                    animator.SetBool(Key, BoolValue);
                    break;
                case AnimatorControllerParameterType.Trigger:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}