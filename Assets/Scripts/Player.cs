using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //属性值
    public float MoveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float TimeVal;
    private float defendTimeVal=5;
    private bool isDefended=true;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    //引用
    SpriteRenderer sr;
    public Sprite[] tankSprite;//上右下左
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;
    public GameObject defendEffectPrefab; 

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
        if (isDefended)
        {
            defendEffectPrefab.SetActive(true);
            defendTimeVal -= Time.deltaTime;
            if (defendTimeVal <= 0)
            {
                isDefended = false;
                defendEffectPrefab.SetActive(false);
            }
        }


        //攻击CD
        if (TimeVal >= 0.5)
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
        if (PlayerManager.Instance.isDefeat == true)
        {
            return;
        }
        Move();
        //攻击CD
        if (TimeVal >= 0.5)
        {
            Attack();
        }
        else
        {
            TimeVal += Time.fixedDeltaTime;
        }
    }


    //坦克死亡方法
    private void Die()
    {
        if (isDefended)
        {
            return;
        }

        PlayerManager.Instance.isDead = true;

        //产生爆炸特效
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //死亡
        Destroy(gameObject);
    }


    //坦克攻击
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            TimeVal = 0;
        }
    }
    
    //坦克移动
    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");
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

        if (Mathf.Abs(h) > 0.05f)
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }


        if (h != 0)
        {
            return;
        }

        float v = Input.GetAxisRaw("Vertical");

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
        if (Mathf.Abs(v) > 0.05f)
        {
            moveAudio.clip = tankAudio[0];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
        else
        {
            moveAudio.clip = tankAudio[1];
            if (!moveAudio.isPlaying)
            {
                moveAudio.Play();
            }
        }
    }
}
