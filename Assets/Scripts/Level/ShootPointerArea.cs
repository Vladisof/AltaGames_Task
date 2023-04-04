using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Level
{
    public class ShootPointerArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            OnTapStarted?.Invoke();
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            OnTapFinished?.Invoke();
        }

        public event Action OnTapStarted;
        public event Action OnTapFinished;
    }
}