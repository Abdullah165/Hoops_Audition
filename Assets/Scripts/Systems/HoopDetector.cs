using System.Collections;
using UnityEngine;

public class HoopDetector : MonoBehaviour
{
    [SerializeField] private IntEventChannelSO scoreChannel;
    [SerializeField] private int pointsPerGoal = 2;
    [SerializeField] private float scoreCooldown = 1f;

    [Header("Hoop Triggers")]
    [SerializeField] private GameObject firstHoop;  
    [SerializeField] private GameObject secondHoop; 

    private bool hasPassedTop = false;
    private bool canScore = true;
    private Coroutine topTimeoutCoroutine;

    private void Start()
    {
        if (firstHoop.TryGetComponent(out HoopTriggerNode topNode))
        {
            topNode.Initialize(this, true); 
        }

        if (secondHoop.TryGetComponent(out HoopTriggerNode bottomNode))
        {
            bottomNode.Initialize(this, false);
        }
    }

    public void RegisterTrigger(bool isTopNode, Collider ball)
    {
        if (!canScore) return;

        if (isTopNode)
        {
            hasPassedTop = true;

            if (topTimeoutCoroutine != null) StopCoroutine(topTimeoutCoroutine);
            topTimeoutCoroutine = StartCoroutine(ResetTopTimeout());
        }
        else if (!isTopNode && hasPassedTop)
        {
            hasPassedTop = false;

            if (scoreChannel != null)
            {
                scoreChannel.RaiseEvent(pointsPerGoal);
            }

            StartCoroutine(ScoreCooldownCoroutine());
        }
    }

    private IEnumerator ResetTopTimeout()
    {
        yield return new WaitForSeconds(1.5f);
        hasPassedTop = false;
    }

    private IEnumerator ScoreCooldownCoroutine()
    {
        canScore = false;
        yield return new WaitForSeconds(scoreCooldown);
        canScore = true;
    }
}