using ARPG.Container;
using ARPG.Input;
using ARPG.Pawns;
using ARPG.Tools;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ARPG.Combat
{
    [Serializable]
    public class SkillSpawner : MonoBehaviour
    {
        private SpawnData data;

        //[SerializeField] private bool[] quickCastSettings = new bool[6];

        [SerializeField] private PlayerController player;
        [SerializeField] private Transform caster;

        private void OnDestroy() => InputReceiver.Instance.OnSetCasting -= TryCast;
        private void Awake() => InputReceiver.Instance.OnSetCasting += TryCast;

        public void TryCast(int index)
        {
            if (GetValidData(index))
            {
                if (data.CooldownTicker.HasRemainingDuration)
                    return;

                if (player.resources.TryGetValue(Enums.ResourceName.ManaCurrent, out ResourceScore mana))
                    if (data.ManaCost <= mana.CurrentValue)
                    {
                        data.CooldownTicker.Start();
                        mana.AddToCurrentValue(-data.ManaCost);

                        // Spawn multiple projectiles:
                        for (int i = 0; i < data.ProjectileAmount; i++)
                            // TODO what about min spawn distance?

                            //TODO FIX ROTATIONS!
                            if (data.SpawnAtCursor)
                                SpawnDamageShape(CalculateSpawnPosition().Item1, CalculateSpawnPosition().Item2);
                            else
                                SpawnDamageShape(caster.position, CalculateSpawnPosition().Item2);
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

            void SpawnDamageShape(Vector3 spawnPosition, Vector3 direction)
            {
                data.Projectile.gameObject.SetActive(false);

                // TODO: caster.rotation might not be the target direction
                // => Quaternion.LookRotation(directionVector)
                var shape = Instantiate(data.Projectile.gameObject, spawnPosition, Quaternion.LookRotation(direction), transform.root).GetComponent<Projectile>();

                shape.data = data;

                shape.gameObject.SetActive(true);

                Debug.Break();
            }

            (Vector3, Vector3) CalculateSpawnPosition()
            {
                var pointerHit = caster.position;

                var pointerPosition = Pointer.current.position.ReadValue();

                Ray ray = Camera.main.ScreenPointToRay(pointerPosition);

                if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, ~0))
                {
                    // if hit == Enemy => queue cast untill in range
                    // cancle queue on other cast input

                    // rotate to target

                    pointerHit = hit.point;

                    var dist = XZPlane.Magnitude(pointerHit, caster.position);

                    if (data.SpawnRange < dist)
                    {
                        var maxCastDistancePosition = caster.position + (XZPlane.Direction(pointerHit, caster.position) * data.SpawnRange);

                        var rayOrigin = new Vector3(maxCastDistancePosition.x, 100, maxCastDistancePosition.z);

                        ray = new Ray(rayOrigin, Vector3.down);

                        if (Physics.Raycast(ray, out hit))
                            pointerHit = hit.point;
                    }
                }

                // TODO return the direction to turn in or the rotation itself
                return (pointerHit, Quaternion.Euler(0, DirectionAngle(index), 0) * caster.forward);
            }
        }

        private float DirectionAngle(int index)
        {
            if (data.ProjectileAmount <= 1)
                return 0;
            else if (data.ShapeAngle % 360 == 0)
                return (data.ShapeAngle / data.ProjectileAmount) * index;
            else
                return data.ShapeAngle * .5f - (data.ShapeAngle / (data.ProjectileAmount - 1) * index);
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
        //    shape.target = data.SpawnAtCursor ? position : transform.position + projectileDirections[index] * data.MaxDistance;
        //    shape.GetComponentInChildren<Canvas>().transform.localScale = new Vector3(data.ProjectileRadius * 2, data.ProjectileRadius * 2, 0);
        //    #endregion
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
