using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Animation))]
    public class AnimationCaller : MonoBehaviour
    {
        private Animation _animationComponent;

        private void Awake()
        {
            _animationComponent = GetComponent<Animation>();
        }

        public void PlayDefaultAnimation()
        {
            _animationComponent.Play();
        }

        public void Play(AnimationClip clip)
        {
            _animationComponent.Play(clip.name);
        }
    }
}