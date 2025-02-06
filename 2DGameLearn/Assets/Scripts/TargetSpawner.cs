using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private Sprite[] _targetSprite;

    [SerializeField] private BoxCollider2D _boxCollider;
    [SerializeField] private GameObject _targetPrefab;
    [SerializeField] private float _coolDown;

    private int _targetCreated;
    private int _targetMilestone;

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = _coolDown;
            _targetCreated++;

            if (_targetCreated > _targetMilestone && _coolDown > .05f)
            {
                _targetMilestone += 10;
                _coolDown -= .3f;
            }

            GameObject newTarget = Instantiate(_targetPrefab);

            float randomX = Random.Range(_boxCollider.bounds.min.x, _boxCollider.bounds.max.x);
            newTarget.transform.position = new Vector2(randomX, transform.position.y);

            int randomIndex = Random.Range(0, _targetSprite.Length);
            newTarget.GetComponent<SpriteRenderer>().sprite = _targetSprite[randomIndex];
        }
    }
}
