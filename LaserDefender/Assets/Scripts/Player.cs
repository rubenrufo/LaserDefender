using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] float xSpeed = 10f;
    [SerializeField] float ySpeed = 5f;
    [SerializeField] float paddingX = 0.5f;
    [SerializeField] float paddingBottom = 1f;
    [SerializeField] float paddingTopProp = 0.7f;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
        
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

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal")* Time.deltaTime;
        var deltaY = Input.GetAxis("Vertical")* Time.deltaTime;
        var newXPos = Mathf.Clamp(transform.position.x + deltaX  * xSpeed, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY  * xSpeed, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
}
