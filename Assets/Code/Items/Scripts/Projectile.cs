using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using ARPG.Container;
using ARPG.Pawns.Enemy;
using ARPG.Tools;
using ARPG.Input;
using ARPG.Pawns;
using TeppichsTools.Logging;

namespace ARPG.Combat
{
    // TODO: make this poolable!

    // TODO:
    // list all that already have taken damage and exclude them untill reset
    // reset after damageTick delay

    public class Projectile : MonoBehaviour, IDealDamage
    {
        [SerializeField] private List<ITakeDamage> damageTaker = new();
        [SerializeField] private List<ITakeDamage> alreadyTakenDamage = new();
        [SerializeField] private List<ITakeDamage> possibleDamageTakerInRange = new();
        private float current;

        [Header("Filled Automatically")]
        public SpawnData data;
        public Vector3 TargetPosition;
        public Vector3 SpawnPosition;

        Ticker ticker = new(0);

        void Update()
        {
            ProjectileTraveling();

            List<Interactable> interactables = new();

            // TODO: make the projectile know it's targetGroup
            foreach (var item in EnemyCollector.collection)
                interactables.Add(item);

            DetectTargetsInRange(interactables);


            // change this into a dictionary => use tryGet
            if (data.hitEffects[0].StatName == Enums.StatName.Damage)
            {
                var damage = data.hitEffects[0].Stat.MaxValue;

                if (0 < data.hitEffects[0].Duration)
                {
                    if (!ticker.IsTicking)
                    {
                        var tickrate = data.hitEffects[0].TickRate;

                        EditorDebug.LogWarning($"tickrate {tickrate}");

                        ticker = new(tickrate);

                        damage = damage * tickrate;

                        foreach (var target in damageTaker)
                            DealDamage(target, damage);
                    }

                    ticker.Tick(Time.deltaTime);
                }
                else
                    foreach (var target in damageTaker)
                        if (!alreadyTakenDamage.Contains(target))
                        {
                            DealDamage(target, damage);
                            alreadyTakenDamage.Add(target);
                        }
            }

            void ProjectileTraveling()
            {
                float progress01 = current / Vector3.Distance(TargetPosition, SpawnPosition);

                if (SpawnPosition != TargetPosition && progress01 < 1)
                    transform.position = Vector3.Lerp(SpawnPosition, TargetPosition, progress01);
                else
                    Destroy(this.gameObject, data.hitEffects[0].Duration); // the .1f duration is for visual debugging

                current += Time.deltaTime * data.ProjectileSpeed; /// [projectileSpeed] units per second
            }

            void DetectTargetsInRange(List<Interactable> interactables)
            {
                damageTaker.Clear();

                foreach (var candidate in interactables)
                {
                    var dist = XZPlane.Magnitude(transform.position, candidate.transform.position);

                    if (data.InnerRadius <= dist && dist <= data.OuterRadius)
                        if (candidate.TryGetComponent(out ITakeDamage target))
                            damageTaker.Add(target);
                }
            }
        }

        //private void OnTriggerEnter(Collider other)
        //{
        //    possibleDamageTaker.Add(other.gameObject);
        //    DetectObjectsInsideShape();
        //}

        //private void OnTriggerExit(Collider other)
        //{
        //    if (possibleDamageTaker.Contains(other))
        //        possibleDamageTaker.Remove(other);
        //
        //    //foreach (var target in damageTaker)
        //    //    DealDamage(target, 30);
        //}

        //public List<Interactable> DetectObjectsInsideShape()
        //{
        //    //possibleDamageTaker.Clear();
        //    possibleDamageTakerInRange.Clear();
        //    damageTaker.Clear();
        //
        //    DistanceCheck();
        //
        //    AngleCheck();
        //
        //    return damageTaker;
        //
        //    void DistanceCheck()
        //    {
        //        foreach (var candidate in possibleDamageTaker)
        //        {
        //            float dist = Vector3.Distance(candidate.transform.position, transform.position);
        //            if (data.InnerRadius <= dist && dist <= data.OuterRadius) // this should include the projectiles radius as it would travel the full range and therefore extend it
        //                possibleDamageTakerInRange.Add(candidate);
        //        }
        //    }
        //
        //    void AngleCheck()
        //    {
        //        foreach (var candidate in possibleDamageTakerInRange)
        //        {
        //            Vector3 targetDir = candidate.transform.position - transform.position;
        //            float possibleAngle = Mathf.Abs(Vector3.Angle(targetDir, transform.forward));
        //            //if (possibleAngle <= data.ShapeAngle / 2)
        //            damageTaker.Add(candidate);
        //        }
        //    }
        //}

        public void DealDamage(ITakeDamage target, float damage) => target.TakeDamage(damage);

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (data)
            {
                Handles.color = Color.red;
                Handles.DrawWireDisc(transform.position, Vector3.up, data.OuterRadius, 2f);
                Handles.DrawWireDisc(transform.position, Vector3.up, data.InnerRadius);
            }
        }
#endif
    }
}