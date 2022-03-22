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

            // needs resources in the player starting stats
            resources.Add(ResourceName.ManaCurrent, new ResourceScore(new StatScore(60)));

            //if (resources.TryGetValue(Resource.ManaCurrent, out ResourceScore manaCurrent))
            //    manaCurrent.AddToCurrentValue(60);

            foreach (var skill in skills)
                skill.SpawnData.CooldownTicker = new Ticker(skill.SpawnData.CooldownDuration, false);
        }

        void Update()
        {
            foreach (var skill in skills)
                if (skill.SpawnData.CooldownTicker.HasRemainingDuration)
                    skill.SpawnData.CooldownTicker.Tick(Time.deltaTime);
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