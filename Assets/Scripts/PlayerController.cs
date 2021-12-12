using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;

    private GameObject[] ghostShips = new GameObject[8];

    private float screenWidth;
    private float screenHeight;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z));
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z));
        
        screenWidth = screenTopRight.x - screenBottomLeft.x;
        screenHeight = screenTopRight.y - screenBottomLeft.y;

        // create ghostShips
        for (int i = 0; i < ghostShips.Length; i++)
        {
            ghostShips[i] = Instantiate(gameObject, transform.position, transform.rotation);

            DestroyImmediate(ghostShips[i].GetComponent<PlayerController>());
        }

        GhostShipsPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        // movement
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);

        // rotation
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateToPoint(mousePosition, transform);

        // ghost ships control
        GhostShipsPosition();

        foreach (GameObject ghost in ghostShips)
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

    private void RotateToPoint(Vector3 point, Transform transform)
    {
        //rotate to point direction
        Vector3 vectorToTarget = point - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
    }

    private void GhostShipsPosition()
    {
        Vector3 ghostPostion = transform.position;
        
        // right
        ghostPostion.x = transform.position.x + screenWidth;
        ghostShips[0].transform.position = ghostPostion;

        // right bot 
        ghostPostion.x = transform.position.x + screenWidth;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostShips[1].transform.position = ghostPostion;

        // bot
        ghostPostion.x = transform.position.x;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostShips[2].transform.position = ghostPostion;

        // left bot
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y - screenHeight;
        ghostShips[3].transform.position = ghostPostion;

        // left
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y;
        ghostShips[4].transform.position = ghostPostion;

        // left top
        ghostPostion.x = transform.position.x - screenWidth;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostShips[5].transform.position = ghostPostion;

        // top
        ghostPostion.x = transform.position.x;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostShips[6].transform.position = ghostPostion;

        // right top 
        ghostPostion.x = transform.position.x + screenWidth;
        ghostPostion.y = transform.position.y + screenHeight;
        ghostShips[7].transform.position = ghostPostion;
    }
}
