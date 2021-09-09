using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    //属性值
    public int lifeValue = 3;
    public int playerSource = 0;
    public bool isDead;
    public bool isDefeat;

    //单例
    private static PlayerManager instance;

    //引用
    public GameObject born;
    public Text PlayerScoreText;
    public Text PlayerLifeValueText;
    public GameObject isDefeatUI;

    public static PlayerManager Instance { get => instance; set => instance = value; }


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDefeat)
        {
            isDefeatUI.SetActive(true);
            Invoke("ReturnMain", 3);
        }
        if (isDead)
        {
            Recover();
        }
        PlayerScoreText.text = playerSource.ToString();
        PlayerLifeValueText.text = lifeValue.ToString();
    }
    private void Recover()
    {
        if (lifeValue <= 0)
        {
            isDefeat = true;
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().createPlayer = true;
            isDead = false;
        }
    }

    private void ReturnMain()
    {
        SceneManager.LoadScene(0);
    }
}
