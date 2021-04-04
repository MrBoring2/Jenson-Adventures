using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchroolBackground : MonoBehaviour
{
    private float startPos, length;
    public GameObject cameraPos;
    public float paralaxEffect;

    
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }
  

    // Update is called once per frame
    void Update()
    {
        float temp = cameraPos.transform.position.x * (1 - paralaxEffect);
        float dist = cameraPos.transform.position.x * paralaxEffect;

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}
