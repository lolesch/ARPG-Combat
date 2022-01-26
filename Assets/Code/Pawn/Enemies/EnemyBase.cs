using ARPG.Combat;
using ARPG.Enums;
using ARPG.Input;
using System.Collections.Generic;
using TeppichsTools.Logging;
using UnityEngine;
using UnityEngine.UI;

namespace ARPG.Pawn.Enemy
{
    public class EnemyBase : Interactable, ITakeDamage
    {
        //[SerializeField] private Dictionary<Stat, StatScore> stats = new Dictionary<Stat, StatScore>();

        [SerializeField] private Image healthbar;

        protected override void Awake()
        {
            base.Awake();
            interactionRange = (interactionCollider as CapsuleCollider).radius;

            //stats.Add(Stat.Health, new StatScore(100, 0, 0));
        }

        void Update()
        {
            //stats.TryGetValue(Stat.Health, out StatScore health);

            //healthbar.fillAmount = health.current / health.max;
        }

        public void TakeDamage(float damage)
        {
            //stats.TryGetValue(Stat.Health, out StatScore health);

            //health.current -= damage;
            EditorDebug.Log($"{this.name} took {damage} damage");

            //healthbar.gameObject.SetActive(0 < health.current && health.current < health.max);
        }
    }
}