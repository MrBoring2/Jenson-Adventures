using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudContoller : MonoBehaviour
{
    private SpriteRenderer sprite;
    bool startFade;
    public float FadeSpeed = 1f;

    public bool StartFade { get => startFade; set => startFade = value; }

    void Start()
    {
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    void Update()
    {
        var color = sprite.color;
        if (StartFade)
        {
            color.a -= FadeSpeed * Time.deltaTime;
        }

        else
        {
            color.a += FadeSpeed * Time.deltaTime;
        }
        color.a = Mathf.Clamp(color.a, 0, 1);
        sprite.color = color;

        if (sprite.color.a <= 0)
        {
            sprite.gameObject.SetActive(false);
            StartCoroutine(WaitUnFade());
           
        }
        
        
    }
    IEnumerator WaitUnFade()
    {
        yield return new WaitForSeconds(1);
        sprite.gameObject.SetActive(true);
        startFade = false;
    }

}
