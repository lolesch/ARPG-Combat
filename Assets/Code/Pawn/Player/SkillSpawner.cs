using ARPG.Container;
using ARPG.Input;
using UnityEngine;
using System;
using TeppichsTools.Logging;
using ARPG.Pawns;
using UnityEngine.InputSystem;
using ARPG.Tools;

namespace ARPG.Combat
{
    [Serializable]
    public class SkillSpawner : MonoBehaviour
    {
        //private Vector3[] projectileDirections;
        private SpawnData data;

        //[SerializeField] private bool[] quickCastSettings = new bool[6];

        [SerializeField] private PlayerController player;
        [SerializeField] private Transform caster;

        private void OnDestroy() => InputTranslator.Instance.castSkill -= TryCast;
        private void Awake() => InputTranslator.Instance.castSkill += TryCast;

        public void TryCast(int index)
        {
            if (GetValidData(index))
            {
                if (data.CooldownTicker.IsTicking)
                    return;

                if (player.resources.TryGetValue(Enums.Resource.ManaCurrent, out ResourceScore current))
                    if (data.ResourceCost <= current.CurrentValue)
                    {
                        data.CooldownTicker.Restart();
                        current.AddToCurrentValue(-data.ResourceCost);

                        //CalculateDirections(XZDirection(target, transform.position));
                        //
                        //for (int i = 0; i < data.AmountToSpawn; i++)
                        //    SpawnObject(target, i);

                        SpawnDamageShape(data.SpawnAtCursor ? CalculateSpawnPosition() : caster.position);
                    }
            }

            bool GetValidData(int index)
            {
                if (!player.skills[index].SpawnData)
                    return false;

                data = player.skills[index].SpawnData;
                player.SetInteractionRange(data.SpawnAtCursor ? data.SpawnRange : data.OuterRadius);

                return true;
            }

            void SpawnDamageShape(Vector3 spawnPosition)
            {
                data.Projectile.gameObject.SetActive(false);
                var shape = Instantiate(data.Projectile.gameObject, spawnPosition, caster.rotation, transform.root).GetComponent<Projectile>();

                shape.data = data;

                shape.gameObject.SetActive(true);
            }

            Vector3 CalculateSpawnPosition()
            {
                Vector3 pointerPosition = caster.position;

                var screenPoint = Pointer.current.position.ReadValue();

                Ray ray = Camera.main.ScreenPointToRay(screenPoint);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~0))
                {
                    // if hit == Enemy => queue cast untill in range
                    // cancle queue on other cast input

                    // rotate to target

                    pointerPosition = hit.point;

                    var dist = XZPlane.Magnitude(pointerPosition, caster.position);

                    if (data.SpawnRange < dist)
                    {
                        var maxCastDistancePosition = caster.position + (XZPlane.Direction(pointerPosition, caster.position) * data.SpawnRange);

                        var rayOrigin = new Vector3(maxCastDistancePosition.x, 100, maxCastDistancePosition.z);

                        ray = new Ray(rayOrigin, Vector3.down);

                        if (Physics.Raycast(ray, out hit))
                            pointerPosition = hit.point;
                    }
                }

                return pointerPosition;
            }
        }

        //private void SpawnObject(Vector3 targetPos, int index)
        //{
        //    Vector3 position = CalculateStartPosition(targetPos, index);
        //
        //    DamageShape shape = Instantiate(data.DamageShape.gameObject, position, Quaternion.identity, this.transform).GetComponent<DamageShape>();
        //
        //    EditorDebug.Log("instantiated a damage shape");
        //
        //    #region travel behaviour
        //    shape.projectileSpeed = data.ProjectileSpeed;
        //    shape.GetComponent<CapsuleCollider>().radius = data.ProjectileRadius;
        //    shape.spawnPosition = position;
        //    shape.target = data.SpawnAtCursor? position : transform.position + projectileDirections[index] * data.MaxDistance;
        //    shape.GetComponentInChildren<Canvas>().transform.localScale = new Vector3(data.ProjectileRadius* 2, data.ProjectileRadius* 2, 0);
        //    #endregion
        //}

        //private void CalculateDirections(Vector3 targetDirection)
        //{
        //    projectileDirections = new Vector3[data.AmountToSpawn];
        //
        //    for (int i = 0; i < projectileDirections.Length; i++)
        //        projectileDirections[i] = Quaternion.Euler(0, DirectionAngle(i), 0) * targetDirection;
        //}

        //private float DirectionAngle(int index)
        //{
        //    if (data.AmountToSpawn <= 1)
        //        return 0;
        //    else if (data.FullAngle % 360 == 0)
        //        return (data.FullAngle / data.AmountToSpawn) * index;
        //    else
        //        return data.FullAngle * .5f - (data.FullAngle / (data.AmountToSpawn - 1) * index);
        //}

        //private Vector3 CalculateStartPosition(Vector3 targetPos, int index)
        //{
        //    /// calculate the position at the skills max range distance towards the cursors position
        //    if (data.SpawnAtCursor)
        //        return transform.position + (targetPos - transform.position).normalized * Mathf.Min(data.MaxDistance, Vector3.Distance(targetPos, transform.position));
        //    else
        //        return 0 < data.MinDistance ? transform.position + projectileDirections[index] * data.MinDistance : transform.position;
        //}

        //private void CalculateIndicators(SpawnData behaviour, Vector3 targetPos)
        //{
        //    float fullAngle = behaviour.FullAngle;
        //    float range = behaviour.MaxDistance;
        //    float diameter = behaviour.ProjectileRadius * 2;
        //
        //    shapeIndicator.transform.localRotation = Quaternion.Euler(0, 0, fullAngle * .5f - Vector3.SignedAngle(Vector3.back, targetPos - transform.position, Vector3.up));
        //
        //    shapeIndicator.transform.localScale = fullAngle < 360 ? new Vector2(range * 2, range * 2) : new Vector2(diameter, diameter);
        //
        //    shapeIndicator.fillAmount = fullAngle / 360f;
        //
        //    //see CalculatePosition() - could use that * (1,0,1)?
        //    shapeIndicator.transform.position = behaviour.SpawnAtCursor ? transform.position + XZDirection(targetPos, transform.position) * Mathf.Min(behaviour.MaxDistance, XZDistance(targetPos, transform.position)) : transform.position;
        //
        //    rangeIndicator.transform.localScale = new Vector2(range * 2, range * 2);
        //
        //    directionIndicator = new Image[behaviour.AmountToSpawn];
        //    pool.ReleaseAll();
        //
        //    for (int i = 0; i < behaviour.AmountToSpawn; i++)
        //    {
        //        directionIndicator[i] = pool.Next();
        //        directionIndicator[i].transform.localScale = new Vector2(diameter, Mathf.Min(range, XZDistance(targetPos, transform.position)));
        //
        //        float worldRotationAdjustment = Vector3.SignedAngle(Vector3.forward, targetPos - transform.position, Vector3.up);
        //
        //        directionIndicator[i].transform.localRotation = Quaternion.Euler(0, 0, DirectionAngle(i) - worldRotationAdjustment);
        //    }
        //}
    }
}
