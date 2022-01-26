using UnityEngine;
using UnityEngine.AI;
using System;

namespace ARPG.Tools
{
    public class Spawner : MonoBehaviour
    {
        [Serializable]
        private struct ObjectToSpawn
        {
            public GameObject gameObject;
            public int count;
            public float radius;
        }

        [SerializeField] private ObjectToSpawn[] objects;

        private void Start()
        {
            //TODO: use an object pool

            for (int i = 0; i < objects.Length; i++)
                SpawnAtRandomPosition(objects[i].count, objects[i].gameObject, objects[i].radius);
        }

        private void SpawnAtRandomPosition(int number, GameObject gameObject, float radius)
        {
            for (int i = 0; i < number; i++)
            {
                Vector3 position = transform.position + UnityEngine.Random.insideUnitSphere * radius;
                Vector3 spawnPosition = Vector3.zero;

                if (NavMesh.SamplePosition(position, out NavMeshHit hit, radius, -1))
                    spawnPosition = hit.position;

                Instantiate(gameObject, spawnPosition, Quaternion.identity, transform);
            }
        }
    }
}