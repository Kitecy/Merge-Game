using DG.Tweening;
using UnityEngine;

namespace MergeGame
{
    public class Cell : MonoBehaviour
    {
        private const string Error = "This cell is already occupied";

        [SerializeField] private DropPlace _dropPlace = null;
        [SerializeField] private BaseMergeable _mergeable = null;

        public bool IsEmpty => _mergeable == null;

        private void OnEnable()
        {
            _dropPlace.Caught += OnDrop;
            _dropPlace.DraggableChangedPlace += OnMergeableChangedCell;
        }

        private void OnDisable()
        {
            _dropPlace.Caught -= OnDrop;
            _dropPlace.DraggableChangedPlace -= OnMergeableChangedCell;
        }

        private void OnDestroy()
        {
            if (_mergeable != null)
            {
                _mergeable.Released -= Throw;
            }
        }

        public bool Compare(BaseMergeable mergeable)
        {
            if (mergeable == null)
            {
                throw new System.ArgumentNullException(nameof(mergeable));
            }

            if (_mergeable == null)
            {
                throw new System.InvalidOperationException("This cell is empty");
            }

            return _mergeable.Compare(mergeable);
        }

        public void Set(BaseMergeable mergeable, bool withAnimation)
        {
            if (_mergeable != null)
            {
                Debug.LogError(Error);
                return;
            }

            _mergeable = mergeable ?? throw new System.ArgumentNullException(nameof(mergeable));
            Draggable draggable = mergeable.GetDraggable();

            draggable.SetPlace(_dropPlace);
            _dropPlace.Accept(draggable);

            _mergeable.transform.SetParent(transform, true);

            if (withAnimation)
            {
                _mergeable.transform.DOLocalMove(Vector3.zero, draggable.MoveDuration);
            }
            else
            {
                _mergeable.transform.localPosition = Vector3.zero;
            }

            _mergeable.Released += Throw;
        }

        public void Throw()
        {
            _mergeable.Released -= Throw;
            _mergeable = null;
        }

        private void OnDrop(Draggable draggable)
        {
            if (draggable.TryGetComponent(out BaseMergeable mergeable) == false)
            {
                draggable.ResetToStart();
                return;
            }

            if (_mergeable == null)
            {
                Set(mergeable, true);
                return;
            }

            if (_mergeable.TryMerge(mergeable, out IMergeable result) == false)
            {
                draggable.ResetToStart();
                return;
            }

            mergeable.Release();
            _mergeable.Release();

            Set(result as BaseMergeable, false);
        }

        private void OnMergeableChangedCell()
        {
            Throw();
        }
    }
}
