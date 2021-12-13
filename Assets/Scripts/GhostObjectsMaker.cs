using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObjectsMaker : MonoBehaviour
{
    private GameObject[] ghostObjects = new GameObject[8];

    static public float screenWidth;
    static public float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.y - screenBottomLeft.y;

        // create ghostObjects
        for (int i = 0; i < ghostObjects.Length; i++)
        {
            ghostObjects[i] = Instantiate(gameObject, transform.position, transform.rotation);

            DestroyImmediate(ghostObjects[i].GetComponent<GhostObjectsMaker>());
            DestroyImmediate(ghostObjects[i].GetComponent<PlayerController>());
            DestroyImmediate(ghostObjects[i].GetComponent<AsteroidController>());
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

        foreach (GameObject ghost in ghostObjects)
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
        ghostObjects[0].transform.position = ghostPostion;

        // right bot 
        ghostPostion.x = transform.position.x + screenWidth;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostObjects[1].transform.position = ghostPostion;

        // bot
        ghostPostion.x = transform.position.x;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostObjects[2].transform.position = ghostPostion;

        // left bot
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostObjects[3].transform.position = ghostPostion;

        // left
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y;
        ghostObjects[4].transform.position = ghostPostion;

        // left top
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostObjects[5].transform.position = ghostPostion;

        // top
        ghostPostion.x = transform.position.x;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostObjects[6].transform.position = ghostPostion;

        // right top 
        ghostPostion.x = transform.position.x + screenWidth;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostObjects[7].transform.position = ghostPostion;
    }

    void OnDestroy()
    {
        foreach (GameObject ghost in ghostObjects)
        {
            Destroy(ghost);
        }
    }
}
