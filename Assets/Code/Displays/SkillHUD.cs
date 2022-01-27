using ARPG.Pawn;
using System.Collections.Generic;
using UnityEngine;

namespace ARPG.GUI
{
    public class SkillHUD : MonoBehaviour
    {
        [SerializeField] private List<SkillSlot> skillSlots = new(6);
        [SerializeField] private Player player;

        void Awake()
        {
            for (int i = 0; i < player.skills.Count; i++)
                skillSlots[i].SetIcon(player.skills[i].Icon);
        }
    }
}
