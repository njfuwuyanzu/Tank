using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //用来装饰初始化地图所需物体的数组
    //0.老家 1.墙 2.障碍 3.出生效果 4.河流 5.草 6.空气墙
    public GameObject[] item;
    //已有物体的位置的列表
    public List<Vector3> itemPositionList = new List<Vector3>();
    private void Awake()
    {
        IniteMap();
    }

    //初始化地图
    private void IniteMap()
    {
        //实例化老家
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //用墙围住老家
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //实例化外围空气墙
        for (int i = -9; i <= 9; i++)
        {
            CreateItem(item[6], new Vector3(-11, i, 0), Quaternion.identity);
        }
        for (int i = -9; i <= 9; i++)
        {
            CreateItem(item[6], new Vector3(11, i, 0), Quaternion.identity);
        }
        for (int i = -11; i <= 11; i++)
        {
            CreateItem(item[6], new Vector3(i, 9, 0), Quaternion.identity);
        }
        for (int i = -11; i <= 11; i++)
        {
            CreateItem(item[6], new Vector3(i, -9, 0), Quaternion.identity);
        }
        //初始化玩家
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

        //初始化敌人
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 4, 5);//延时调用;参数：方法名，第一次延时多久调用，之后每次延时多久调用

        //实例化地图
        for (int i = 0; i < 60; i++)
        {
            CreateItem(item[1], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[2], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[4], CreateRandomPosition(), Quaternion.identity);
        }
        for (int i = 0; i < 20; i++)
        {
            CreateItem(item[5], CreateRandomPosition(), Quaternion.identity);
        }
    }


    //封装初始化方法
    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    //产生随机位置
    private Vector3 CreateRandomPosition()
    {
        while (true)
        {
            Vector3 createPosition = new Vector3(Random.Range(-9, 10), Random.Range(-7, 8), 0);
            if (!HasThePosition(createPosition))
            {
                return createPosition;
            }
        }
    }

    //随机产生敌人
    private void CreateEnemy()
    {
        int num = Random.Range(0, 3);
        Vector3 Enemyposition = new Vector3();
        switch (num)
        {
            case 0:Enemyposition = new Vector3(-10, 8, 0);break;
            case 1: Enemyposition = new Vector3(0, 8, 0); break;
            case 2: Enemyposition = new Vector3(10, 8, 0); break;
        }
        CreateItem(item[3], Enemyposition, Quaternion.identity);
    }

    //判断该位置是否已经被占用
    private bool HasThePosition(Vector3 position)
    {
        for(int i = 0; i < itemPositionList.Count; i++)
        {
            if (position == itemPositionList[i])
            {
                return true;
            }
        }
        return false;
    }
}
