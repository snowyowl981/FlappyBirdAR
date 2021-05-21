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

    // 플레이어 컴포넌트
    public Rigidbody birdRig;
    private Transform birdTr;
    private Touch touch;
    public Animator anim;
    public GameObject sky;

    // 필요 컴포넌트
    public GameManager gameManager;

    // 필요 변수
    public bool isDie = false;

    // Start is called before the first frame update
    void Start()
    {
        // 오브젝트 변수 할당
        birdRig = GetComponent<Rigidbody>();
        birdTr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        sky = GameObject.Find("Sky");

        //Physics.gravity = new Vector3(0, fallSpeed, 0);
        birdRig.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        // 중력 설정
        Physics.gravity = new Vector3(0, fallSpeed, 0);

        if(GameManager.isStart && isDie == false)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Jump();
            }
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

        if (birdTr.transform.position.y > sky.transform.position.y)
        {
            birdTr.transform.position = sky.transform.position;
        }
    }

    private void Jump()
    {
        birdRig.useGravity = true;
        birdRig.velocity = Vector3.up * jumpForce;
    }

    public void OnCollisionEnter(Collision coll)
    {
        if(coll.gameObject.tag == "PILLAR" || coll.gameObject.tag == "GROUND")
        {
            //Debug.Log($"Dead by {coll.gameObject.name}");
            gameManager.GameOver();
        }
    }

    IEnumerator OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "SCOREZONE")
        {
            gameManager.score++;
            gameManager.UpdateScore();
            yield return new WaitForSeconds(2f);
        }
    }
}
