using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public static Player instance;
    public CapsuleCollider playerCol;
    //플레이어 콜라이더
    public GameObject player;
	//
	public bool testMode;

	public float slideTime;
	//슬라이드 후 이동 가능 시간
	public  int score = 0;
	bool isHammer = false;
	bool isElec = false;

	Vector3 currentColTransform;
    //콜라이더 좌표
    public float slideColRotateTime;
    //콜라이더 회전 지속 시간
    public GameObject hammer;

    // 플레이어 게임 오브젝트
    public static  bool canMove = true;
    //이동 종료 여부
    public float slideColMov;
    //콜라이더 이동
    public float playerSpeed;
    // 플레이어 이동 속도 애니메이션과 관계 없음
    public float sideSpeed;
    //옆으로 움직이는 속도
    public float jumpSpeed;
    //점프 속도
    public float downSpeed;
    // 떨어지는 속도
    public float jumpHeight;
    //점프 높이

    float destinationPos;
    // Vector3 destinationPos = new Vector3();
    // Vector3 dir = new Vector3();
    float dir;

    RaycastHit Hit;
    // 벽과의 충돌을 감지할 Ray

    public Animator anim;



    // 애니메이션 속도 


    [SerializeField] float sensitivity;
    // 드래그 민감도
    

    Vector3 firstTouch;
    // 첫번째 터치 
    Vector3 endTouch;
    //두번째 터치
    Vector3 firstMouseTouch;
    // 마우스 첫번째 
    Vector3 endMouseTouch;
    //마우스 두번째 
    void Start()
    {
        currentColTransform = playerCol.center;
		instance = this;

	}
 
    void Update()
    {
        
        if (anim.GetBool("Dead") == false)
        {

            player.transform.position += new Vector3(0, 0, playerSpeed) * Time.deltaTime;
        }


        //모바일 애니메이션 로직 

        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {

                firstTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                Debug.Log("first" + firstTouch + touch.position);

            }

            if (touch.phase == TouchPhase.Ended)

            {
                Debug.Log("ended" + touch.position);
                endTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));

                float xMoved = endTouch.x - firstTouch.x;
                float yMoved = endTouch.y - firstTouch.y;
                if (Mathf.Abs(xMoved) > Mathf.Abs(yMoved))
                {
                    if (xMoved > 0 && canMove == true)
                    {
                        StartCoroutine(RightMoveCo(player.transform, 1)); //우측이동
                    }
                    else
                    {
                        StartCoroutine(LeftMoveCo(player.transform, -1)); //좌측이동
                    }
                }
                else
                {
                    if (yMoved > 0 && canMove == true)
                    {
                        StartCoroutine(JumpCo(player.transform, 1)); //점프
                    }
                    else
                    {
                        anim.SetTrigger("Slide"); //슬라이드

                    }
                }
            }
        }

        */

        if (PauseManager.paused == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
				if (isElec == true &&canMove ==true)
			{
					anim.SetBool("PoleJump", true);
					canMove = false;
					
				}
				if(isHammer==true && canMove ==true)
				{
					anim.SetBool("Hammer_1", true);
					
					hammer.SetActive(true);
				}
                firstMouseTouch = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
                //   Debug.Log("first" + firstTouch + touch.position);

            }
			if(Input.GetMouseButton(0))
			{
				if (isElec == true && canMove == true)
				{
					anim.SetBool("PoleJump", true);
					canMove = false;

				}
			}
            if (Input.GetMouseButtonUp(0))

            {
	                 if (anim.GetBool("PoleJump")==true)
				{
					anim.SetBool("PoleJump", false);
					canMove = true;
				}
				
				
					//  Debug.Log("ended" + touch.position);
					endMouseTouch = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));

                float xMoved = endMouseTouch.x - firstMouseTouch.x;
                float yMoved = endMouseTouch.y - firstMouseTouch.y;
                if (Mathf.Abs(xMoved - yMoved) >= sensitivity)
                {
                    if (Mathf.Abs(xMoved) > Mathf.Abs(yMoved))
                    {
                        if (xMoved > 0 && canMove == true)
                        {
                            StartCoroutine(RightMoveCo(player.transform, 1)); //우측이동
                        }
                        else if (xMoved < 0 && canMove == true)
                        {
                            StartCoroutine(LeftMoveCo(player.transform, -1)); //좌측이동
                        }
                    }
                    else
                    {
                        if (yMoved > 0 && canMove == true)
                        {
                            StartCoroutine(JumpCo(player.transform, 1)); //점프
                        }
                        else if (yMoved < 0 && canMove == true)
                        {

                            anim.SetBool("Slide", true); //슬라이드
                            playerCol.center = new Vector3(currentColTransform.x, currentColTransform.y - slideColMov, currentColTransform.z);
                            playerCol.direction = 2;
                            canMove = false;
                        }
                    }
                }


            }
        }
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Polejump_1") )
		{

			playerCol.center = new Vector3(currentColTransform.x, currentColTransform.y+3, currentColTransform.z);
			
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Polejump_3"))
		{

			playerCol.center = currentColTransform;

		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= slideTime)
		{

			canMove = true;
			//   Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
			Invoke("OriginColCen", slideColRotateTime);
		}
		if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 2f)
        {
         

         //   Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            Invoke("OriginColCen", slideColRotateTime);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Hammer_1") && anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8f)
        {
			anim.SetBool("Hammer_1", false);
			Debug.Log(anim.GetCurrentAnimatorStateInfo(0).normalizedTime);
            hammer.SetActive(false);
       
        }




        /*
        if(Input.touchCount>=1)
        {
                var touch = Input.GetTouch(0);
                Vector2 touchposition = Camera.main.ScreenToWorldPoint(touch.position);



        }
        */











        if (Input.GetKeyDown(KeyCode.A) && canMove == true) //좌로 이동
        {
            StartCoroutine(LeftMoveCo(player.transform, -1));
        }
        if (Input.GetKeyDown(KeyCode.D) && canMove == true) //우로 이동 
        {




            StartCoroutine(RightMoveCo(player.transform, 1));

        }

        if (Input.GetKeyDown(KeyCode.W) && canMove == true) //점프 
        {



            StartCoroutine(JumpCo(player.transform, 1));

        }
        if (Input.GetKeyDown(KeyCode.F)) //사망
        {


            anim.SetBool("Dead", true);

            canMove = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && canMove == true) //슬라이드
        {



            anim.SetBool("Slide", true);


        }
        if (Input.GetKey(KeyCode.H) && canMove == true) //망치 변경   재현이형과 상의 후 조건 추가
        {



            anim.SetBool("Hammer_1", true);
            hammer.SetActive(true);

        }
        if (Input.GetKeyUp(KeyCode.H) ) //망치 변경
        {




            anim.SetBool("Hammer_1", false);

        }

        if (Input.GetKey(KeyCode.Q) && canMove == true) //왼벽타기  재현이형과 상의 후 조건 추가
        { 



            anim.SetBool("WallWalk_Left", true);
            canMove = false;

        }
        if (Input.GetKeyUp(KeyCode.Q)) //왼벽타기
        {



            anim.SetBool("WallWalk_Left", false);
            canMove = true;

        }
        if (Input.GetKey(KeyCode.E) && canMove == true) // 오른벽타기  재현이형과 상의 후 조건 추가
        {



            anim.SetBool("WallWalk_Right", true);
            canMove = false;

        }
        if (Input.GetKeyUp(KeyCode.E)) //오른벽타기
        {



            anim.SetBool("WallWalk_Right", false);
            canMove = true;

        }
        if (Input.GetKey(KeyCode.T) && canMove == true) // 전신주 타기  재현이형과 상의 후 조건 추가
        {



            anim.SetBool("PoleJump", true);
            canMove = false;
           


        }
        if (Input.GetKeyUp(KeyCode.T)) // 전신주 타기  재현이형과 상의 후 조건 추가
        {



            anim.SetBool("PoleJump", false);
            canMove = true;


        }
       
    }

    void OriginColCen()
    {
        playerCol.center = currentColTransform;
        playerCol.direction = 1;
    }

    IEnumerator JumpCo(Transform transform_param, int a)
    {
        dir = a;
        destinationPos = transform_param.position.y +jumpHeight+ dir;
        anim.SetBool("Jump", true);
        Vector3 currentPos = transform_param.position;

        canMove = false;

        while (transform_param.position.y - destinationPos >= 0.0001f || destinationPos - transform_param.position.y >= 0.0001f)
        {

            transform_param.position = Vector3.MoveTowards(transform_param.position, new Vector3(transform.position.x, destinationPos, transform.position.z), jumpSpeed * Time.deltaTime);
            yield return null;
        }
        transform_param.position = new Vector3(transform.position.x, destinationPos, transform.position.z);

        while (transform_param.position.y - currentPos.y >= 0.0001f || currentPos.y - transform_param.position.y >= 0.0001f)
        {

            transform_param.position = Vector3.MoveTowards(transform_param.position, new Vector3(transform.position.x, currentPos.y, transform.position.z), downSpeed * Time.deltaTime);
            yield return null;

        }
        transform_param.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
        anim.SetBool("Jump", false);
        canMove = true;
    }


    IEnumerator LeftMoveCo(Transform transform_param, int a)
    {
        dir = a;
        destinationPos = transform_param.position.x + dir;
        Debug.DrawRay(transform.position, new Vector3(a, 0, 0), Color.white, 1f);
        // Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.blue, 0.5f);
        if (Physics.Raycast(transform.position, new Vector3(a, 0, 0), out Hit, 1))
        {
            if (Hit.collider.CompareTag("Wall"))
            {
                
            }

        }
        else
        {
            

            anim.SetBool("Left", true);
            canMove = false;
            while (transform_param.position.x - destinationPos >= 0.0001f)
            {

                transform_param.position = Vector3.MoveTowards(transform_param.position, new Vector3(destinationPos, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);
                yield return null;
            }
            transform_param.position = new Vector3(destinationPos, transform.position.y, transform.position.z);
            anim.SetBool("Left", false);
            canMove = true;
        }
    }
    IEnumerator RightMoveCo(Transform transform_param, int a)
    {
        dir = a;
        destinationPos = transform_param.position.x + dir;

        Debug.DrawRay(transform.position, new Vector3(a, 0, 0), Color.white, 1f);
        // Debug.DrawRay(transform.position, transform.forward * 1.5f, Color.blue, 0.5f);
        if (Physics.Raycast(transform.position, new Vector3(a, 0, 0), out Hit, 1))
        {
            if (Hit.collider.CompareTag("Wall"))
            {
                Debug.Log("바로앞에 벽이있음");
            }

        }
        else
        {
            anim.SetBool("Right", true);
            canMove = false;
            while (destinationPos - transform_param.position.x >= 0.0001f)
            {

                transform_param.position = Vector3.MoveTowards(transform_param.position, new Vector3(destinationPos, transform.position.y, transform.position.z), sideSpeed * Time.deltaTime);
                yield return null;
            }
            transform_param.position = new Vector3(destinationPos, transform.position.y, transform.position.z);
            anim.SetBool("Right", false);
            canMove = true;

        }
    }
	private void OnTriggerStay(Collider _col)
	{
		if(_col.tag == "BreakWall"&&anim.GetBool("Hammer_1")==true)
		{
			Debug.Log("sd");
			_col.gameObject.transform.parent.GetComponent<Animator>().SetBool("WallBreak", true);
			_col.gameObject.transform.parent.GetComponent<BoxCollider>().enabled = false;
		}
	}
	void OnTriggerEnter(Collider _col)
	{
		if (_col.tag == "obstacle" && testMode==false)
		{
			anim.SetBool("Dead", true);
			canMove = false;
		}

		if (_col.tag == "hammer")
		{
			isHammer = true;
			Debug.Log(isHammer);
		}

		if (_col.tag == "electro")
		{
			isElec = true;
			Debug.Log(isElec);
		}

		if (_col.tag == "score")
		{
			score += _col.gameObject.GetComponent<Col>().score;
			Debug.Log(score);
		}
	}

	void OnTriggerExit(Collider _col)
	{
		if (_col.tag == "hammer")
		{
			isHammer = false;
			Debug.Log(isHammer);
		}

		if (_col.tag == "electro")
		{
			isElec = false;
			Debug.Log(isElec);
		}
	}
}

