using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeaponBehavior : WeaponBehavior
{
    public Action BulletReturned;
    
    private Coroutine fireBulletCoroutine = null;
    private const float bulletLimit = 0.55f;
    private Vector3 originalPosition;

    private void Awake()
    {
        DamageMultiplier = 1f;
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
    }

    private IEnumerator FireBulletRoutine()
    {
        transform.localPosition = Vector3.zero;
        
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

        PostBulletReturned();
        ResetBullet();
    }

    private void ResetBullet()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    private void PostBulletReturned()
    {
        if(BulletReturned != null)
        {
            BulletReturned.Invoke();
        }
    }
}
