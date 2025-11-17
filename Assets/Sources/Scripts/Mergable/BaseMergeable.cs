using UnityEngine;

namespace MergeGame
{
    [RequireComponent(typeof(Draggable))]
    public class BaseMergeable : PoolableObject<BaseMergeable>, IMergeable
    {
        [SerializeField] private MergeablesPool _nextMergeablePool = null;
        [SerializeField] private Draggable _draggable = null;

        #region IMergeable
        [field: SerializeField] public int Level { get; private set; }
        [field: SerializeField] public IMergeable Next { get; private set; }

        public void SetNextMergeablesPool(MergeablesPool pool)
        {
            _nextMergeablePool = pool ?? throw new System.ArgumentNullException(nameof(pool));
        }

        public bool TryMerge(IMergeable mergeable, out IMergeable result)
        {
            result = null;

            if (Compare(mergeable))
            {
                result = _nextMergeablePool.Get();
                return true;
            }

            return false;
        }

        public bool Compare(IMergeable mergeable)
        {
            return mergeable.Level == Level;
        }
        #endregion

        public Draggable GetDraggable()
        {
            return _draggable;
        }

        protected override void OnReleased()
        {
            _draggable.ResetState();
        }
    }
}
