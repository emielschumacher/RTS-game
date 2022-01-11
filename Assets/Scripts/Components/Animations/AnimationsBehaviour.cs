using UnityEngine;
using UnityEngine.Animations;

namespace Game.Components.Animations
{
    [RequireComponent(typeof(Animator))]
    public class AnimationsBehaviour : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private string _currentState;
        private string _state;

        public void SetAnimationState(AnimationClip animationClip)
        {
            _state = animationClip.name;
            
            if(_currentState == _state) return;

            _animator.CrossFade(_state, 0.25f);
            
            _currentState = _state;
        }
    }
} 