using UnityEngine;

public class city_hex : MonoBehaviour
{
#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        float hexSize = 1f; // Adjust this to change gizmo size
        float radius = hexSize / 2;
        Vector3 gizmoPosition = transform.position + new Vector3(0, radius * 8, 0);
        Gizmos.DrawSphere(gizmoPosition, radius);
    }

#endif
}
