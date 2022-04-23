using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target_object;
    public Vector3 offset;

    private void FixedUpdate()
    {
        transform.position = target_object.position+ offset;sdfsf
    }
}
