using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObjectsMaker : MonoBehaviour
{
    private GameObject[] ghostObject = new GameObject[8];

    static public float screenWidth;
    static public float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.y - screenBottomLeft.y;

        // create ghostShips
        for (int i = 0; i < ghostObject.Length; i++)
        {
            ghostObject[i] = Instantiate(gameObject, transform.position, transform.rotation);

            DestroyImmediate(ghostObject[i].GetComponent<GhostObjectsMaker>());
            DestroyImmediate(ghostObject[i].GetComponent<PlayerController>());
        }

        GhostObjectsPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // ghost ships control
        GhostObjectsPosition();

        foreach (GameObject ghost in ghostObject)
        {
            Transform ghostT = ghost.transform;
            ghostT.rotation = transform.rotation;

            if (ghostT.position.x < screenWidth / 2 && ghostT.position.x > -screenWidth / 2 &&
                ghostT.position.y < screenHeight / 2 && ghostT.position.y > -screenHeight / 2)
            {
                transform.position = ghostT.position;

                break;
            }
        }
    }

    private void GhostObjectsPosition()
    {
        Vector3 ghostPostion = transform.position;
        
        // right
        ghostPostion.x = transform.position.x + screenWidth;
        ghostObject[0].transform.position = ghostPostion;

        // right bot 
        ghostPostion.x = transform.position.x + screenWidth;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostObject[1].transform.position = ghostPostion;

        // bot
        ghostPostion.x = transform.position.x;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostObject[2].transform.position = ghostPostion;

        // left bot
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostObject[3].transform.position = ghostPostion;

        // left
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y;
        ghostObject[4].transform.position = ghostPostion;

        // left top
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostObject[5].transform.position = ghostPostion;

        // top
        ghostPostion.x = transform.position.x;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostObject[6].transform.position = ghostPostion;

        // right top 
        ghostPostion.x = transform.position.x + screenWidth;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostObject[7].transform.position = ghostPostion;
    }
}
