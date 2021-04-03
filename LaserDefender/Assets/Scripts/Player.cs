using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    // Movement
    [SerializeField] float xSpeed = 10f;
    [SerializeField] float ySpeed = 5f;
    [SerializeField] float paddingX = 0.5f;
    [SerializeField] float paddingBottom = 1f;
    [SerializeField] float paddingTopProp = 0.7f;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Laser
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float xVelLaser = 0f;
    [SerializeField] float yVelLaser = 10f;
    [SerializeField] float laserFiringPeriod = 0.3f;
    Coroutine fireCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }


    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }


    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
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
            yield return new WaitForSeconds(laserFiringPeriod);
        }
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal")* Time.deltaTime;
        var deltaY = Input.GetAxis("Vertical")* Time.deltaTime;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX  * xSpeed, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY  * ySpeed, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        float xMinCam = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float xMaxCam = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float yMinCam = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        float yMaxCam = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        xMin = xMinCam + paddingX;
        xMax = xMaxCam - paddingX;
        yMin = yMinCam + paddingBottom;
        yMax = yMinCam + (yMaxCam - yMinCam) * paddingTopProp;


    }
}
