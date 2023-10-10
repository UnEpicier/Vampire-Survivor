using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 _offset = new Vector3(0f, 0f, -10f);
    private float _smoothTime = 10f;
    private Vector3 _velocity = Vector3.zero;

    [SerializeField] private Transform _target;

    private void FixedUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, _smoothTime * Time.deltaTime);
    }
}
