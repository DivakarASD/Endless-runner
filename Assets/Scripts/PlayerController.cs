using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 velocity;
    private bool isDead = false;

    public float speed = 10f;
    public float strafeSpeed = 10f;
    public float animationDuration = 2.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();        
    }
    
    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        if (Input.GetMouseButton(0))
        {
            if (Input.mousePosition.x > Screen.width / 2)
            {
                inputX = 1;
            }
            else
            {
                inputX = -1;
            }
        }
        velocity = new Vector3(inputX * strafeSpeed, 0, speed);          
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        if (Time.timeSinceLevelLoad < animationDuration)
        {
            rb.MovePosition(rb.position + ((Vector3.forward * speed) * Time.deltaTime));
            return;
        }
        rb.MovePosition(rb.position + velocity * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.CompareTag("Props"))
        {
            Death();
        }

        /*ContactPoint contact = collisionInfo.contacts[0];
        Vector3 pos = contact.point;
        if(pos.z > transform.position.z + GetComponent<CapsuleCollider>().radius)
        {
            Death(); 
        }This could be used if we need to check the game end state only in the transform.position.z*/

    }

    void Death()
    {
        isDead = true;
        Debug.Log("You are Dead");
        GetComponent<Score>().OnDead();
    }

    public void SetSpeed(int speedToIncrease)
    {
        speed = 11.0f + speedToIncrease;
    }
}
