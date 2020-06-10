using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float spawnRate;
    float timer;
    public GameObject[] enemyPrefab;                                                //一系列敌人
    public Transform enemyHolder;                                                   //将生成的敌人保存在enemyHolder中
    public GameObject enemy;
    

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer > spawnRate)
        {
            timer = 0;
            SpawnNewEnemy();                                                      //调用SpawnNewEnemy方法
        }
    }

    //生成敌人的方法
    void SpawnNewEnemy()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-28f, 28f), 15f);             //在区间内生成敌人
        int num = Random.Range(1, 3);                                             //生成随机数范围1-2
		if(num == 1){
            //实例化enemyPrefab[]敌人数组中的第一个
            enemy = Instantiate(enemyPrefab[0], spawnPos, Quaternion.identity);
        }
        else if(num == 2){
            //实例化enemyPrefab[]敌人数组中的第二个
            enemy = Instantiate(enemyPrefab[1], spawnPos, Quaternion.identity);
        }
        enemy.transform.parent = enemyHolder;                                     //将enemyHolder作为生成敌人的父级

    }
}
