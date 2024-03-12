using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public ArchitecturalWorks minigame;
    public float timeLimit;
    public float endGameDelay;
    public TextMeshProUGUI timer;
    public GameObject mainPanel;
    public GameObject closeButton; 
    public static bool timeExpired = false;
    private bool gameEnded = false;

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(endGameDelay);
    }

    private IEnumerator StartCountdown()
    {
        float currentCountdownValue = timeLimit;
        while (currentCountdownValue > 0 && !gameEnded)
        {
            timer.text = currentCountdownValue.ToString();
            yield return new WaitForSeconds(1f);
            currentCountdownValue--;
        }
        timeExpired = true;
        gameEnded = true;
        StartCoroutine(EndGame());
        minigame.Loose();
    }

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }
}
