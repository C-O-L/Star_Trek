using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;                                                 //移动速度
    public float bulletSpeed;                                               //子弹速度
    public GameObject bulletPrefab;                                         //子弹
    public Transform firePoint;                                             //发炮位置
    Rigidbody2D rb;
    public float fireRate;                                                  //发射频率
    float timer;                                                            //计时                                                         
    public GameObject exploPrefab;                                          //爆炸动画
    public GameObject[] PropTitle;                                          //一系列道具
    public GameObject prop;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                   //抓取Rigidbody2D组件
        rb.velocity = Vector2.down * moveSpeed;                             //设置速度的方向和大小
    }

    private void Update() {
        // 开火
        timer += Time.deltaTime;
        if(timer > fireRate)                                                //每隔fireRate发一颗子弹
        {
            Fire();
            timer = 0;
        }
    }

    //发射子弹的方法
    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        //抓取子弹的Rigidbody2D组件，速度方向设置为下，大小设置为bulletSpeed
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.down * bulletSpeed;
        //定时销毁子弹
        Destroy(bullet, 3.0f);   
    }

    //子弹碰撞
    private void OnTriggerEnter2D(Collider2D collision) {
        //如果碰到普通子弹
        if(collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);                                  //销毁子弹
            TakeDamage();                                                   //调用TakeDamage方法
        }
        //如果碰到子弹plus
        if(collision.CompareTag("BulletPlus"))
        {
            Destroy(collision.gameObject);                                  //销毁子弹
            TakeDamage();                                                   //调用TakeDamage方法
        }
    }
    
    // 敌人碰撞方法
    private void OnCollisionEnter2D(Collision2D collision) {
        // 如果碰到玩家
        if(collision.gameObject.CompareTag("Player"))
        {
            //玩家掉血
            FindObjectOfType<UIManager>().TakeDamage();                     //调用UIManager中的TakeDamage方法
            TakeDamage();                                                   //TakeDamage调用销毁敌人自己
        }
        
        //如果碰到场景边沿
        if(collision.gameObject.CompareTag("Border"))
        {
            //玩家掉血
            FindObjectOfType<UIManager>().TakeDamage();                     //调用UIManager中的TakeDamage方法
            Destroy(gameObject);                                            //销毁敌人自己
        }
    }

    //死亡方法
    void TakeDamage()
    {
        //在自己的位置生成exploPrefab，不旋转, 并保存在explode中
        GameObject explode = Instantiate(exploPrefab, transform.position, Quaternion.identity);
        // 玩家加分
        FindObjectOfType<UIManager>().GetScore(50);                         //调用UIManager中的GetScore方法,每销毁一架敌机加**分
        Destroy(explode, 0.5f);                                             //0.5s之后销毁explode
        Destroy(gameObject);                                                //销毁自己

        Produce();                                                          //调用Produce()方法生成道具
    }

    // 掉落道具的方法
    void Produce()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-26f, 26f), 15f);       //在区间内生成道具
        int num = Random.Range(1, 30);                                      //生成随机数范围1-29
		if(num == 1 || num == 29){
            //实例化PropTitle[]道具数组中的第一个
            prop = Instantiate(PropTitle[0], spawnPos, Quaternion.identity);
        }
        if(num == 10 || num == 20){
            //实例化PropTitle[]道具数组中的第二个
            prop = Instantiate(PropTitle[1], spawnPos, Quaternion.identity);
        }
    }

}
