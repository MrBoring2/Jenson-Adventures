using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTainer : MonoBehaviour
{
    Player player;
    public GameObject skillUp;
    public GameObject game;

    // Start is called before the first frame update
    void Start()
    {    
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) <= 3)
        {
            if (Input.GetKeyDown(KeyCode.E) && game.GetComponent<GameController>().Pause==false)
            {
                skillUp.SetActive(!skillUp.activeSelf);
            }
        }
        else skillUp.SetActive(false);
        
      
    }
}
