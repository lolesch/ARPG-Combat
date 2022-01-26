using UnityEngine;

public class MeshOfsetter : MonoBehaviour
{
    [SerializeField] private Vector3 meshOffset = Vector3.zero;

    private void Awake()
    {
        Vector3 root = transform.parent.position;

        this.transform.position = root + meshOffset;
    }

    private void OnValidate()
    {
        Awake();
    }
}
