using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using ARPG.Container;

namespace ARPG.Combat
{
    // TODO: make this poolable!

    // TODO:
    // list all that already have taken damage and exclude them untill reset
    // reset after damageTick delay

    public class Projectile : MonoBehaviour, IDealDamage
    {
        [SerializeField] private RectTransform innerRadiusParent;

        [SerializeField] private List<GameObject> damageTaker = new List<GameObject>();
        [SerializeField] private List<GameObject> possibleDamageTaker = new List<GameObject>();
        [SerializeField] private List<GameObject> possibleDamageTakerInRange = new List<GameObject>();
        private float current;

        [Header("Filled Automatically")]
        public SpawnData data;
        public Vector3 TargetPosition;
        public Vector3 SpawnPosition;

        void OnEnable()
        {
            // the canvas is for debugging
            GetComponentInChildren<Canvas>().transform.localScale = new Vector2(data.OuterRadius * 2, data.OuterRadius * 2);

            var innerScale = 1 / data.OuterRadius * data.InnerRadius;
            if (innerRadiusParent && 0 < innerScale)
                innerRadiusParent.localScale = new(innerScale, innerScale, innerScale);
        }

        void Update()
        {
            float progress01 = current / Vector3.Distance(TargetPosition, SpawnPosition);

            if (SpawnPosition != TargetPosition && progress01 < 1)
                transform.position = Vector3.Lerp(SpawnPosition, TargetPosition, progress01);
            else
                Destroy(this.gameObject, .1f); // the duration is for debugging, remove that later

            current += Time.deltaTime * data.ProjectileSpeed; /// [projectileSpeed] units per second
        }

        private void OnTriggerEnter(Collider other)
        {
            // could use distanceChecks instead of Trigger
            // => go over all enemies
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

        public List<GameObject> DetectObjectsInsideShape()
        {
            //possibleDamageTaker.Clear();
            possibleDamageTakerInRange.Clear();
            damageTaker.Clear();

            foreach (var candidate in possibleDamageTaker)
            {
                float dist = Vector3.Distance(candidate.transform.position, transform.position);
                if (data.InnerRadius <= dist && dist <= data.OuterRadius) // this should include the projectiles radius as it would travel the full range and therefore extend it
                    possibleDamageTakerInRange.Add(candidate);
            }

            foreach (var candidate in possibleDamageTakerInRange)
            {
                Vector3 targetDir = candidate.transform.position - transform.position;
                float possibleAngle = Mathf.Abs(Vector3.Angle(targetDir, transform.forward));
                //if (possibleAngle <= data.ShapeAngle / 2)
                damageTaker.Add(candidate);
            }

            return damageTaker;
        }

        public void DealDamage(ITakeDamage target, float damage) => target.TakeDamage(damage);

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (data)
            {
                Handles.DrawWireDisc(transform.position, Vector3.up, data.OuterRadius);
                Handles.DrawWireDisc(transform.position, Vector3.up, data.InnerRadius);
            }
        }
#endif
    }
}