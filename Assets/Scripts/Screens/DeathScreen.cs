using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public Button retryButton;
    public Button checkPointButton;
    public Text text;
    public bool isCheckPoint;
    private float alpha;
    private CanvasGroup canvasGroup;

    public float Alpha { get => alpha; set => alpha = value; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Alpha = 0;
        text.gameObject.SetActive(true); 
        retryButton.gameObject.SetActive(true);
        checkPointButton.gameObject.SetActive(false);
    }
    public IEnumerator Fade(bool toVisible)
    {
        Alpha = 0;
        float step = toVisible ? 0.01f : -0.01F;
        int endValue = toVisible ? 1 : 0;
        
        while (Alpha!=endValue)
        {
            Alpha += step;
            canvasGroup.alpha = Alpha;
            if(Alpha<0)
            {
                Alpha = 0;
            }
            else if (Alpha>1)
            {
                Alpha = 1;
            }
            yield return new WaitForSeconds(0.005f);
        }
        
    }

    
}
