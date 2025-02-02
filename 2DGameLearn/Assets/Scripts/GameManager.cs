using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Header("Game Settings")]
    public int maxMissed = 6;
    [HideInInspector] public int missedCount = 0;
    [Header("UI References")]
    public TMP_Text missedText;
    public TMP_Text gameOverText;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// Call this when a word is missed (falls off the bottom).
    /// </summary>
    public void WordMissed()
    {
        missedCount++;
        if (missedText != null)
            missedText.text = "Missed: " + missedCount;
        if (missedCount >= maxMissed)
        {
            GameOver();
        }
    }
    void GameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "Game Over!";
        }
    }
}
