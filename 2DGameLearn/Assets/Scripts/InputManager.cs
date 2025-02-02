using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Bullet Settings")]
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
                                      
    private FallingWord currentTarget = null;
    void Update()
    {
        if (GameManager.Instance != null &&
        GameManager.Instance.gameOverText != null &&
        GameManager.Instance.gameOverText.gameObject.activeSelf)
        {
            return;
        }
        // Process all characters typed this frame.
        foreach (char c in Input.inputString)
        {
            ProcessInput(c);
        }
    }

    void ProcessInput(char inputChar)
    {
        // If we already have a target word, only send letters to that word.
        if (currentTarget != null)
        {
            if (currentTarget.CheckLetter(inputChar))
            {
                // Spawn a bullet effect toward the current target.
                SpawnBullet(currentTarget.transform.position);
                // If the word was fully typed, clear the target.
                // (Since the word destroys itself on completion, we can simply reset our reference
                if (currentTarget == null || currentTarget.gameObject == null)
                {
                    currentTarget = null;
                }
            }
            // (Optional) If the letter was wrong, you might add error feedback.
        }
        else
        {
            // No current target – search all falling words for one whose first letter matches.
            FallingWord[] words = FindObjectsOfType<FallingWord>();
            FallingWord candidate = null;
            float lowestY = float.MaxValue; // We choose the word closest to the bottom.
            foreach (FallingWord word in words)
            {
                if (word.fullWord.Length > 0 &&
                char.ToLower(word.fullWord[0]) == char.ToLower(inputChar))
                {
                    if (word.transform.position.y < lowestY)
                    {
                        candidate = word;
                        lowestY = word.transform.position.y;
                    }
                }
            }
            if (candidate != null)
            {
                currentTarget = candidate;
                // Process the first letter for this target.
                if (currentTarget.CheckLetter(inputChar))
                {
                    SpawnBullet(currentTarget.transform.position);
                }
            }
        }
    }

    /// <summary>
    /// Instantiates a bullet that will travel toward the target word.
    /// </summary>
    void SpawnBullet(Vector3 targetPosition)
    {
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            TypingBullet bulletScript = bullet.GetComponent<TypingBullet>();
            if (bulletScript != null)
            {
                bulletScript.SetTarget(targetPosition);
            }
        }
    }
}
