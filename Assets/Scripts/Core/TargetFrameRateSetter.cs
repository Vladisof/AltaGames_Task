using UnityEngine;

namespace Core
{
    public class TargetFrameRateSetter : MonoBehaviour
    {
        [SerializeField] [Range(0, 240)] private int targetFrameRate = 60;

        private void Start()
        {
            Application.targetFrameRate = targetFrameRate;
        }
    }
}