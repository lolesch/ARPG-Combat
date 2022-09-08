using ARPG.Pawns;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.Displays
{
    public class SkillHUD : MonoBehaviour
    {
        [SerializeField] private PlayerController player;
        [SerializeField] private List<SkillSlot> skillSlots = new(6);

        private void OnEnable()
        {
            if (!player)
                this.enabled = false;
        }

        void LateUpdate()
        {
            for (int i = 0; i < player.skills.Count; i++)
            {
                skillSlots[i].SetIcon(player.skills[i].Icon);
                skillSlots[i].SetCooldownFillAmount(1 - player.skills[i].SpawnData.CooldownTicker.Progress01);
            }
        }
    }
}
