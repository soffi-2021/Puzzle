using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ArchitecturalWorks : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject[] castles;
    public GameObject AssemblyPanel;
    public TextMeshProUGUI winText; 
    public SceneChanger sceneChanger; 

    private int scoreToWin = 7;
    private int score;
    private bool gameLost = false;

    

    private void Start()
    {
        Initialize();
        LaunchGame();
    }

    public void IncrementScore()
    {
        if (++score == scoreToWin)
            Win();
    }

    protected void Initialize()
    {
        score = 0;
        AssemblyPanel.SetActive(false);
        foreach (var castle in castles)
        {
            castle.SetActive(false);
        }
        winText.text = "";
    }

    protected void LaunchGame()
    {
        AssemblyPanel.SetActive(true);
        castles[Random.Range(0, castles.Length)].SetActive(true);
    }

    protected void Win()
    {   
        if (!gameLost)
        {
            float timeDuration = 1.5f;
            Color32 mainPanelWinColor = Color.green;
            StartCoroutine(FadeColor(mainPanelWinColor, timeDuration, 1.0f));
            winText.text = "Win!";
        }
    }

    public void Loose()
    {
        gameLost = true;
        DragDropManager dragDropManager = GetComponentInChildren<DragDropManager>();
        dragDropManager.HandleTimeExpired();
        float timeDuration = 1.5f;
        Color mainPanelLooseColor = Color.red;
        StartCoroutine(FadeColor(mainPanelLooseColor, timeDuration, 1.5f));
        winText.text = "Loose:(";
    }

    private IEnumerator FadeColor(Color32 desiredColor, float fadeDuration, float hideDelay)
    {
        Color32 initialColor = mainPanel.GetComponent<Image>().color;
        float t = fadeDuration;
        while ((t -= Time.deltaTime) > 0.0f)
        {
            float tNormalized = (((t - 0) * (1.0f - 0)) / (fadeDuration - 0)) + 0;
            mainPanel.GetComponent<Image>().color = Color32.Lerp(initialColor, desiredColor, 1-tNormalized);
            yield return null;
        }
        mainPanel.GetComponent<Image>().color = desiredColor;
        yield return new WaitForSeconds(hideDelay);
        sceneChanger.LoadMainMenu();
    }
}
