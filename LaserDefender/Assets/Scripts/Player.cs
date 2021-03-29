using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{

    [SerializeField] float xSpeed = 2f;
    [SerializeField] float ySpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal");
        var deltaY = Input.GetAxis("Vertical");
        var newXPos = transform.position.x + deltaX * Time.deltaTime * xSpeed;
        var newYPos = transform.position.y + deltaY * Time.deltaTime * xSpeed;
        transform.position = new Vector2(newXPos, newYPos);
    }
}
