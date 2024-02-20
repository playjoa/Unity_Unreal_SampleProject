using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Utils.Patterns
{
    public class SimpleObjectPool<TPoolObject> where TPoolObject : MonoBehaviour
    {
        private readonly TPoolObject _poolObjectModel;
        private readonly List<TPoolObject> _allPoolObjects = new();
        private readonly Queue<TPoolObject> _objectPoolQueue = new();

        private readonly RectTransform _rectTransformTarget;
        private readonly Transform _transformTarget;

        public SimpleObjectPool(TPoolObject poolObjectModel) => this._poolObjectModel = poolObjectModel;

        public SimpleObjectPool(TPoolObject poolObjectModel, RectTransform rectTransformTarget)
        {
            _poolObjectModel = poolObjectModel;
            this._rectTransformTarget = rectTransformTarget;
        }

        public SimpleObjectPool(TPoolObject poolObjectModel, Transform transformTarget)
        {
            _poolObjectModel = poolObjectModel;
            _transformTarget = transformTarget;
        }

        public TPoolObject RequestObject(bool objectActive = false)
        {
            if (!_objectPoolQueue.Any())
            {
                CreateAndEnqueueObject();
            }

            var targetObject = _objectPoolQueue.Dequeue();
            targetObject.gameObject.SetActive(objectActive);
            return targetObject;
        }

        public TPoolObject RequestObjectAtPosition(Vector3 position, bool objectActive = false)
        {
            var targetObject = RequestObject();
            targetObject.transform.position = position;
            targetObject.gameObject.SetActive(true);
            return targetObject;
        }

        public void DeactivatePool()
        {
            foreach (var poolObject in _allPoolObjects)
            {
                if (poolObject.gameObject.activeSelf)
                {
                    DeactivateAndEnqueue(poolObject);
                }
            }
        }

        public void ClearPool()
        {
            foreach (var poolObject in _allPoolObjects)
                Object.Destroy(poolObject);

            _allPoolObjects.Clear();
            _objectPoolQueue.Clear();
        }

        public void ReturnToPool(TPoolObject poolObject)
        {
            if (!_allPoolObjects.Contains(poolObject)) return;

            DeactivateAndEnqueue(poolObject);
        }

        private void DeactivateAndEnqueue(TPoolObject poolObject)
        {
            poolObject.gameObject.SetActive(false);
            _objectPoolQueue.Enqueue(poolObject);
        }

        private void CreateAndEnqueueObject()
        {
            TPoolObject newObject;

            if (_rectTransformTarget != null)
                newObject = Object.Instantiate(_poolObjectModel, _rectTransformTarget);
            else if (_transformTarget != null)
                newObject = Object.Instantiate(_poolObjectModel, _transformTarget);
            else
                newObject = Object.Instantiate(_poolObjectModel);

            _allPoolObjects.Add(newObject);
            _objectPoolQueue.Enqueue(newObject);
        }
    }
}