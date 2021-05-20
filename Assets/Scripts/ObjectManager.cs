using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // 기둥 프리팹
    public GameObject downPillar;
    public GameObject upPillar;
    public GameObject scoreZone;

    // 기둥 저장 배열
    GameObject[] downPillars = new GameObject[3];
    GameObject[] upPillars = new GameObject[3];
    GameObject[] scoreZones = new GameObject[3];

    // 필요 변수
    [SerializeField]
    private float pillarSpeed;                                  // 기둥 속도
    private int j =0;                                           // 배열 번호 저장 변수
    private float nextTime = 0;                                 // 기둥 생성 주기(1.5초)
    public float[] randomHeights = {0.2f, 0.3f, 0.4f, 0.5f};    // 기둥 높이값 저장 배열

    // 필요 컴포넌트
    private GameObject imgTarget;
    private GameObject startPos;

    void Awake()
    {
        imgTarget = GameObject.Find("ImageTarget");
        startPos = GameObject.Find("StartPos");
    }

    // Update is called once per frame
    void Update()
    {
        SpawnPillars();
        MovePillars();
    }

    void SpawnPillars()
    {
        // 기둥 생성기
        if(Time.time > nextTime)
        {
            int rNum = Random.Range(0, 4);      // 기둥 높이값 사용할 랜덤 변수
            nextTime = Time.time + 1.5f;        // 기둥 생성 주기
            
            // 기둥 생성
            if(imgTarget == null)
            {
                downPillars[j] = (GameObject) Instantiate(downPillar,
                                                    new Vector3(0, 0.15f, 2.85f),
                                                    Quaternion.identity);

                upPillars[j] = (GameObject) Instantiate(upPillar,
                                                        new Vector3(0, 3f, 2.85f),
                                                        upPillar.transform.rotation);

                scoreZones[j] = (GameObject) Instantiate(scoreZone,
                                                        new Vector3(0, 1.95f - (0.25f * (3 - rNum)), 2.85f),
                                                        Quaternion.identity);
            }

            else 
            {
                downPillars[j] = (GameObject) Instantiate(downPillar,
                                                    startPos.transform.position + new Vector3(0, 0.15f, 0),
                                                    startPos.transform.rotation);

                upPillars[j] = (GameObject) Instantiate(upPillar,
                                                    startPos.transform.position + new Vector3(0, 3f, 0),
                                                    startPos.transform.rotation * Quaternion.Euler(0f, 0f, 180f));

                scoreZones[j] = (GameObject) Instantiate(scoreZone,
                                                    startPos.transform.position + new Vector3(0, 1.95f - (0.25f * (3 - rNum)), 0),
                                                    startPos.transform.rotation);
            }
            
            // 기둥 높이 조절
            downPillars[j].transform.localScale = new Vector3(1.5f,
                                                            randomHeights[rNum],
                                                            1.5f);

            upPillars[j].transform.localScale = new Vector3(1.5f,
                                                            randomHeights[3 - rNum],
                                                            1.5f);
            if(++j == 3) j = 0;
        }
    }

    // 기둥 등속도운동 및 특정 좌표 이동시 제거
    void MovePillars()
    {
        for (int i=0; i<3; i++)
        {
            if(downPillars[i])
            {
                downPillars[i].transform.Translate(0, 0, -pillarSpeed * Time.deltaTime);
                if (downPillars[i].transform.position.z < -2.85f)
                {
                    Destroy(downPillars[i]);
                }
            }

            if(upPillars[i])
            {
                upPillars[i].transform.Translate(0, 0, -pillarSpeed * Time.deltaTime);
                if (upPillars[i].transform.position.z < -2.85f)
                {
                    Destroy(upPillars[i]);
                }
            }

            if(scoreZones[i])
            {
                scoreZones[i].transform.Translate(0, 0, -pillarSpeed * Time.deltaTime);
                if (scoreZones[i].transform.position.z < -2.85f)
                {
                    Destroy(scoreZones[i]);
                }
            }
        }
    }
}
