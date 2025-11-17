using UnityEngine;

namespace MergeGame
{
    public class MergeablesPlacer : MonoBehaviour
    {
        [SerializeField] private Board _board = null;
        [SerializeField] private MergeablesCreator _creator = null;

        private void OnEnable()
        {
            _creator.Created += OnCreated;
        }

        private void OnDisable()
        {
            _creator.Created -= OnCreated;
        }

        private void OnCreated(BaseMergeable mergeable)
        {
            _board.PutIntoRandomEmptyCell(mergeable);
        }
    }
}
