using ARPG.Container;
using ARPG.Enums;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.Pawn
{
    public class Player : Character
    {
        public List<Skill> skills = new(6);

        [SerializeField] private Image healthbar;
        protected void Awake()
        {
            stats.Add(StatName.HealthMax, new StatScore(100));
            if (stats.TryGetValue(StatName.HealthMax, out StatScore healthMax))
                resources.Add(Resource.HealthCurrent, new ResourceScore(healthMax));

            if (resources.TryGetValue(Resource.HealthCurrent, out ResourceScore health))
                healthbar.fillAmount = healthMax.MaxValue / health.CurrentValue;
        }
    }
}