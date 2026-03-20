using UnityEngine;
using UnityEngine.UI;

public class PowerUI : MonoBehaviour
{
    [SerializeField] private Image powerImage;
    [SerializeField] private FloatEventChannelSO powerChannel;

    private void Start()
    {
        powerImage.fillAmount = 0f;
        powerChannel.OnEventRaised += UpdatePowerFill; 
    }

    private void UpdatePowerFill(float fillAmount) 
    {
        powerImage.fillAmount = fillAmount;
    }

    private void OnDestroy()
    {
        if (powerChannel != null)
        {
            powerChannel.OnEventRaised -= UpdatePowerFill;
        }
    }
}