using ARPG.Enums;
using ARPG.Pawns;
using System;
using TeppichsTools.Logging;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.GUI
{
    public class UpdateResourceGUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI percentageText;
        [SerializeField] private TextMeshProUGUI currentText;
        [SerializeField] private Image resourceImage;

        [SerializeField] private Pawns.Pawn character;
        [SerializeField] private Resource resourceToUpdate;
        [SerializeField] private StatName statToUpdate;

        private float max;
        private float current;

        private void OnEnable()
        {
            if (character)
                if (character.stats.TryGetValue(statToUpdate, out StatScore stat) && character.resources.TryGetValue(resourceToUpdate, out ResourceScore resource))
                {
                    max = stat.MaxValue;
                    current = resource.CurrentValue;

                    stat.maxHasChanged += UpdateMax;
                    resource.currentHasChanged += UpdateCurrent;

                    UpdateGUI(max, current);
                }
        }

        private void OnDisable()
        {
            if (character)
                if (character.stats.TryGetValue(statToUpdate, out StatScore stat) && character.resources.TryGetValue(resourceToUpdate, out ResourceScore resource))
                {
                    stat.maxHasChanged -= UpdateMax;
                    resource.currentHasChanged -= UpdateCurrent;
                }
        }

        private void Start()
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        private void UpdateMax(float max)
        {
            if (this.max != max)
            {
                this.max = max;
                UpdateGUI(max, current);
            }
        }

        private void UpdateCurrent(float current)
        {
            if (this.current != current)
            {
                this.current = current;
                UpdateGUI(max, current);
            }
        }

        void UpdateGUI(float max, float current)
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
