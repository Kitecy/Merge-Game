using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MergeGame
{
    public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Transform _canvas = null;
        [SerializeField] private Graphic _graphic = null;
        [SerializeField] private DropPlace _place = null;

        private RectTransform _transform = null;
        private Transform _cachedParent = null;

        private Vector2 _startPosition;

        [field: SerializeField] public float MoveDuration { get; private set; }

        private void Awake()
        {
            _transform = transform as RectTransform;
        }

        #region IBeginDragHandler
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_graphic != null)
            {
                _graphic.raycastTarget = false;
            }

            MakeThisMaxPriority();

            _startPosition = _transform.position;
        }
        #endregion

        #region IDraghandler
        public void OnDrag(PointerEventData eventData)
        {
            _transform.position = eventData.position;
        }
        #endregion

        #region IEndDragHandler
        public void OnEndDrag(PointerEventData eventData)
        {
            if (eventData.pointerEnter == null || eventData.pointerEnter.GetComponent<DropPlace>() == null)
            {
                ResetToStart();
            }

            if (_graphic != null)
            {
                _graphic.raycastTarget = true;
            }

            _cachedParent = null;
        }
        #endregion 

        public void SetCanvas(Transform canvas)
        {
            _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
        }

        private void MakeThisMaxPriority()
        {
            _cachedParent = _transform.parent;
            _transform.SetParent(_canvas, true);
            _transform.SetAsLastSibling();
        }

        public void ResetToStart()
        {
            _transform.SetParent(_cachedParent, true);
            _transform.DOMove(_startPosition, MoveDuration);
            _cachedParent = null;
        }

        public void SetPlace(DropPlace place)
        {
            if (place == null)
                throw new ArgumentNullException(nameof(place));

            if (_place != null)
            {
                _place.NotificateAboutChanges();
            }

            _place = place;
        }

        public void ResetState()
        {
            _place = null;
            _graphic.raycastTarget = true;
        }

        public bool IsSamePlace(DropPlace place)
        {
            return place == _place;
        }
    }
}
