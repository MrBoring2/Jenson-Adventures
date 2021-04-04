using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    private Player player;
    public float OffsetX;
    public float OffsetY;
    public Vector2 maxXandY;
    public Vector2 minXandY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        MoveCamera();
    }
    private void MoveCamera()
    {
        float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float targetX = Mathf.Clamp(playerX, minXandY.x, maxXandY.x);
        float targetY = Mathf.Clamp(playerY, minXandY.y, maxXandY.y);
        Vector3 curCameraPos = playerCamera.transform.position;
        playerCamera.transform.position = Vector3.MoveTowards(curCameraPos, new Vector3(targetX + OffsetX, targetY+OffsetY, curCameraPos.z), player.MaxSpeed);
    }
}
