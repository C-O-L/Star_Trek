using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyThree : MonoBehaviour
{
    public float moveSpeed;                                                 //移动速度
    public float bulletSpeed;                                               //子弹速度
    public GameObject bulletPrefab;                                         //子弹
    Rigidbody2D rb;
    public float fireRate;                                                  //发射频率
    float timer;                                                            //计时                                                       
    public GameObject exploPrefab;                                          //爆炸动画
    public GameObject dropBloodPrefab;                                      //掉血动画
    public GameObject[] PropTitle;                                          //一系列道具
    public GameObject prop;
    int hp = 1000;                                                          //血量

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                                   //抓取Rigidbody2D组件
        rb.velocity = Vector2.down * moveSpeed;                             //设置速度的方向和大小
    }
        
    private void Update() {
        // 开火
        timer += Time.deltaTime;
        if(timer > fireRate)                                                //每隔fireRate发一次子弹
        {
            Fire();
            timer = 0;
        }
    }

    //发射子弹的方法
    public void Fire()
    {
        Vector3 fireDirection = transform.up;                               //发射方向
        //使用四元数制造绕Z轴旋转45度的旋转
        Quaternion startQuaternion = Quaternion.AngleAxis(45, Vector3.forward);
        for(int j = 0; j < 8; j++)
        {
            //生成子弹
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            //抓取子弹的Rigidbody2D组件，设置子弹速度的方向和大小
            bullet.GetComponent<Rigidbody2D>().velocity = fireDirection * bulletSpeed;
            fireDirection = startQuaternion * fireDirection;                //发射方向旋转45度，到达下一个方向
            //定时销毁子弹
            Destroy(bullet, 15.0f);
        }
    }

    //子弹碰撞
    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision);
        //如果碰到普通子弹
        if(collision.CompareTag("Bullet"))
        {
            //在自己的位置生成dropBloodPrefab，不旋转, 并保存在explode中
            GameObject explode = Instantiate(dropBloodPrefab, transform.position, Quaternion.identity);
            Destroy(explode, 0.5f);                                         //0.5s之后销毁explode
            Destroy(collision.gameObject);                                  //销毁子弹

            TakeDamage(100);                                                //调用TakeDamage方法,血量减100
        }
        //如果碰到子弹plus
        if(collision.CompareTag("BulletPlus"))
        {
            //在自己的位置生成dropBloodPrefab，不旋转, 并保存在explode中
            GameObject explode = Instantiate(dropBloodPrefab, transform.position, Quaternion.identity);
            Destroy(explode, 0.5f);                                         //0.5s之后销毁explode
            Destroy(collision.gameObject);
            TakeDamage(200);                                                //调用TakeDamage方法,血量减200
        }
    }
    
    // Enemy3碰撞方法
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision);
        // 如果碰到玩家
        if(collision.gameObject.CompareTag("Player"))
        {
            //在自己的位置生成dropBloodPrefab，不旋转, 并保存在explode中
            GameObject explode = Instantiate(dropBloodPrefab, transform.position, Quaternion.identity);
            Destroy(explode, 0.5f);                                         //0.5s之后销毁explode
            FindObjectOfType<UIManager>().TakeDamage();                     //调用UIManager中的TakeDamage方法,玩家掉血
            TakeDamage(100);                                                //敌人3掉血
        }
        //如果碰到场景边沿
        if(collision.gameObject.CompareTag("Border"))
        {
            //玩家掉血
            FindObjectOfType<UIManager>().TakeDamage();                     //调用UIManager中的TakeDamage方法
            // Destroy(gameObject);                                            //销毁敌人自己
        }
    }

    //死亡方法
    public void TakeDamage(int loss)
    {
        hp -= loss;
        if(hp <= 0)
        {
            //在自己的位置生成exploPrefab(爆炸动画)，不旋转, 并保存在explode中
            GameObject explode = Instantiate(exploPrefab, transform.position, Quaternion.identity);
            Destroy(explode, 0.5f);                                         //0.5s之后销毁explode
            Destroy(gameObject);                                            //销毁自己
            // 玩家加分
            FindObjectOfType<UIManager>().GetScore(500);                    //调用UIManager中的GetScore方法,每销毁一架敌机加**分
            Produce();                                                      //调用Produce()方法生成道具
        }
    }

    // 掉落道具的方法
    void Produce()
    {
        Vector2 spawnPos = new Vector2(Random.Range(-26f, 26), 15f);       //在区间内生成道具
        int num = Random.Range(1, 20);                                       //生成随机数范围1-2
		if(num == 1){
            //实例化PropTitle[]道具数组中的第一个
            prop = Instantiate(PropTitle[0], spawnPos, Quaternion.identity);
            //定时销毁道具
            Destroy(prop, 100.0f);
        }
        if(num == 8 || num == 14 || num == 19){
            //实例化PropTitle[]道具数组中的第二个
            prop = Instantiate(PropTitle[1], spawnPos, Quaternion.identity);
            //定时销毁道具
            Destroy(prop, 100.0f);
        }
    }
}
