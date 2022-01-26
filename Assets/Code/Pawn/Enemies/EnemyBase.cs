using ARPG.Combat;
using ARPG.Enums;
using ARPG.Input;
using System.Collections.Generic;
using TeppichsTools.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.Pawn.Enemy
{
    public class EnemyBase : Interactable, ITakeDamage
    {
        [SerializeField] private Dictionary<StatName, StatScore> stats = new Dictionary<StatName, StatScore>();
        [SerializeField] private Dictionary<Resource, ResourceScore> resources = new Dictionary<Resource, ResourceScore>();

        [SerializeField] private Image healthbar;

        protected override void Awake()
        {
            base.Awake();
            interactionRange = interactionCollider.radius;

            stats.Add(StatName.HealthMax, new StatScore(100));
            if (stats.TryGetValue(StatName.HealthMax, out StatScore healthMax))
                resources.Add(Resource.HealthCurrent, new ResourceScore(healthMax));

            if (resources.TryGetValue(Resource.HealthCurrent, out ResourceScore health))
                healthbar.fillAmount = healthMax.MaxValue / health.CurrentValue;
        }

        public void TakeDamage(float damage)
        {
            stats.TryGetValue(StatName.HealthMax, out StatScore healthMax);
            resources.TryGetValue(Resource.HealthCurrent, out ResourceScore health);

            health.AddToCurrentValue(-damage);
            EditorDebug.Log($"{this.name} took {damage} damage and has {health.CurrentValue} of {healthMax.MaxValue} health ({health.CurrentValue * 100 / healthMax.MaxValue} %)");

            healthbar.gameObject.SetActive(0 < health.CurrentValue && health.CurrentValue < healthMax.MaxValue);

            healthbar.fillAmount = healthMax.MaxValue / health.CurrentValue;
        }
    }
}