using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //属性值
    public float MoveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float h, v=-1;
    //private float defendTimeVal = 3;
    //private bool isDefended = true;

    //计时器
    private float TimeVal;
    private float timeValChangeDirection;//转向时间计时器


    

    //引用
    SpriteRenderer sr;
    public Sprite[] tankSprite;//上右下左
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
        //是否处于无敌状态
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


        //攻击时间间隔
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


    //坦克死亡方法
    private void Die()
    {
        /*if (isDefended)
        {
            return;
        }*/

        PlayerManager.Instance.playerSource++;
        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }


    //坦克攻击
    private void Attack()
    {
       
        Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
        TimeVal = 0;
        
    }

    //坦克移动
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
