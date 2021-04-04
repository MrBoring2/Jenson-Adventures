using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundParallax : MonoBehaviour
{
    public float speed = 0;
    float pos = 0;
    private RawImage image;
    bool isMoveToRight;
    int direction;
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<RawImage>();
        
    }
    void Awaike()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player.Move > 0.01 || player.Move < -0.01)
        {
            if (player.IsMoveToRight)
            {
                direction = 1;
            }
            else
            {
                direction = -1;
            }
            pos += speed * direction*Time.deltaTime;

            if (pos > 1.0F)

                pos -= 1.0F;

            image.uvRect = new Rect(pos, 0, 1, 1);
        }
    }
}
