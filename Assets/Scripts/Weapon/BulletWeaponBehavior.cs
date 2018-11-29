using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeaponBehavior : WeaponBehavior
{
    private Transform bulletParent;
    private Coroutine fireBulletCoroutine = null;
    private float bulletLimit = 0.45f;
    private Vector3 originalPosition;

    private void Awake()
    {
        DamageMultiplier = 1f;
        bulletParent = transform.parent;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        fireBulletCoroutine = StartCoroutine(FireBulletRoutine());
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopCoroutine(fireBulletCoroutine);
        ResetBullet();
    }

    protected override void HandleObjectReturned(object sender, ToOrFromPoolEventArgs e)
    {
        base.HandleObjectReturned(sender, e);
        ResetBullet();
    }

    private IEnumerator FireBulletRoutine()
    {
        transform.parent = null;
        originalPosition = transform.position;
        var speed = (weaponLite.HeavyReference as Weapon).WeaponSpeed;

        var distance = 0f;
        do
        {
            distance = Vector3.Distance(transform.position, originalPosition);
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            yield return null;
        }
        while(distance < bulletLimit);

        ResetBullet();
    }

    private void ResetBullet()
    {
        transform.parent = bulletParent;
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }
}
