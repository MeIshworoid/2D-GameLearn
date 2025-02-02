using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypingBullet : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 target;

    /// <summary>
    /// Called by the InputManager to set the target destination.
    /// </summary>
    public void SetTarget(Vector3 targetPosition)
    {
        target = targetPosition;
        // Optional: rotate the bullet to face the target.
        Vector3 direction = (target - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    void Update()
    {
        // Move the bullet toward the target.
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        // If the bullet is close enough, destroy it.
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
