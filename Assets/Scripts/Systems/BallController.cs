using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform holdPosition;
    [SerializeField] private FloatEventChannelSO powerChannel;
    [SerializeField] private AudioEventChannelSO sfxChannel;
    [SerializeField] private VoidEventChannelSO throwChannel;
    [SerializeField] private AudioClip holdBall;

    [SerializeField] private float throwForce = 8f;
    [SerializeField] private float maxChargeMultiplier = 2f;
    [SerializeField] private float chargeRate = 1.5f;
    [SerializeField] private float upwardArcForce = 4f;
    [SerializeField] private float pickupRange = 5f;

    private Camera playerCamera;
    private Rigidbody heldBallRb;
    private bool isHoldingBall = false;
    private float currentCharge = 0f;

    private void Awake()
    {
        playerCamera =  Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isHoldingBall) TryPickupBall();
        
        if (Input.GetMouseButton(0) && isHoldingBall)
        {
            currentCharge += Time.deltaTime * chargeRate;
            currentCharge = Mathf.Clamp(currentCharge, 0f, maxChargeMultiplier);
            
            if (powerChannel != null)
            {
                powerChannel.RaiseEvent(currentCharge / maxChargeMultiplier);
            }
        }

        if (Input.GetMouseButtonUp(0) && isHoldingBall)
        {
            ThrowBall();
        }

        if (isHoldingBall && heldBallRb != null)
        {
            heldBallRb.position = holdPosition.position;
        }
    }

    private void TryPickupBall()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                heldBallRb = hit.collider.GetComponent<Rigidbody>();
                heldBallRb.isKinematic = true;
                heldBallRb.useGravity = false;
                isHoldingBall = true;
                currentCharge = 0f; 
                
                if (sfxChannel != null && holdBall != null)
                {
                    sfxChannel.RaiseEvent(holdBall, transform.position);
                }
            }
        }
    }

    private void ThrowBall()
    {
        isHoldingBall = false;
        heldBallRb.isKinematic = false;
        heldBallRb.useGravity = true;
        
        float finalArcForce = upwardArcForce + (upwardArcForce * currentCharge);
        Vector3 force = (playerCamera.transform.forward * throwForce) + (playerCamera.transform.up * finalArcForce);

        heldBallRb.AddForce(force, ForceMode.Impulse);

        heldBallRb = null;
        currentCharge = 0f;
        
        if (powerChannel != null)
        {
            powerChannel.RaiseEvent(0f);
        }
        
        if (throwChannel != null)
        {
            throwChannel.RaiseEvent();
        }
    }
}