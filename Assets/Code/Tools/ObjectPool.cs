using UnityEngine;
using System.Collections.Generic;

namespace ARPG.Tools
{
    public class ObjectPool<T> : IObjectToPool<T> where T : MonoBehaviour
    {
        private T objectToPool;
        private Transform poolParent;
        private uint amountToPool;

        public Queue<T> objectQueue;

        public ObjectPool(T obj, Transform parent)
        {
            objectToPool = obj;
            poolParent = parent;
        }

        void Start()
        {
            if (!objectToPool)
                throw new MissingReferenceException();
            if (!poolParent)
                throw new MissingReferenceException();

            Populate(amountToPool);
        }

        //Queue.Peek
        //Queue.TrimTiSize
        //Queue.Contains

        public void Populate(uint amountToPool)
        {
            objectQueue = new Queue<T>();

            for (int i = 0; i < amountToPool; i++)
            {
                T item = GameObject.Instantiate(objectToPool, poolParent);
                if (null == item)
                {
                    Debug.LogError($"couldn't instantiate {objectToPool}");
                    return;
                }
                item.gameObject.SetActive(false);
                objectQueue.Enqueue(item);
            }
        }

        //next, release, cull, release all

        // on demand hand over the first object not in use (turned off)
        // TODO: if no items left, use the oldest item in use OR extend the pool
        public T Next(Vector3 position, Quaternion rotation)
        {
            //TODO: detect if still objects to spawn
            T objectToSpawn = objectQueue.Dequeue();

            objectToSpawn.gameObject.SetActive(true);
            objectToSpawn.transform.position = position;
            objectToSpawn.transform.rotation = rotation;

            return objectToSpawn;
        }

        // on release turn off object
        public void Release(T obj)
        {
            obj.gameObject.SetActive(false);
            objectQueue.Enqueue(obj);
        }
    }
}