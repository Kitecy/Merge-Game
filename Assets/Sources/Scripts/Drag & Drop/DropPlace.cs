using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MergeGame
{
    public class DropPlace : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Draggable _draggable = null;

        public event Action<Draggable> Caught;
        public event Action DraggableChangedPlace;

        #region IDropHandler
        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out Draggable draggable) == false)
            {
                return;
            }

            if (draggable.IsSamePlace(this))
            {
                draggable.ResetToStart();
                return;
            }

            Caught?.Invoke(draggable);
        }
        #endregion

        public void Accept(Draggable draggable)
        {
            _draggable = draggable;
        }

        public void NotificateAboutChanges()
        {
            if (_draggable != null)
            {
                _draggable = null;
                DraggableChangedPlace?.Invoke();
            }
        }
    }
}
