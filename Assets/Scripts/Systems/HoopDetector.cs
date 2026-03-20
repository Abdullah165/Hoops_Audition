using System.Collections;
using UnityEngine;

public class HoopDetector : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO scoreChannel;
    [SerializeField] private int points = 2;
    [SerializeField] private int pointsPerGoal = 2;
    [SerializeField] private float scoreCooldown = 1f;
    
    private bool canScore = true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && canScore)
        {
            if (scoreChannel != null)
            {
                scoreChannel.RaiseEvent(pointsPerGoal);
            }
            
            StartCoroutine(SlowMotionCoroutine());
            StartCoroutine(ScoreCooldownCoroutine());
        }
    }
    
    private IEnumerator ScoreCooldownCoroutine()
    {
        canScore = false;
        yield return new WaitForSeconds(scoreCooldown);
        canScore = true;
    }

    private IEnumerator SlowMotionCoroutine()
    {
        Time.timeScale = 0.4f;
        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 1f;
    }
}