using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    
    [Header("Player Movement")]
    [SerializeField] float xSpeed = 10f;
    [SerializeField] float ySpeed = 5f;
    [SerializeField] float paddingX = 0.5f;
    [SerializeField] float paddingBottom = 1f;
    [SerializeField] float paddingTopProp = 0.7f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    float xMinCam;
    float xMaxCam;
    float yMinCam;
    float yMaxCam;

    [Header("Health")]
    [SerializeField] float health = 500f;

    [Header("Laser")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float xVelLaser = 0f;
    [SerializeField] float yVelLaser = 10f;
    [SerializeField] float laserFiringPeriod = 0.3f;
    Coroutine fireCoroutine;
    [SerializeField] AudioClip shootSFX;
    [SerializeField] [Range(0, 1)] float shootSFXVolume = 0.75f;

    [Header("Death Explosion")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationExplosion = 1f;
    Quaternion explosionRotation;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] [Range(0, 1)] float deathSFXVolume = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        Debug.Log(new Vector2(xMin, xMax));
    }


    // Update is called once per frame
    void Update()
    {
        //Move();      // WASD
        MouseMove();   // Mouse
        Fire();
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
            PlayerDies();
        }
    }

    private void PlayerDies()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        explosionRotation.SetFromToRotation(new Vector3(0, 1, 0), new Vector3(0, 0, 1));
        GameObject explosion = Instantiate(deathVFX, transform.position, explosionRotation);
        Destroy(explosion, durationExplosion);
        AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, deathSFXVolume);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire2"))     // Fire1 for keyboard, Fire2 for mouse
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire2"))       // Fire1 for keyboard, Fire2 for mouse
        {
            StopCoroutine(fireCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(xVelLaser, yVelLaser);
            AudioSource.PlayClipAtPoint(shootSFX, Camera.main.transform.position, shootSFXVolume);
            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }

    private void Move()  // Funció original de moviment per teclat
    {
        var deltaX = Input.GetAxis("Horizontal")* Time.deltaTime;
        var deltaY = Input.GetAxis("Vertical")* Time.deltaTime;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX  * xSpeed, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY  * ySpeed, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void MouseMove()
    {
        double mousePosInUnitsX = (xMaxCam - xMinCam) * ((Input.mousePosition.x / Screen.width) - 0.5);
        double mousePosInUnitsY = (yMaxCam - yMinCam) * ((Input.mousePosition.y / Screen.height) - 0.5);
        float clampedMousePIUX = Mathf.Clamp((float)mousePosInUnitsX, xMin, xMax);
        float clampedMousePIUY = Mathf.Clamp((float)mousePosInUnitsY, yMin, yMax);
        var targetPosition = new Vector2(clampedMousePIUX, clampedMousePIUY);

        // For maximum speed movement
        var movementThisFrame = Mathf.Max(xSpeed, ySpeed) * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

        // For instantaneous movement
        //  transform.position = targetPosition; 
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMinCam = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        xMaxCam = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        yMinCam = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        yMaxCam = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        xMin = xMinCam + paddingX;
        xMax = xMaxCam - paddingX;
        yMin = yMinCam + paddingBottom;
        yMax = yMinCam + (yMaxCam - yMinCam) * paddingTopProp;
    }


    public Vector3 GetPlayerPosition()
    {
        return transform.position;
    }

    public float GetPlayerHealth()
    {
        return health;
    }


}
