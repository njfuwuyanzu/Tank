using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //����ֵ
    public float MoveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float h, v=-1;
    //private float defendTimeVal = 3;
    //private bool isDefended = true;

    //��ʱ��
    private float TimeVal;
    private float timeValChangeDirection;//ת��ʱ���ʱ��


    

    //����
    SpriteRenderer sr;
    public Sprite[] tankSprite;//��������
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    //public GameObject defendEffectPrefab;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //�Ƿ����޵�״̬
        /*if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal == 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }*/


        //����ʱ����
        if (TimeVal >= 3)
        {
            Attack();
        }
        else
        {
            TimeVal += Time.deltaTime;
        }
    }
    private void FixedUpdate()
    {
        Move();

    }


    //̹����������
    private void Die()
    {
        /*if (isDefended)
        {
            return;
        }*/

        PlayerManager.Instance.playerSource++;
        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }


    //̹�˹���
    private void Attack()
    {
       
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        TimeVal = 0;
        
    }

    //̹���ƶ�
    private void Move()
    {
        if (timeValChangeDirection >= 4)
        {
            int num = Random.Range(0, 8);
            if (num >= 5)
            {
                v = -1;
                h = 0;
            }
            else if (num == 0)
            {
                v = 1;
                h = 0;
            }
            else if (num > 0 && num <= 2)
            {
                v = 0;
                h = 1;
            }
            else if (num > 2 && num <= 4)
            {
                v = 0;
                h = -1;
            }
            timeValChangeDirection = 0;
        }
        else
        {
            timeValChangeDirection += Time.fixedDeltaTime;
        }




        transform.Translate(Vector3.right * h * Time.fixedDeltaTime * MoveSpeed, Space.World);
        if (h < 0)
        {
            sr.sprite = tankSprite[3];
            bulletEulerAngles = new Vector3(0, 0, 90);
        }
        else if (h > 0)
        {
            sr.sprite = tankSprite[1];
            bulletEulerAngles = new Vector3(0, 0, -90);
        }

        transform.Translate(Vector3.up * v * MoveSpeed * Time.fixedDeltaTime, Space.World);
        if (v < 0)
        {
            sr.sprite = tankSprite[2];
            bulletEulerAngles = new Vector3(0, 0, 180);
        }
        else if (v > 0)
        {
            sr.sprite = tankSprite[0];
            bulletEulerAngles = new Vector3(0, 0, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy"|| collision.gameObject.tag =="Barriar"|| collision.gameObject.tag =="River")
        {
            timeValChangeDirection = 4;
        }
        if(collision.gameObject.tag == "Wall")
        {
            Attack();
        }
    }
}
