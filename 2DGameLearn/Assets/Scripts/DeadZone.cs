using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Target"||other.tag=="Player")
        {
            Time.timeScale = 0;
            Debug.Log("You Looser...");
        }
    }
}
