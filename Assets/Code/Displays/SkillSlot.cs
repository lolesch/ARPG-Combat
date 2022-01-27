using UnityEngine;
using UnityEngine.UI;

namespace ARPG.GUI
{
    public class SkillSlot : MonoBehaviour
    {
        [SerializeField] private Image icon;
        [SerializeField] private Image cooldown;

        public void SetIcon(Sprite sprite) => icon.sprite = sprite;
        public void SetCooldownFillAmount(float fillAmount) => this.cooldown.fillAmount = fillAmount;
    }
}
