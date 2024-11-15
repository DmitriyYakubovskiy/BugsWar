using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform point;
    private Camera camera;

    private void Start()
    {
        camera = GetComponent<Camera>();
        camera.transform.LookAt(point);
    }
}
