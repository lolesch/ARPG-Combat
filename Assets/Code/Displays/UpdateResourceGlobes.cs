using TMPro;
using UnityEngine;

namespace ARPG.GUI
{
    public class UpdateResourceGlobes : UpdateHealthbarGUI
    {
        [SerializeField] protected TextMeshProUGUI percentageText;
        [SerializeField] protected TextMeshProUGUI currentText;

        protected override void UpdateGUI(float max, float current)
        {
            if (percentageText)
                percentageText.text = $"{current:0} %";

            if (currentText)
                currentText.text = $"{max * current / 100:0} / {max:0}";

            if (resourceImage)
                resourceImage.fillAmount = current / 100;
        }
    }
}
