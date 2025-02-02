using System.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class WordSpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    public GameObject wordPrefab; // Assign the Word prefab here.
    public float spawnInterval = 2f;
    public float xMin = -7f;
    public float xMax = 7f;
    public float ySpawn = 6f; // Y position to spawn the word.
    [Header("Word List")]
    public string[] wordList = new string[] { "apple", "banana", "orange", "grape", "watermelon" };
    void Start()
    {
        StartCoroutine(SpawnWords());
    }

    IEnumerator SpawnWords()
    {
        while(true)
        {
            SpawnWord();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnWord()
    {
        // Pick a random word from the list
        string randomWord = wordList[Random.Range(0, wordList.Length)];
        // Choose a random X position within the defined range
        float xPos = Random.Range(xMin, xMax);
        Vector3 spawnPos = new Vector3(xPos, ySpawn, 0);
        // Instantiate the Word prefab at the chosen position
        GameObject wordObj = Instantiate(wordPrefab, spawnPos, Quaternion.identity);
        // Set the word’s text via its Word component
        FallingWord wordScript = wordObj.GetComponent<FallingWord>();
        if (wordScript != null)
        {
            wordScript.fullWord = randomWord;
        }
    }
}
