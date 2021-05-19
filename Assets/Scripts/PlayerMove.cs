using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // 점프력
    [SerializeField]
    private float jumpForce;

    // 낙하속도
    [SerializeField]
    private float fallSpeed;

    // 필요 컴포넌트
    private Rigidbody birdRig;
    private Transform birdTr;
    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 변수 할당
        birdRig = GetComponent<Rigidbody>();
        birdTr = GetComponent<Transform>();
        // 중력 설정
        //Physics.gravity = new Vector3(0, fallSpeed, 0);
        birdRig.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = new Vector3(0, fallSpeed, 0);
        if(Input.GetMouseButtonDown(0))
        {
            Jump();
        }
        if (birdRig.velocity.y > 0)
        {
            float jumpAngle = Mathf.Lerp(birdTr.localRotation.x, 30f, birdRig.velocity.y /2);
            birdTr.localRotation = Quaternion.Euler(-jumpAngle, birdTr.localRotation.y, 0);
        }
        else 
        {
            float fallAngle = Mathf.Lerp(birdTr.localRotation.x, 90f, -birdRig.velocity.y /2);
            birdTr.localRotation = Quaternion.Euler(fallAngle, birdTr.localRotation.y, 0);
        }
    }

    private void Jump()
    {
        birdRig.useGravity = true;
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
