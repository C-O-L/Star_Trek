using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float spawnRate;
    float timer;                                                                    //计时
    public GameObject[] enemyPrefab;                                                //一系列敌人
    public GameObject boss1;                                                        //第一个boss，在玩家获得1000分的时候生成
    public GameObject boss2;                                                        //第二个boss，在玩家获得3000分的时候生成
    public Transform enemyHolder;                                                   //将生成的敌人保存在enemyHolder中
    public GameObject enemy;
    public GameObject gameOverPanel;                                                //游戏结束图层
    public GameObject gameStartPanel;                                               //游戏开始图层
    bool isPlaying = true;                                                          //游戏是否开始
    public static GameManager instance = null;				                        //GameManager的静态实例，允许任何其他脚本访问它。
    // [HideInInspector] public bool isEnemy = true;		                            //布尔值检查是否生成敌人
    
    
    private void Start() 
    {
        gameStartPanel.SetActive(true);                                             //游戏开始的时候显示gameStartPanel
        isPlaying = false;                                                          //游戏未开始
        gameOverPanel.SetActive(false);                                             //游戏开始的时候隐藏gameOverPanel
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlaying)
            return;

        timer += Time.deltaTime;
        if(timer > spawnRate)
        {
            timer = 0;
            SpawnNewEnemy();                                                      //调用SpawnNewEnemy方法
        }
    }

    //生成敌人的方法
    public void SpawnNewEnemy()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-28f, 28f), 15f);             //在区间内生成敌人
            
        int num = Random.Range(1, 4);                                             //生成随机数范围1-2
        if(num == 1){
            //实例化enemyPrefab[]敌人数组中的第一个
            enemy = Instantiate(enemyPrefab[0], spawnPos, Quaternion.identity);
            FindObjectOfType<Enemy>().Fire();                                     //调用Enemy的Fire方法，在实例化敌人后让敌人开火
        }
        else if(num == 3){
            //实例化enemyPrefab[]敌人数组中的第二个
            enemy = Instantiate(enemyPrefab[1], spawnPos, Quaternion.identity);
            FindObjectOfType<Enemy>().Fire();                                     //调用Enemy的Fire方法，在实例化敌人后让敌人开火
        }
        enemy.transform.parent = enemyHolder;                                     //将enemyHolder作为生成敌人的父级
        
    }

    // 生成第一个boss的方法
    public void SpawnNewBoss1()
    {
        Instantiate (boss1, new Vector3 (0f, 10.0f, 0f), Quaternion.identity);
    }

    // 生成第二个boss的方法
    public void SpawnNewBoss2()
    {
        Instantiate (boss2, new Vector3 (0f, 9.0f, 0f), Quaternion.identity);
    }

    // 游戏开始的方法
    public void GameStart()
    {
        isPlaying = true;                                                           //游戏开始
        gameStartPanel.SetActive(false);                                            //隐藏gameStartPanel
    }

    // 游戏结束的方法
    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        isPlaying = false;                                                          //停止生成敌人
        FindObjectOfType<PlayerController>().gameObject.SetActive(false);           //销毁玩家
        Destroy(enemyHolder.gameObject);                                            //销毁全屏敌人
    }

    // 重新开始游戏的方法
    public void Retry()
    {
        SceneManager.LoadScene(0);                                                  //只有一个0号场景，在重新开始游戏的时候加载它
    }
}
