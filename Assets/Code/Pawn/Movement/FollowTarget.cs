using TeppichsTools.Logging;
using UnityEngine;

namespace ARPG.Pawn.Movement
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] protected Transform target;
        [Range(1f, 100f)]
        [SerializeField] protected float lerpSpeed = 20f;
        [Range(0f, 1f)]
        [SerializeField] protected float accuracyRadius = .1f;

        protected Vector3 startPosition;

        void OnEnable()
        {
            if (!target)
                EditorDebug.LogWarning($"Camera \t Missing FollowTarget on {gameObject.name}");

            startPosition = transform.position - target.position;
        }

        void LateUpdate()
        {
            if (target && Vector3.Distance(transform.position, target.position + startPosition) >= accuracyRadius)
                transform.position = Vector3.Lerp(transform.position, target.position + startPosition, Time.deltaTime * lerpSpeed);
        }
    }
}
