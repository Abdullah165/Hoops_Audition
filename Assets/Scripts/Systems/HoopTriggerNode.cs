using UnityEngine;

public class HoopTriggerNode : MonoBehaviour
{
    private HoopDetector manager;
    private bool isTop;

    public void Initialize(HoopDetector manager, bool isTop)
    {
        this.manager = manager;
        this.isTop = isTop;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && manager != null)
        {
            manager.RegisterTrigger(isTop, other);
        }
    }
}