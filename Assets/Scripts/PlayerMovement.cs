using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Joystick joystick;
    private float horizontalMove;
    private float verticalMove;
    private float speed = 3.0f;

    void Update()
    {
        horizontalMove = joystick.Horizontal * Time.deltaTime * speed;
        if (joystick.Horizontal < 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else if (joystick.Horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        transform.Translate(Vector3.right * horizontalMove);
        //mAnimator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        verticalMove = joystick.Vertical * Time.deltaTime * speed;
        transform.Translate(Vector3.up * verticalMove);
    }
}
