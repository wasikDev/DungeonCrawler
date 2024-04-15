using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerDoorController : MonoBehaviour
{
    public Sprite doorClosed;
    public Sprite doorOpen;

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = doorClosed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // gameObject.transform.position += new Vector3(0, -0.5f, 0);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().sprite = doorOpen;
    }

}
