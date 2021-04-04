using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    CloudContoller cloudContoller;
    void Start()
    {
        cloudContoller = GetComponentInParent<CloudContoller>();
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            cloudContoller.StartFade = true;
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            cloudContoller.StartFade = false;
        }
    }

}
