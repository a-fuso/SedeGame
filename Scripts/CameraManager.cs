using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public float leftLimit = 0.0f;
    public float rightLimit = 0.0f;
    public float topLimit = 0.0f;
    public float bottomLimit = 0.0f;

    public GameObject subScrren;

    public bool isForseScrollX = false;
    public float forceScrollSpeedX = 0.5f;
    public bool isForseScrollY = false;
    public float forceScrollSpeedY= 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject Player =
            GameObject.FindGameObjectWithTag("Player");
        if (Player != null)
        {
            float x = Player.transform.position.x;
            float y = Player.transform.position.y;
            float z = transform.position.z;

            if (isForseScrollX)
            {
                x = transform.position.x + (forceScrollSpeedX* Time.deltaTime);
            }

            if (x < leftLimit )
            {
                x = leftLimit;
            }

            else if (x > rightLimit)
            {
                x =  rightLimit;
            }

            if (isForseScrollY)
            {
            x = transform.position.y + (forceScrollSpeedY * Time.deltaTime);
            }

            if (y < bottomLimit )
            {
                y = bottomLimit;
            }

            else if (y > topLimit)
            {
                y = topLimit;
            }

            Vector3 v3 = new Vector3(x, y, z);
            transform.position = v3;

            if (subScrren != null)
            {
                y = subScrren.transform.position.y;
                z = subScrren.transform.position.z;

                Vector3 v = new Vector3(x / 2.0f, y, z);
                subScrren.transform.position = v;
            }
        }

       
    }
}
