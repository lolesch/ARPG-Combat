using ARPG.Container;
using ARPG.Enums;
using ARPG.Tools;
using System.Collections.Generic;
using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawns
{
    public class PlayerController : Pawn
    {
        public List<Skill> skills = new(6);

        protected override void Awake()
        {
            base.Awake();

            SetCurrentMana();

            foreach (var skill in skills)
                skill.SpawnData.CooldownTicker = new Ticker(skill.SpawnData.CooldownDuration, true);
        }

        protected override void Update()
        {
            base.Update();

            Regenerate(ResourceName.ManaCurrent, StatName.ManaPerSecond);

            foreach (var skill in skills)
                if (skill.SpawnData.CooldownTicker.HasRemainingDuration)
                    skill.SpawnData.CooldownTicker.Tick(Time.deltaTime);
        }

        protected void SetCurrentMana()
        {
            if (stats.TryGetValue(StatName.ManaMax, out StatScore manaMax))
            {
                resources.Add(ResourceName.ManaCurrent, new ResourceScore(manaMax));
                //if (resources.TryGetValue(ResourceName.ManaCurrent, out ResourceScore manaCurrent))
                //    manaCurrent.AddToCurrentValue(manaMax.MaxValue);
            }
        }

        // for debugging
        public void SetInteractionRange(float value) => interactionRange = value;

        protected override void Kill()
        {
            EditorDebug.LogWarning($"{this.name} died");
            Debug.Break();

            // if(!hardcoreCharacter)
            //  Respawn();
            // else
            //  show death cause and 'createNewCharacter' menu
        }
    }
}