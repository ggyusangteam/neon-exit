using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject player;
    public Camera camera;
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

    [SerializeField] Animator anim;

    // �ִϸ��̼� �ӵ� 





    void Start()
    {

    }


    void Update()
    {
    if(anim.GetBool("Dead")==false)
        {
       
        player.transform.position += new Vector3(0, 0, playerSpeed) * Time.deltaTime; 
        }
       


        anim.SetBool("Left", false);
        anim.SetBool("Jump", false);
        anim.SetBool("Right", false);
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
    }


    IEnumerator JumpCo(Transform transform_param, int a)
    {
        dir = a;
        destinationPos = transform_param.position.y + dir;
        anim.SetBool("Jump", true);
        Vector3 currentPos = transform_param.position;
        float cameraPosY = camera.transform.position.y;
        canMove = false;

        while (transform_param.position.y - destinationPos >= 0.0001f || destinationPos - transform_param.position.y >= 0.0001f)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, cameraPosY, camera.transform.position.z);
            transform_param.position = Vector3.MoveTowards(transform_param.position, new Vector3(transform.position.x, destinationPos, transform.position.z), jumpSpeed * Time.deltaTime);
            yield return null;
        }
        transform_param.position = new Vector3(transform.position.x, destinationPos, transform.position.z);
        camera.transform.position = new Vector3(camera.transform.position.x, cameraPosY, camera.transform.position.z);
        while (transform_param.position.y - currentPos.y >= 0.0001f || currentPos.y - transform_param.position.y >= 0.0001f)
        {
            camera.transform.position = new Vector3(camera.transform.position.x, cameraPosY, camera.transform.position.z);
            transform_param.position = Vector3.MoveTowards(transform_param.position, new Vector3(transform.position.x, currentPos.y, transform.position.z), downSpeed * Time.deltaTime);
            yield return null;

        }
        transform_param.position = new Vector3(transform.position.x, currentPos.y, transform.position.z);
        camera.transform.position = new Vector3(camera.transform.position.x, cameraPosY, camera.transform.position.z);
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

            canMove = true;
        }
    }

}