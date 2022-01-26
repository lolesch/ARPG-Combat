using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace ARPG.Combat
{
    [RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
    // DOTO: make this poolable!
    public class DamageShape : MonoBehaviour, IDealDamage
    {
        [SerializeField] private List<GameObject> damageTaker = new List<GameObject>();
        [SerializeField] private List<GameObject> possibleDamageTaker = new List<GameObject>();
        [SerializeField] private List<GameObject> possibleDamageTakerInRange = new List<GameObject>();
        private float current;

        [Header("Filled Automatically")]
        public float projectileSpeed;
        public Vector3 target;
        public Vector3 spawnPosition;

        void Update()
        {
            float progress01 = current / Vector3.Distance(target, spawnPosition);

            if (spawnPosition != target && progress01 < 1)
                transform.position = Vector3.Lerp(spawnPosition, target, progress01);
            else
                Destroy(this.gameObject, .2f);

            current += Time.deltaTime * projectileSpeed;
        }

        public List<GameObject> DetectObjectsInsideShape()
        {
            possibleDamageTaker.Clear();
            possibleDamageTakerInRange.Clear();
            damageTaker.Clear();

            //foreach (var candidate in possibleDamageTaker)
            //{
            //    float dist = Vector3.Distance(candidate.transform.position, transform.position);
            //    if (behaviour.MinDistance <= dist && dist <= behaviour.MaxDistance)
            //        possibleDamageTakerInRange.Add(candidate);
            //}
            //
            //foreach (var candidate in possibleDamageTakerInRange)
            //{
            //    Vector3 targetDir = candidate.transform.position - transform.position;
            //    float possibleAngle = Mathf.Abs(Vector3.Angle(targetDir, transform.forward));
            //    if (possibleAngle <= behaviour.FullAngle / 2)
            //        damageTaker.Add(candidate);
            //}

            return damageTaker;
        }

        private void OnTriggerEnter(Collider other)
        {
            possibleDamageTaker.Add(other.gameObject);
            DetectObjectsInsideShape();
        }

        private void OnTriggerExit(Collider other)
        {
            if (possibleDamageTaker.Contains(other.gameObject))
                possibleDamageTaker.Remove(other.gameObject);

            //foreach (var target in damageTaker)
            //    DealDamage(target, 30);
        }

        public void DealDamage(ITakeDamage target, float damage) => target.TakeDamage(damage);

#if UNITY_EDITOR
        private void OnDrawGizmos() => Handles.DrawWireDisc(transform.position, Vector3.up, GetComponent<CapsuleCollider>().radius);
#endif
    }
}