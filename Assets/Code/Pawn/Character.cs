using System.Collections.Generic;
using ARPG.Combat;
using ARPG.Enums;
using ARPG.Input;
using TeppichsTools.Logging;

namespace ARPG.Pawn
{
    public class Character : Interactable, ITakeDamage
    {
        public Dictionary<StatName, StatScore> stats = new Dictionary<StatName, StatScore>();
        public Dictionary<Resource, ResourceScore> resources = new Dictionary<Resource, ResourceScore>();

        protected virtual void Awake()
        {
            stats.Add(StatName.HealthMax, new StatScore(100));
            SetCurrentHealth();
        }

        public void SetCurrentHealth()
        {
            if (stats.TryGetValue(StatName.HealthMax, out StatScore healthMax))
            {
                resources.Add(Resource.HealthCurrent, new ResourceScore(healthMax));
                if (resources.TryGetValue(Resource.HealthCurrent, out ResourceScore healthCurrent))
                    healthCurrent.AddToCurrentValue(healthMax.MaxValue);
            }
        }

        public void TakeDamage(float damage)
        {
            stats.TryGetValue(StatName.HealthMax, out StatScore healthMax);
            resources.TryGetValue(Resource.HealthCurrent, out ResourceScore health);

            health.AddToCurrentValue(-damage);
            EditorDebug.Log($"{this.name} took {damage} damage and has {health.CurrentValue} of {healthMax.MaxValue} health ({health.CurrentValue * 100 / healthMax.MaxValue} %)");
        }

        protected override void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}