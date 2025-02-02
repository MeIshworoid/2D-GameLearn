using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FallingWord : MonoBehaviour
{
    [Header("Word Settings")]
    [Tooltip("The full word to type")]
    public string fullWord;
    private int letterIndex = 0;
    public float fallSpeed = 2f;
    [Header("UI Reference")]
    public TMP_Text wordText;

    // Flags to track completion state
    private bool completed = false;
    private bool missed = false;
    void Start()
    {
        if (wordText != null)
            wordText.text = fullWord;
    }
    void Update()
    {
        // Move the word downward
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
        // Check if the word has fallen past a threshold (adjust the value as needed)
        if (transform.position.y < -5f && !completed && !missed)
        {
            // Mark as missed and destroy; the miss will be counted in OnDisable
            missed = true;
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Checks if the next letter in the word matches the input character.
    /// Updates the displayed text (with the typed portion colored, for example).
    /// Returns true if the letter is correct.
    /// </summary>
    public bool CheckLetter(char c)
    {
        if (letterIndex < fullWord.Length && char.ToLower(fullWord[letterIndex]) == char.ToLower(c))
        {
            letterIndex++;
            if (wordText != null)
            {
                // Show the typed portion in green and the rest in the default color.
                wordText.text = "<color=green>" + fullWord.Substring(0, letterIndex) + "</color>" +
                fullWord.Substring(letterIndex);
            }
            // If the whole word has been typed correctly:
            if (letterIndex >= fullWord.Length)
            {
                completed = true;
                Destroy(gameObject);
            }
            return true;
        }
        return false;
    }
    /// <summary>
    /// When the GameObject is disabled (destroyed), if it was not completed,
    /// count it as a missed word.
    /// </summary>
    void OnDisable()
    {
        if (missed)
        {
            GameManager.Instance.WordMissed();
        }
    }
}
