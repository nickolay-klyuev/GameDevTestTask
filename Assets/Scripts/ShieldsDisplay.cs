using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldsDisplay : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        int childCount = transform.childCount;

        if (childCount > player.GetShields() - 1 && childCount > 0)
        {
            int difference = childCount - player.GetShields() + 1;

            for (int i = 0; i < difference; i++)
            {
                Destroy(transform.GetChild(childCount - 1).gameObject);
            }
        }
    }
}
