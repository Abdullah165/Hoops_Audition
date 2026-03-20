using UnityEngine;

public class BallRespawner : MonoBehaviour
{
    private Vector3 startPosition;
    private Rigidbody rb;

    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("OutOfBounds"))
        {
            transform.position = startPosition;
            
            rb.linearVelocity = Vector3.zero; 
            rb.angularVelocity = Vector3.zero;
        }
    }
}