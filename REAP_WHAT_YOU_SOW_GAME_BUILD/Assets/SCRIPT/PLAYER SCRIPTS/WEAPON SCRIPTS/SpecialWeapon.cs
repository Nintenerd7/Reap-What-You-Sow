using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialWeapon : MonoBehaviour
{
    public float FireRate = 1f;
    float timeUntilFire;
    public GameObject BulletPrefab;
    public Transform firePoint;
    public float fireforce = 20f;

    [SerializeField] private Vector3 initialLocalPos;
    float time = 0;
    float kickbackTime = 0;
    bool kickbackRunning = false;

    bool shootingRunning = false;

    [SerializeField] AudioClip shoot;
    //

    private void Start()
    {
        initialLocalPos = transform.localPosition;
    }

    public void Fire()
    {
        //fires projectile 
        if (!shootingRunning)
            StartCoroutine(Shoot(FireRate));
        //timeUntilFire = Time.fixedDeltaTime * FireRate;//time for bullet to fire.
        //
    }

    IEnumerator Shoot(float _delayBetweenShots)
    {
        shootingRunning = true;

        AudioSource.PlayClipAtPoint(shoot, transform.position);

        Quaternion _bulletRotation = Quaternion.Euler(0,0,-90);

        GameObject bullet = Instantiate(BulletPrefab, firePoint.position, transform.parent.rotation * _bulletRotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(firePoint.right * 2, ForceMode2D.Impulse);

        transform.root.GetComponent<Rigidbody2D>().AddForce(-firePoint.right * 2, ForceMode2D.Impulse);

        kickbackTime = 0;
        time = 0;
        if (!kickbackRunning)
            StartCoroutine(SmoothMove());
        yield return new WaitForSeconds(_delayBetweenShots);
        EnergyBarEvents.CallOnSpecialShot();
        shootingRunning = false;
    }

    IEnumerator SmoothMove()
    {
        kickbackRunning = true;
        while (kickbackTime <= 1)
        {

            transform.localPosition = Vector3.Lerp(initialLocalPos, initialLocalPos + new Vector3(-0.1f, 0), kickbackTime);
            kickbackTime += Time.fixedDeltaTime;
            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = initialLocalPos + new Vector3(-0.1f, 0);

        while (time <= 1)
        {
            time += Time.fixedDeltaTime;
            transform.localPosition = Vector3.Lerp(transform.localPosition, initialLocalPos, Mathf.SmoothStep(0, 1, time / 2));
            yield return new WaitForEndOfFrame();
        }

        transform.localPosition = initialLocalPos;

        kickbackRunning = false;

    }
}
