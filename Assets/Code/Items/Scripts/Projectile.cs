using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using ARPG.Container;
using ARPG.Pawns.Enemy;
using ARPG.Tools;
using TeppichsTools.Logging;

namespace ARPG.Combat
{
    // TODO: make this poolable!

    public class Projectile : MonoBehaviour, IDamageDealer
    {
        [SerializeField] private List<Transform> possibledamageTaker = new();
        [SerializeField] private List<IDamageTaker> damageTaker = new();
        [SerializeField] private List<IDamageTaker> alreadyTakenDamage = new();

        [SerializeField] private float current = 0;
        private Vector3 targetPosition;
        private Vector3 spawnPosition;
        private Ticker ticker = new(0);

        [Header("Filled Automatically")]
        public SpawnData data;

        void Awake() => spawnPosition = transform.position;

        void Update()
        {

            // projectile has lifetime && lifetime isn't over
            if (0 < data.Lifetime && data.Lifetime < current)
                Destroy(this.gameObject);

            if (0 < data.ProjectileSpeed)
                ProjectileTraveling();

            // does this need to happen each update? => make it just before applying damage
            GetPossibleDamageTaker();

            DetectTargetsInRange();

            // DPS = TotalDamage / Duration = DamagePerTick * TicksPerSecond
            // TicksPerSecond = 1 / Tickrate

            // => DamagePerTick = TotalDamage / (Duration * TicksPerSecond)
            // => DamagePerTick = TotalDamage * Tickrate / Duration

            // change this into a dictionary => use tryGet
            if (data.hitEffects[0].StatName == Enums.StatName.Damage)
            {
                var damage = data.hitEffects[0].Stat.MaxValue;

                //TODO: whats the diff between Lifetime and EffectDuration?

                if (0 < data.hitEffects[0].Duration) // Effect has duration
                {
                    //TODO: this condition seems not to work as intended => use Coroutine?
                    if (!ticker.IsTicking) // is off cooldown
                    {
                        var tickrate = data.hitEffects[0].TickRate;
                        var duration = data.hitEffects[0].Duration;

                        EditorDebug.LogWarning($"tickrate {tickrate} | current {current}");

                        ticker = new(tickrate, true);

                        damage = (damage * tickrate) / duration; // damage per tick

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

                current += Time.deltaTime;

                if (data.Lifetime <= 0 && data.ProjectileSpeed <= 0)
                    Destroy(this.gameObject, .1f); // .1f is for debugging! remove this later!
            }


            void ProjectileTraveling()
            {
                targetPosition = spawnPosition + (transform.forward * data.DespawnRange);

                float progress01 = current * data.ProjectileSpeed / Vector3.Distance(spawnPosition, targetPosition); // current / despawnRange?

                if (progress01 < 1)
                    transform.position = Vector3.Lerp(spawnPosition, targetPosition, progress01);
                else
                    Destroy(this.gameObject);
            }

            void DetectTargetsInRange()
            {
                damageTaker.Clear();
                //alreadyTakenDamage.Clear();

                if (0 < possibledamageTaker.Count)
                    foreach (var candidate in possibledamageTaker)
                    {
                        var dist = XZPlane.Magnitude(transform.position, candidate.transform.position);

                        if (data.InnerRadius <= dist && dist <= data.OuterRadius)
                        {
                            candidate.TryGetComponent(out IDamageTaker target);
                            if (!damageTaker.Contains(target))
                                damageTaker.Add(target);
                        }
                    }
            }

            void GetPossibleDamageTaker()
            {
                possibledamageTaker.Clear();

                foreach (var type in data.TargetTypes)
                    switch (type)
                    {
                        case Enums.InteractionType.Enemy:
                            foreach (var enemy in EnemyCollector.collection)
                                possibledamageTaker.Add(enemy.transform);
                            break;
                        case Enums.InteractionType.Destroyable:
                            foreach (var destroyable in DestroyableCollector.collection)
                                possibledamageTaker.Add(destroyable.transform);
                            break;
                        default:
                            break;
                    }
            }
        }

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

        public void DealDamage(IDamageTaker target, float damage) => target.TakeDamage(damage);

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