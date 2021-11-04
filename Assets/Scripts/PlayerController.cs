using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    public Rigidbody playerRigid;
    private const float PlayerSpeed = 1.5f;
    
    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void MovePlayer()
    {
        float move = default;
        if (Input.GetKey("left")) move = -1.0f;
        else if (Input.GetKey("right")) move = 1.0f;
        playerRigid.MovePosition(playerRigid.position + (transform.forward + new Vector3(move,0,0)) * PlayerSpeed * Time.fixedDeltaTime);
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Nail")) return;
        GameManager.Instance.CollidedNail(other.gameObject);
    }
}
