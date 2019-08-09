using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public BaseBrain brain;
    public float turnSpeed = 50f;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float move = Input.GetAxis("Vertical");
        move *= ((Input.GetKey(KeyCode.LeftShift)) ? 2f : 1.5f);
        brain.animator.SetFloat("MoveSpeed", move);
        transform.Translate(Vector3.forward * move * Time.deltaTime);

        float rotate = Input.GetAxis("Horizontal") * 80f * Time.deltaTime;
        transform.Rotate(Vector3.up * rotate);
    }
}
