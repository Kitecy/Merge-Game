using UnityEngine;

namespace MergeGame
{
    public class MergeablesPool : Pool<BaseMergeable>
    {
        [SerializeField] private Transform _poolStorage = null;
        [SerializeField] private Transform _canvas = null;
        [SerializeField] private MergeablesPool _nextLevelMergeablePool = null;

        protected override BaseMergeable CreateObject()
        {
            BaseMergeable baseMergeable = Instantiate(Prefab);
            baseMergeable.SetPool(this);
            baseMergeable.SetNextMergeablesPool(_nextLevelMergeablePool);

            baseMergeable.GetDraggable().SetCanvas(_canvas);

            return baseMergeable;
        }

        protected override void OnGetObject(BaseMergeable poolableObject)
        {
            poolableObject.gameObject.SetActive(true);
        }

        protected override void OnReleaseObject(BaseMergeable poolableObject)
        {
            poolableObject.gameObject.SetActive(false);
            poolableObject.transform.SetParent(_poolStorage, true);
        }
    }
}
