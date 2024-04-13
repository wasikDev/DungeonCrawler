using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerMovement : MonoBehaviour
{
    float horizontalMove = 0f;
    float verticalMove = 0f;
    public float runSpeed = 1f;
    Rigidbody2D rb;

    public VisualEffect vfxRenderer;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed; ;

        transform.position += Vector3.right * horizontalMove * Time.deltaTime;
        transform.position += Vector3.up * verticalMove * Time.deltaTime;

        vfxRenderer.SetVector3("ColliderPos", gameObject.transform.localPosition);
    }
}
