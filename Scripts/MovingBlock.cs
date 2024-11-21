using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlock : MonoBehaviour
{
    public float moveX = 0.0f;
    public float moveY = 0.0f;
    public float times = 0.0f;
    public float wait = 0.0f;
    public bool isMoveWhenOn = false;
    public bool isCanMove = true;
    Vector3 startPos;
    Vector3 endPos;
    bool isReverse = false;

    float movep = 0;

    // Start is called before the first frame update
    void Start()
    {
       startPos = transform.position;
        endPos = new Vector2(startPos.x + moveX, startPos.y + moveY);
        if (isMoveWhenOn)
        {
            isCanMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCanMove)
        {
            float distance = Vector2.Distance(startPos, endPos);
            float ds = distance / times;
            float df =ds * Time.deltaTime;
            movep += df / distance;
            if (isReverse)
            {
                transform.position = Vector2.Lerp(endPos, startPos, movep);
            }
            else
            {
                transform.position = Vector2.Lerp(startPos, endPos, movep);
            }
            if(movep >= 1.0f)
            {
                movep = 0.0f;
                isReverse = !isReverse;
                isCanMove = false ;
                if(isMoveWhenOn == false)
                {
                    Invoke("Move",wait);
                }
            }
        }
    }

    public void Move()
    {
        isCanMove=true;
    }

    public void Stop()
    {
        isCanMove = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(transform);
            if (isMoveWhenOn)
            {
                isCanMove = true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.SetParent(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 fromPos;
        if(startPos == Vector3.zero)
        {
            fromPos = transform.position;
        }
        else
        {
            fromPos = startPos;
        }

        Gizmos.DrawLine(fromPos,new Vector2(fromPos.x + moveX, fromPos.y + moveY));
        Vector2 size = GetComponent<SpriteRenderer>().size;

        Gizmos.DrawWireCube(fromPos,new Vector2(size.x,size.y));

        Vector2 toPos = new Vector3(fromPos.x + moveX, fromPos.y + moveY);
        Gizmos.DrawWireCube(toPos,new Vector2(size.x,size.y));
    }
}
