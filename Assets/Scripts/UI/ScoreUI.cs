using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private IntEventChannelSO scoreChannel;

    private int currentScore;
    
    private void Start()
    {
        scoreText.text = currentScore.ToString(); 
        
        scoreChannel.OnEventRaised += UpdateScore;
    }

    private void UpdateScore(int pointsToAdd)
    {
        currentScore += pointsToAdd;
        scoreText.text = currentScore.ToString();
    }
    
    private void OnDestroy() 
    {
        if (scoreChannel != null)
        {
            scoreChannel.OnEventRaised -= UpdateScore;
        }
    }
}