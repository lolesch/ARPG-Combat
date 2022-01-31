using ARPG.Combat;
using ARPG.Pawns;
using ARPG.Tools;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Container
{
    [CreateAssetMenu(fileName = "New Spawn Behaviour", menuName = "Inventory System/Items/SkillBehaviour/SpawnBehaviour")]
    public class SpawnData : ScriptableObject
    {
        public Ticker CooldownTicker;
        //
        [Tooltip("The duration (seconds) to wait before re-casting is enabled")]
        [Range(0f, 300f)]
        [SerializeField] private float cooldownDuration = 0f;
        public float CooldownDuration => cooldownDuration;
        //
        [Tooltip("The required resource amount consumed during casting")]
        [Range(0u, 300u)]
        [SerializeField] private uint resourceCost = 10;
        public uint ResourceCost => resourceCost;

        ///
        [Header("Spawn Settings")]
        //
        [Tooltip("Spawn at the current cursor position? else spawn at the caster's position")]
        [SerializeField] private bool spawnAtCursor = false;
        public bool SpawnAtCursor => spawnAtCursor;

        [Tooltip("The max distance to the caster the skill can spawn at")]
        [Range(1f, 30f)]
        [SerializeField] private float spawnRange = 5;
        public float SpawnRange => spawnRange;

        /// 
        [Header("Projectile Settings")]

        [Tooltip("The inner radius of the shape")]
        [Range(0f, 14f)]
        [SerializeField] private float innerRadius = 0f;
        public float InnerRadius => innerRadius;

        [Tooltip("The outer radius of the shape")]
        [Range(0.1f, 15f)]
        [SerializeField] private float outerRadius = 5;
        public float OuterRadius => outerRadius;

        [Tooltip("projectile passes through enemies? else despawn on first collision")]
        [SerializeField] private bool isPiercing = false;
        public bool IsPiercing => isPiercing;

        ///
        [Header("Path Settings")]

        [Tooltip("The amount of projectiles spawned")]
        [Range(1u, 24u)]
        [SerializeField] private uint projectileAmount = 1u;
        public uint ProjectileAmount => projectileAmount;

        [Tooltip("The angle of launched projectiles centered on the caster's forward direction. 0 == line")]
        [Range(0u, 360u)]
        [SerializeField] private uint shapeAngle = 0u;
        public float ShapeAngle => shapeAngle;

        [Range(0u, 30u)]
        [SerializeField] private uint projectileSpeed = 3u;
        public float ProjectileSpeed => projectileSpeed;

        [Range(0f, 30f)]
        [SerializeField] private float despawnRange = 1;
        public float DespawnRange => despawnRange;

        [SerializeField] private Projectile projectile;
        public Projectile Projectile => projectile;

        // needs to be a dictionary to get the effects by name
        public List<Effect> hitEffects = new();

        // tickrate
        // duration (if DoT)
    }
}