using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //����ֵ
    public float MoveSpeed = 3;
    private Vector3 bulletEulerAngles;
    private float TimeVal;
    private float defendTimeVal=5;
    private bool isDefended=true;
    public AudioSource moveAudio;
    public AudioClip[] tankAudio;

    //����
    SpriteRenderer sr;
    public Sprite[] tankSprite;//��������
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
        //�Ƿ����޵�״̬
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


        //����CD
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
        //����CD
        if (TimeVal >= 0.5)
        {
            Attack();
        }
        else
        {
            TimeVal += Time.fixedDeltaTime;
        }
    }


    //̹����������
    private void Die()
    {
        if (isDefended)
        {
            return;
        }

        PlayerManager.Instance.isDead = true;

        //������ը��Ч
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        //����
        Destroy(gameObject);
    }


    //̹�˹���
    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.Euler(transform.eulerAngles + bulletEulerAngles));
            TimeVal = 0;
        }
    }
    
    //̹���ƶ�
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
