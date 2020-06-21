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
    public GameObject enemy3;                                                       //敌人3
    public GameObject enemy4;                                                       //敌人4
    public Transform enemyHolder;                                                   //将生成的敌人保存在enemyHolder中
    public GameObject enemy;
    public GameObject gameOverPanel;                                                //游戏结束图层
    public GameObject gameStartPanel;                                               //游戏开始图层
    bool isPlaying = true;                                                          //游戏是否开始
    public static GameManager instance = null;				                        //GameManager的静态实例，允许任何其他脚本访问它。
    bool isEnemy = true;		                                                    //布尔值检查是否生成普通敌人
    bool isEnemy3 = true;		                                                    //布尔值检查是否生成敌人3
    
    
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
        if(isEnemy){

            Vector2 spawnPos = new Vector2(Random.Range(-28f, 28f), 15f);             //在区间内生成敌人
            
            int num = Random.Range(1, 3);                                             //生成随机数范围1-2
            if(num == 1){
                //实例化enemyPrefab[]敌人数组中的第一个
                enemy = Instantiate(enemyPrefab[0], spawnPos, Quaternion.identity);
                FindObjectOfType<Enemy>().Fire();                                     //调用Enemy的Fire方法，在实例化敌人后让敌人开火
            }
            else if(num == 2){
                //实例化enemyPrefab[]敌人数组中的第二个
                enemy = Instantiate(enemyPrefab[1], spawnPos, Quaternion.identity);
                FindObjectOfType<Enemy>().Fire();                                     //调用Enemy的Fire方法，在实例化敌人后让敌人开火
            }
            enemy.transform.parent = enemyHolder;                                     //将enemyHolder作为生成敌人的父级
        
        }  
    }

    // 生成Enemy3的方法
    public void SpawnNewEnemy3()
    {
        
        if(isEnemy3)
        {
            
            Vector3 spawnPos = new Vector3(Random.Range(-28.0f, 28.0f), 14.0f, 0f);  //在区间内生成敌人3
            Instantiate (enemy3, spawnPos, Quaternion.identity);
            isEnemy = false;                                                         //生成敌人三后不再生成普通敌人
        }
        
    }

    // 生成Enemy4的方法
    public void SpawnNewEnemy4()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-28.0f, 28.0f), 13.0f);         //在区间内生成敌人3
        Instantiate (enemy4, spawnPos, Quaternion.identity);
        isEnemy3 = true;                                                            //生成第二个boss后不再生成敌人3
    }

    // 生成第一个boss的方法
    public void SpawnNewBoss1()
    {
        Instantiate (boss1, new Vector3 (0f, 9.0f, 0f), Quaternion.identity);
        isEnemy = false;                                                            //生成第一个boss后不再生成普通敌人
    }

    // 生成第二个boss的方法
    public void SpawnNewBoss2()
    {
        Instantiate (boss2, new Vector3 (0f, 9.0f, 0f), Quaternion.identity);
        isEnemy3 = false;                                                           //生成第二个boss后不再生成敌人3
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
        DesEnemy();                                                                 //销毁全屏敌人
    }

    // 销毁全屏敌人的方法
    public void DesEnemy()
    {
        Destroy(enemyHolder.gameObject);                                            //销毁全屏敌人
    }

    // 重新开始游戏的方法
    public void Retry()
    {
        SceneManager.LoadScene(0);                                                  //只有一个0号场景，在重新开始游戏的时候加载它
    }
}
