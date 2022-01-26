using UnityEngine;

public interface IObjectToPool<T>
{
    T Next(Vector3 position, Quaternion rotation);

    void Release(T obj);
}
