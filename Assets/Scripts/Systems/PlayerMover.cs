using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Transform[] positions;
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float delayBeforeMove = 1.5f; 
    [SerializeField] private VoidEventChannelSO throwChannel;

    private int currentIndex = 1;
    private float lockedYPosition; 

    private void Start()
    {
        lockedYPosition = transform.position.y; 

        if (throwChannel != null)
        {
            throwChannel.OnEventRaised += OnThrowReceived;
        }
    }

    private void OnThrowReceived()
    {
        StartCoroutine(MoveAfterDelayRoutine());
    }

    private IEnumerator MoveAfterDelayRoutine()
    {
        yield return new WaitForSeconds(delayBeforeMove);
        
        currentIndex++;
        if (currentIndex >= positions.Length)
        {
            currentIndex = 0;
        }
    }

    private void Update()
    {
        if (positions.Length == 0) return;

        Transform target = positions[currentIndex];
        
        Vector3 targetPosition = new Vector3(target.position.x, lockedYPosition, target.position.z);
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
        
        Quaternion targetRotation = Quaternion.Euler(transform.eulerAngles.x, target.eulerAngles.y, transform.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
    }

    private void OnDestroy()
    {
        if (throwChannel != null)
        {
            throwChannel.OnEventRaised -= OnThrowReceived;
        }
    }
}