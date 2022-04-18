using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject player;
 
    // �÷��̾� ���� ������Ʈ
    public bool canMove = true;
    //�̵� ���� ����
    public float playerSpeed;
    // �÷��̾� �̵� �ӵ� �ִϸ��̼ǰ� ���� ����
    public float sideSpeed;
    //������ �����̴� �ӵ�
    public float jumpSpeed;
    //���� �ӵ�
    public float downSpeed;
    // �������� �ӵ�
    float destinationPos;
    // Vector3 destinationPos = new Vector3();
    // Vector3 dir = new Vector3();
    float dir;
 
    RaycastHit Hit;
    // ������ �浹�� ������ Ray

    public Animator anim;

    // �ִϸ��̼� �ӵ� 


    [SerializeField] float sensitivity;
    // �巡�� �ΰ���

  
    Vector3 firstTouch; 
    // ù��° ��ġ 
    Vector3 endTouch;
   //�ι�° ��ġ
    void Start()
    {
        
    }


    void Update()
    {

        if (anim.GetBool("Dead") == false)
        {

            player.transform.position += new Vector3(0, 0, playerSpeed) * Time.deltaTime;
        }





        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
           if (touch.phase == TouchPhase.Began)
            {
           
                firstTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x,touch.position.y,10));
                Debug.Log("first" + firstTouch+touch.position);

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
                        StartCoroutine(RightMoveCo(player.transform, 1));
                    }
                    else
                    {
                        StartCoroutine(LeftMoveCo(player.transform, -1));
                    }
                }
                else
                {
                    if (yMoved > 0 && canMove == true)
                    {
                        StartCoroutine(JumpCo(player.transform, 1));
                    }
                    else
                    {
                        anim.SetTrigger("Slide");

                    }
                }
            } 
            }







        /*
        if(Input.touchCount>=1)
        {
                var touch = Input.GetTouch(0);
                Vector2 touchposition = Camera.main.ScreenToWorldPoint(touch.position);



        }
        */











        if (Input.GetKeyDown(KeyCode.A)&& canMove == true) //�·� �̵� ���߿� �����̵�� ����
        {                     
                         StartCoroutine(LeftMoveCo(player.transform, -1));       
        }
        if (Input.GetKeyDown(KeyCode.D) && canMove == true) //��� �̵� ���߿� �����̵�� ����
        {
           
            


                StartCoroutine(RightMoveCo(player.transform, 1));
        
        }

        if (Input.GetKeyDown(KeyCode.W) && canMove == true) //���� ���߿� �����̵�� ����
        {
        
      

                StartCoroutine(JumpCo(player.transform, 1));
            
        }
        if (Input.GetKeyDown(KeyCode.F) ) //���
        {


            anim.SetBool("Dead", true);

            canMove = false;
        }
        if (Input.GetKeyDown(KeyCode.S) && canMove == true) //���� ���߿� �����̵�� ����
        {



            anim.SetBool("Slide", true);


        }
        if (Input.GetKey(KeyCode.H) ) //��ġ ����
        {



            anim.SetBool("hammer_1", true);
           

        }
        if (Input.GetKeyUp(KeyCode.H)) //��ġ ����
        {



        
            anim.SetBool("hammer_1", false);

        }
        if (Input.GetKey(KeyCode.J)) //��ġ �ֵθ���
        {



            anim.SetBool("hammer_use", true);
           

        }
        if (Input.GetKeyUp(KeyCode.J)) //��ġ �ֵθ���
        {



          
            anim.SetBool("hammer_use", false);

        }
        if (Input.GetKey(KeyCode.Q)) //�޺�Ÿ��
        {



            anim.SetBool("WallWalk_Left", true);


        }
        if (Input.GetKeyUp(KeyCode.Q)) //�޺�Ÿ��
        {



            anim.SetBool("WallWalk_Left", false);


        }
        if (Input.GetKey(KeyCode.E)) // ������Ÿ��
        {



            anim.SetBool("WallWalk_Right", true);


        }
        if (Input.GetKeyUp(KeyCode.E)) //������Ÿ��
        {



            anim.SetBool("WallWalk_Right", false);


        }

    }



    IEnumerator JumpCo(Transform transform_param, int a)
    {
        dir = a;
        destinationPos = transform_param.position.y + dir;
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
                Debug.Log("�ٷξտ� ��������");
            }

        }
        else
        {
            Debug.Log("�ٷξտ� �� ���� �̵� ����");
           
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
                Debug.Log("�ٷξտ� ��������");
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

}