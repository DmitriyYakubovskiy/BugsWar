using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float speed = 5f;

    private Rigidbody rb;

    public Vector3 TargetPosition
    {
        get => targetPosition;
        set
        {
            targetPosition = value;
            IsMoved = true;
        }
    }
    public bool IsMoved {  get; private set; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log(targetPosition);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Debug.Log("Достигнута целевая позиция!");
            IsMoved = false;
            return;
        }
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition - transform.position).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.deltaTime);
    }
}
