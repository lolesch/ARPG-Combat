using ARPG.Pawns;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.GUI
{
    public class SkillHUD : MonoBehaviour
    {
        [SerializeField] private List<SkillSlot> skillSlots = new(6);
        [SerializeField] private PlayerController player;

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
