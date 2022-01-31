using ARPG.Combat;
using ARPG.Container;
using ARPG.Enums;
using ARPG.Tools;
using System.Collections.Generic;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns
{
    public class PlayerController : Pawn, IRegenerate
    {
        public List<Skill> skills = new(6);

        protected override void Awake()
        {
            base.Awake();

            stats.Add(StatName.HealthPerSecond, new StatScore(5));
            stats.Add(StatName.ManaMax, new StatScore(60));
            stats.Add(StatName.ManaPerSecond, new StatScore(12));

            resources.Add(Resource.ManaCurrent, new ResourceScore(new StatScore(60)));
            //if (resources.TryGetValue(Resource.ManaCurrent, out ResourceScore manaCurrent))
            //    manaCurrent.AddToCurrentValue(60);

            foreach (var skill in skills)
                skill.SpawnData.CooldownTicker = new Ticker(skill.SpawnData.CooldownDuration, false);
        }

        private void LateUpdate()
        {
            foreach (var skill in skills)
                if (skill.SpawnData.CooldownTicker.IsTicking)
                    skill.SpawnData.CooldownTicker.Tick(Time.deltaTime);

            Regenerate(StatName.HealthMax, Resource.HealthCurrent, StatName.HealthPerSecond);
            Regenerate(StatName.ManaMax, Resource.ManaCurrent, StatName.ManaPerSecond);
        }

        // for debugging
        public void SetInteractionRange(float value) => interactionRange = value;

        public void Regenerate(StatName max, Resource resource, StatName regen)
        {
            if (stats.TryGetValue(max, out StatScore maxValue))
                if (resources.TryGetValue(resource, out ResourceScore current))
                    if (current.CurrentValue < maxValue.MaxValue)
                        if (stats.TryGetValue(regen, out StatScore regenValue))
                            current.AddToCurrentValue(regenValue.MaxValue * Time.deltaTime);
        }

        protected override void Kill()
        {
            // if(!hardcoreCharacter)
            //  respawn
            // else
            //  show death cause and 'createNewCharacter' menu
        }
    }
}