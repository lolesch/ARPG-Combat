using ARPG.Container;
using ARPG.Pawns.Destroyables;
using ARPG.Pawns.Enemy;
using ARPG.Tools;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ARPG.Combat
{
    // TODO: make this poolable!

    public class Projectile : MonoBehaviour
    {
        [SerializeField] private List<Transform> possibleEffectReceiver = new();
        [SerializeField] private List<IEffectReceiver> effectReceiver = new();
        [SerializeField] private List<IEffectReceiver> alreadyReceivedEffect = new();

        [SerializeField] private float lifetime = 0;
        private Vector3 targetPosition;
        private Vector3 spawnPosition;

        [Header("Filled Automatically")]
        public SpawnData data;

        void Awake() => spawnPosition = transform.position;

        void Update()
        {
            // if projectile's lifetime is over
            if (0 < data.Lifetime && data.Lifetime < lifetime)
                Destroy(this.gameObject);

            if (0 < data.ProjectileSpeed)
                ProjectileTraveling();

            DetectTargetsInRange();

            // TODO: rework damage on projectiles / DoT as groundEffect
            // => 

            #region ApplyEffects
            foreach (var receiver in effectReceiver)
                foreach (var effect in data.effects)
                    if (!alreadyReceivedEffect.Contains(receiver))
                    {
                        effect.ApplyEffect(receiver);
                        alreadyReceivedEffect.Add(receiver);
                    }
            #endregion

            /// if (0 < data.effects[0].Duration) // Effect has duration
            /// {
            ///    //TODO: this condition seems not to work as intended => use Coroutine?
            ///    if (!ticker.IsTicking) // is off cooldown
            ///    {
            ///        var tickrate = 0.2f;// data.hitEffects[0].TickRate;
            ///        var duration = data.effects[0].Duration;
            ///
            ///        ticker = new(tickrate, true);
            ///
            ///        // DPS = TotalDamage / Duration
            ///        // DPS = DamagePerTick * TicksPerSecond
            ///
            ///        // TicksPerSecond = 1 / Tickrate
            ///
            ///        // DamagePerTick = TotalDamage / (Duration * TicksPerSecond)
            ///        // DamagePerTick = TotalDamage * Tickrate / Duration
            ///
            ///        damage = damage * tickrate / duration; // damage per tick
            ///
            ///        foreach (var target in damageTaker)
            ///            DealDamage(target, damage);
            ///    }
            ///
            ///    ticker.Tick(Time.deltaTime);
            /// }
            /// else
            ///    foreach (var target in damageTaker)
            ///        if (!alreadyTakenDamage.Contains(target))
            ///        {
            ///            DealDamage(target, damage);
            ///            alreadyTakenDamage.Add(target);
            ///        }

            lifetime += Time.deltaTime;

            // instant and stationary
            if (data.Lifetime <= 0 && data.ProjectileSpeed <= 0)
                Destroy(this.gameObject, .1f); // .1f is for debugging! remove this once there are vfx to show the skill's impact/shape
        }

        void ProjectileTraveling()
        {
            targetPosition = spawnPosition + (transform.forward * data.DespawnRange);

            float progress01 = lifetime * data.ProjectileSpeed / data.DespawnRange;

            if (progress01 < 1)
                transform.position = Vector3.Lerp(spawnPosition, targetPosition, progress01);
            else
                Destroy(this.gameObject);
        }

        void DetectTargetsInRange()
        {
            GetPossibleDamageTaker();

            //damageTaker.Clear();

            if (possibleEffectReceiver.Any())
                foreach (var candidate in possibleEffectReceiver)
                {
                    var dist = XZPlane.Distance(transform.position, candidate.transform.position);

                    if (data.InnerRadius <= dist && dist <= data.OuterRadius)
                    {
                        candidate.TryGetComponent(out IEffectReceiver target);
                        if (!effectReceiver.Contains(target))
                            effectReceiver.Add(target);
                    }
                }

            void GetPossibleDamageTaker()
            {
                possibleEffectReceiver.Clear();

                foreach (var type in data.TargetTypes)
                    switch (type)
                    {
                        case Enums.InteractionType.Enemy:
                            foreach (var enemy in EnemyCollector.collection)
                                possibleEffectReceiver.Add(enemy.transform);
                            break;
                        case Enums.InteractionType.Destroyable:
                            foreach (var destroyable in DestroyableCollector.collection)
                                possibleEffectReceiver.Add(destroyable.transform);
                            break;
                        default:
                            break;
                    }
            }
        }

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

        //public void DealDamage(IDamageTaker target, float damage) => target.AddToCurrentHealth(damage);

        //public void ApplyEffect(IEffectReceiver receiver, DamageEffect effect)
        //{
        //    effect.Modifier.SetOrigin(this as IEffectApplier); // might need to be the caster instead
        //    receiver.ReceiveEffect(effect);
        //}

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (data)
            {
                Handles.color = Color.red;
                Handles.DrawWireDisc(transform.position, Vector3.up, data.OuterRadius, 2f);
                if (0 < data.InnerRadius)
                    Handles.DrawWireDisc(transform.position, Vector3.up, data.InnerRadius);
            }
        }
#endif
    }
}