using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator _gunAnimator;
    [SerializeField] private Transform _gun;
    [SerializeField]
    private float _gunDistance = 1.5f;

    private bool isGunFacingRight = true;

    [Header("Bullet")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletSpeed;

    void Update()
    {
        // get the world space of mouse position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // direction mouse position and gun position
        Vector3 direction = mousePos - transform.position;

        // rotate the gun
        _gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(_gunDistance, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot(direction);
        }

        GunFlipController(mousePos);
    }

    private void GunFlipController(Vector3 mousePos)
    {
        if (mousePos.x < _gun.position.x && isGunFacingRight)
        {
            GunFlip();
        }
        else if (mousePos.x > _gun.position.x && !isGunFacingRight)
        {
            GunFlip();
        }
    }

    private void GunFlip()
    {
        isGunFacingRight = !isGunFacingRight;
        _gun.localScale = new Vector3(_gun.localScale.x, _gun.localScale.y * -1, _gun.localScale.z);
    }

    public void Shoot(Vector3 direction)
    {
        _gunAnimator.SetTrigger("Shoot");

        GameObject bullet = Instantiate(_bulletPrefab, _gun.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * _bulletSpeed;

        Destroy(bullet, 7);
    }
}
