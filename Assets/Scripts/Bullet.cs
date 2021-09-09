using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //属性值
    public float MoveSpeed=5;
    public bool isPlayerBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * MoveSpeed*Time.deltaTime,Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Tank":
                if (!isPlayerBullet)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;
            case "Heart":
                collision.SendMessage("Die");
                Destroy(gameObject);
                break;
            case "Grass":
                break;
            case "Wall":
                Destroy(collision.gameObject);//被碰撞的物体
                Destroy(gameObject);//子弹 
                break;
            case "Barriar":
                if (isPlayerBullet)
                {
                    collision.SendMessage("PlayAudio");
                }
                  
                Destroy(gameObject);
                break;
            case "Enemy":
                if (isPlayerBullet)
                {
                    collision.SendMessage("Die");
                    Destroy(gameObject);
                }
                break;

        }
    }
}
