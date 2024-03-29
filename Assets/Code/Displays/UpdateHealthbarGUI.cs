﻿using ARPG.Enums;
using ARPG.Pawns;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.Displays
{
    public class UpdateHealthbarGUI : MonoBehaviour
    {
        [SerializeField] protected Image resourceImage;

        [SerializeField] protected Pawn pawn;
        [SerializeField] protected ResourceName resourceToUpdate;
        [SerializeField] protected StatName statToUpdate;

        protected float max;
        protected float current;

        protected void OnEnable()
        {
            if (pawn)
                if (pawn.stats.TryGetValue(statToUpdate, out StatScore stat) && pawn.resources.TryGetValue(resourceToUpdate, out ResourceScore resource))
                {
                    max = stat.MaxValue;
                    current = resource.CurrentValue;

                    stat.maxHasChanged += UpdateMax;
                    resource.currentHasChanged += UpdateCurrent;

                    UpdateGUI(max, current);
                }
        }

        protected void OnDisable()
        {
            if (pawn)
                if (pawn.stats.TryGetValue(statToUpdate, out StatScore stat) && pawn.resources.TryGetValue(resourceToUpdate, out ResourceScore resource))
                {
                    stat.maxHasChanged -= UpdateMax;
                    resource.currentHasChanged -= UpdateCurrent;
                }
        }

        protected void Start()
        {
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        protected void UpdateMax(StatScore stat)
        {
            if (this.max != stat.MaxValue)
            {
                this.max = stat.MaxValue;
                UpdateGUI(max, current);
            }
        }

        protected void UpdateCurrent(ResourceScore resource)
        {
            if (this.current != resource.CurrentValue)
            {
                this.current = resource.CurrentValue;
                UpdateGUI(max, current);
            }
        }

        protected virtual void UpdateGUI(float max, float current)
        {
            if (resourceImage)
                resourceImage.fillAmount = current / 100; // is this in percent?

            //if (current == 0 || max == 0)
            //    gameObject.SetActive(false);
        }
    }
}
