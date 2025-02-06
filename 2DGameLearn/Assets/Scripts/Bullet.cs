using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();

    // Update is called once per frame
    void Update() => transform.right = _rb.velocity;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Target"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
