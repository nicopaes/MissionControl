
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothSpeed = 0.2f;
    public Vector3 offset;

    private Vector3 _currentVelocity;

    
    private void Update()
    {
        Vector3 realTarget = new Vector3(target.position.x, target.position.y, transform.position.z) + offset;
        transform.position = Vector3.SmoothDamp(transform.position, realTarget, ref _currentVelocity, smoothSpeed);  
    }
    public void SetTarget(Transform nextTarget)
    {
        target = nextTarget;
    }
}
