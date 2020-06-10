using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;                                                 //移动速度
    Rigidbody2D rb;                                                         
    public GameObject exploPrefab;                                          //爆炸动画

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                   //抓取Rigidbody2D组件
        rb.velocity = Vector2.down * moveSpeed;                             //设置速度的方向和大小
    }

    //子弹碰撞
    private void OnTriggerEnter2D(Collider2D collision) {
        //如果碰到子弹
        if(collision.CompareTag("Bullet"))
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

            TakeDamage();                                                   //TakeDamage调用销毁敌人自己
        }
        
        //如果碰到场景边沿
        if(collision.gameObject.CompareTag("Border"))
        {
            //玩家掉血

            Destroy(gameObject);                                            //销毁敌人自己
        }
    }

    //死亡方法
    void TakeDamage()
    {
        //在自己的位置生成exploPrefab，不旋转, 并保存在explode中
        GameObject explode = Instantiate(exploPrefab, transform.position, Quaternion.identity);
        Destroy(explode, 0.5f);                                             //0.5s之后销毁explode
        Destroy(gameObject);                                                //销毁自己

    }
}
