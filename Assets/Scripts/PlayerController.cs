using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private float laserForce = 10f;
    private GameObject[] laserPool = new GameObject[8];

    // Start is called before the first frame update
    void Start()
    {
        // create laser pool
        for (int i = 0; i < laserPool.Length; i++)
        {
            laserPool[i] = Instantiate(laserPrefab, transform.position, Quaternion.identity);
            laserPool[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FireLaser();
        }
    }

    void FixedUpdate()
    {
        // movement
        if (Input.GetAxis("Vertical") > 0) // only forward
        {
            transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed);
        }
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed);

        // rotation
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RotateToPoint(mousePosition, transform);
    }

    void OnCollisionEnter2D()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RotateToPoint(Vector3 point, Transform transform)
    {
        //rotate to point direction
        Vector3 vectorToTarget = point - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - 90;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 10f);
    }

    private void FireLaser()
    {
        foreach (GameObject laser in laserPool)
        {
            if (!laser.activeSelf)
            {
                laser.SetActive(true);
                laser.transform.rotation = transform.rotation;
                laser.transform.position = transform.position;
                laser.GetComponent<Rigidbody2D>().AddForce(transform.up * laserForce, ForceMode2D.Impulse);
                break;
            }
        }
    }
}
