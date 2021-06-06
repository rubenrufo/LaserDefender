using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Enemy Shooting")]
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBtwShots = 0.2f;
    [SerializeField] float maxTimeBtwShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float xVelLaserRand = 0f;
    [SerializeField] float yVelLaser = 10f;
    float xVelLaser;
    Quaternion laserRotation;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.75f;

    [Header("Life and Death")]
    [SerializeField] float health = 100f;
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationExplosion = 1f;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0,1)] float deathSFXVolume = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        shotCounter = Random.Range(minTimeBtwShots, maxTimeBtwShots);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter=Random.Range(minTimeBtwShots, maxTimeBtwShots);
        }
    }

    private void Fire()
    {
        xVelLaser = Random.Range(-xVelLaserRand, xVelLaserRand);
        laserRotation.SetFromToRotation(new Vector3(0, -1, 0), new Vector3(xVelLaser, -yVelLaser, 0));
        GameObject laser = Instantiate(projectile, transform.position, laserRotation) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelLaser, -yVelLaser);
        AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }        // Protecting against NULL situations.  
        ProcessHit(damageDealer);

    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            EnemyDies();
        }
    }

    private void EnemyDies()
    {
        Destroy(gameObject);
        GameObject explosion = Instantiate(deathVFX, transform.position, transform.rotation);
        Destroy(explosion, durationExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }
}
