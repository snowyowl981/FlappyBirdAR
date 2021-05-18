using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float fallSpeed;

    private Rigidbody birdRig;
    // Start is called before the first frame update
    void Start()
    {
        birdRig = GetComponent<Rigidbody>();
        // 중력 설정
        //Physics.gravity = new Vector3(0, fallSpeed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = new Vector3(0, fallSpeed, 0);
        if(Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void Jump()
    {
        birdRig.velocity = Vector3.up * jumpForce;
    }

    void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "PIPE")
        {
            Debug.Log("DEAD");
        }
    }
}
