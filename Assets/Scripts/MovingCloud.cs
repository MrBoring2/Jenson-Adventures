using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCloud : MonoBehaviour
{
    float dirX;
    public float speed = 3f;
    public float movingDistance=4f;
    public Transform point;
    public bool isMovingRight = false;



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > point.position.x + movingDistance)
        {
            isMovingRight = false;
        }
        else if (transform.position.x < point.position.x - movingDistance) 
        {
            isMovingRight = true;
        }

        if(isMovingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else transform.position = new Vector2(transform.position.x -speed * Time.deltaTime, transform.position.y);
    }
}
