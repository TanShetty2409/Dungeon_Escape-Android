using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("UIManager is null");
            }

            return _instance;
        }
    }
    public Text playerGemCountText;
    public Image selectedItem;
    public Text gemCount;
    public Image[] HealthBars;
    [SerializeField] private UINotifications notification;
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject endPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private CanvasGroup pauseCanvasGroup;
    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = " " + gemCount + "G";

    }
    public void UpdateItemSelected(int yPos)
    {
        selectedItem.rectTransform.anchoredPosition = new Vector2(selectedItem.rectTransform.anchoredPosition.x, yPos);
    }
    public void UpdateGemCount(int count)
    {
        gemCount.text = "" + count;
    }
    public void UpdateLives(int livesRemaining)
    {
        for (int i = 0; i < HealthBars.Length; i++)
        {
            if (i == livesRemaining)
            {
                HealthBars[i].enabled = i < livesRemaining;
            }
        }

    }
    public void ShowNotification(string message)
    {
        notification.Show(message);
    }
    public void FadeToBlackAndEnd()
    {
        StartCoroutine(FadeOutRoutine());
    }
    private IEnumerator FadeOutRoutine()
    {
        fadeImage.gameObject.SetActive(true);

        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.unscaledDeltaTime;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
        hudPanel.SetActive(false);
        Time.timeScale = 0f;
        endPanel.SetActive(true);
    }
    public void pauseGame()
    {
        hudPanel.SetActive(false);
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseCanvasGroup.alpha = 1f;
        pauseCanvasGroup.blocksRaycasts = true;
        pauseCanvasGroup.interactable = true;
    }

    public void resumeGame()
    {
        pauseCanvasGroup.alpha = 0f;
        pauseCanvasGroup.blocksRaycasts = false;
        pauseCanvasGroup.interactable = false;
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        hudPanel.SetActive(true);
        FindAnyObjectByType<Player>().resumeInput();
    }
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Awake()
    {
        _instance = this;
    }
}
