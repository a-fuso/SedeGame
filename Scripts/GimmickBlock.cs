using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmickBlock : MonoBehaviour
{
    public float lenght = 0.0f;
    public bool isDelete = false;
    public GameObject deadObj;

    bool isFell = false;
    float fadeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
     Rigidbody2D rbody = GetComponent<Rigidbody2D>();
        rbody.bodyType = RigidbodyType2D.Static;
        deadObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = 
            GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float d =Vector2.Distance(transform.position, player.transform.position);

            if(lenght >= d)
            {
                Rigidbody2D rbody = GetComponent<Rigidbody2D>();
                if(rbody.bodyType == RigidbodyType2D.Static)
                {
                    rbody.bodyType = RigidbodyType2D.Dynamic;
                    deadObj.SetActive(true);
                }
            }
        }

        if (isFell)
        {
            fadeTime -= Time.deltaTime;
            Color col = GetComponent<SpriteRenderer>().color;
            col.a = fadeTime;
            GetComponent<SpriteRenderer>().color = col;
            if (fadeTime <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDelete)
        {
            isFell = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position,lenght);
    }
}
