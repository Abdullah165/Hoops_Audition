using UnityEngine;

public class BallSFX : MonoBehaviour
{
    [SerializeField] private VoidEventChannelSO ballTouchGroundChannel;
    [SerializeField] private VoidEventChannelSO ballTouchRimChannel;
    [SerializeField] private VoidEventChannelSO ballTouchBackboardChannel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (collision.relativeVelocity.magnitude > 1.0f)
            {
                if (ballTouchGroundChannel != null)
                {
                    ballTouchGroundChannel.RaiseEvent();
                }
            }
        }

        else if (collision.gameObject.CompareTag("Rim"))
        {

            if (ballTouchRimChannel != null)
                ballTouchRimChannel.RaiseEvent();
        }

        else if (collision.gameObject.CompareTag("Backboard"))
        {

            if (ballTouchBackboardChannel != null)
                ballTouchBackboardChannel.RaiseEvent();
        }
    }
}