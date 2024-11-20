using UnityEngine;

public class CanvasController : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
