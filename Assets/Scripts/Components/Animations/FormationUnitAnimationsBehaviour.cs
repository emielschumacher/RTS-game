using UnityEngine;
using Game.Components.Navigations;

namespace Game.Components.Animations
{
    public class FormationUnitAnimationsBehaviour : MonoBehaviour
    {
        [SerializeField] private AnimationsBehaviour _animationsBehaviour;
        [SerializeField] private NavigationBehaviour _navigationBehaviour;

        public AnimationClip idle;
        public AnimationClip hit;
        public AnimationClip death;
        public AnimationClip run;
        public AnimationClip attack;
        private int _frameInterval = 3;

        public void Start()
        {
            _animationsBehaviour.SetAnimationState(idle);
        }

        private void Update()
        {
            if (!(Time.frameCount % _frameInterval == 0))
            {
                if(_navigationBehaviour.isMoving == true) {
                    _animationsBehaviour.SetAnimationState(run);
                    return;
                }
                
                _animationsBehaviour.SetAnimationState(idle);
            }
        }
    }
} 