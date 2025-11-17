using System;
using UnityEngine;

namespace MergeGame
{
    public class MergeablesCreator : MonoBehaviour
    {
        [SerializeField] private Board _board = null;
        [SerializeField] private MergeablesPool _pool = null;
        [SerializeField, Min(0.01f)] private float _waitingTimeForCreate;

        private float _timer;

        public event Action<BaseMergeable> Created;

        private void Update()
        {
            if (_board.HaveEmptyCells)
            {
                _timer += Time.deltaTime;

                if (_timer >= _waitingTimeForCreate)
                {
                    Create();
                }
            }
        }

        private void Create()
        {
            _timer = 0;

            BaseMergeable mergeable = _pool.Get();
            Created?.Invoke(mergeable);
        }
    }
}
