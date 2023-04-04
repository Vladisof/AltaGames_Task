using UnityEngine;

namespace Level
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private Transform jumpPoint;
        [SerializeField] private Animation doorAnimation;
        public Vector3 FinalPoint => transform.position;

        public Vector3 JumpPoint => jumpPoint.position;

        public void Open()
        {
            doorAnimation.Play();
        }
    }
}