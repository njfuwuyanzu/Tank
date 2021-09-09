using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour
{
    //����װ�γ�ʼ����ͼ�������������
    //0.�ϼ� 1.ǽ 2.�ϰ� 3.����Ч�� 4.���� 5.�� 6.����ǽ
    public GameObject[] item;
    //���������λ�õ��б�
    public List<Vector3> itemPositionList = new List<Vector3>();
    private void Awake()
    {
        IniteMap();
    }

    //��ʼ����ͼ
    private void IniteMap()
    {
        //ʵ�����ϼ�
        CreateItem(item[0], new Vector3(0, -8, 0), Quaternion.identity);
        //��ǽΧס�ϼ�
        CreateItem(item[1], new Vector3(1, -8, 0), Quaternion.identity);
        CreateItem(item[1], new Vector3(-1, -8, 0), Quaternion.identity);
        for (int i = -1; i < 2; i++)
        {
            CreateItem(item[1], new Vector3(i, -7, 0), Quaternion.identity);
        }
        //ʵ������Χ����ǽ
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
        //��ʼ�����
        GameObject go = Instantiate(item[3], new Vector3(-2, -8, 0), Quaternion.identity);
        go.GetComponent<Born>().createPlayer = true;

        //��ʼ������
        CreateItem(item[3], new Vector3(-10, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(0, 8, 0), Quaternion.identity);
        CreateItem(item[3], new Vector3(10, 8, 0), Quaternion.identity);
        InvokeRepeating("CreateEnemy", 4, 5);//��ʱ����;����������������һ����ʱ��õ��ã�֮��ÿ����ʱ��õ���

        //ʵ������ͼ
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


    //��װ��ʼ������
    private void CreateItem(GameObject createGameObject,Vector3 createPosition,Quaternion createRotation)
    {
        GameObject itemGo = Instantiate(createGameObject, createPosition, createRotation);
        itemGo.transform.SetParent(gameObject.transform);
        itemPositionList.Add(createPosition);
    }
    //�������λ��
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

    //�����������
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

    //�жϸ�λ���Ƿ��Ѿ���ռ��
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
