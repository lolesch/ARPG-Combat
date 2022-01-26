using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawn.Movement
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] protected Transform target;
        [Range(1f, 50f)]
        [SerializeField] protected float slerpSpeed = 20f;
        [Range(0f, 1f)]
        [SerializeField] protected float accuracyRadius = .1f;

        void OnEnable()
        {
            if (!target)
                EditorDebug.LogWarning($"Camera \t Missing FollowTarget on {gameObject.name}");
        }

        void LateUpdate()
        {
            if (target && Vector3.Distance(transform.position, target.position) >= accuracyRadius)
                transform.position = Vector3.Slerp(transform.position, target.position, Time.deltaTime * slerpSpeed);
        }
    }
}
