using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;                                     //玩家移速
    public float bulletSpeed;                                   //子弹速度
    Rigidbody2D rb;
    AudioSource au;
    public float fireRate;                                      //发射频率
    float timer;                                                //计时
    public GameObject[] bulletPrefab;                           //子弹
    public Transform firePoint;                                 //发炮位置
    public GameObject bullet;
    int firebullet = 0;                                         //判断发射哪种子弹

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                       //抓取Rigidbody2D游戏组件，并保存在rb里
        au = GetComponent<AudioSource>();                       //抓取AudioSource游戏组件，并保存在au里
    }

    // Update is called once per frame
    void Update()
    {
        // 移动
        float h = Input.GetAxisRaw("Horizontal");               //按下右键h=1，左键h=-1,不按h=0；检测左右输入
        float v = Input.GetAxisRaw("Vertical");                 //检测上下输入

        // 按下右，上；h = 1, v = 1; dir = (1, 1)
        Vector2 dir = new Vector2(h, v).normalized;             //构造方向向量

        rb.velocity = dir * moveSpeed;                          //设置速度的方向和大小

        // 开火
        timer += Time.deltaTime;
        if(timer > fireRate && Input.GetKey(KeyCode.Space))     //按空格键
        {
            timer = 0;
            if(firebullet == 0){
                Fire();                                         //调用Fier方法
            }
            if(firebullet == 1){
                FirePlus();                                     //调用FierPlus方法
            }
            if(firebullet == 2){
                FireRosefinch();
            }
        }
    }

    //发射子弹的方法
    void Fire()
    {
        au.Play();                                              //播放发射子弹的音效

        // 实例化子弹数组中的第一个
        bullet = Instantiate(bulletPrefab[0], firePoint.position, Quaternion.identity);

        //抓取子弹的Rigidbody2D组件，速度方向设置为上，大小设置为bulletSpeed
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;

        //定时销毁子弹
        Destroy(bullet, 0.7f);
    }

    //发射子弹Plus的方法
    void FirePlus()
    {
        au.Play();                                              //播放发射子弹的音效
        
        bullet = Instantiate(bulletPrefab[1], firePoint.position, Quaternion.identity);

        //抓取子弹的Rigidbody2D组件，速度方向设置为上，大小设置为bulletSpeed
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;

        //定时销毁子弹
        Destroy(bullet, 0.7f);
    }

    //发射朱雀子弹的方法
    void FireRosefinch()
    {
        au.Play();                                              //播放发射子弹的音效
        
        bullet = Instantiate(bulletPrefab[2], transform.position, Quaternion.identity);

        //抓取子弹的Rigidbody2D组件，速度方向设置为上，大小设置为bulletSpeed
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;

        //定时销毁子弹
        Destroy(bullet, 1.0f);
    }

    // 玩家碰撞方法
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        //如果碰到敌机子弹
        if(collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.gameObject);                        //销毁子弹
            FindObjectOfType<UIManager>().TakeDamage();           //调用UIManager中的TakeDamage方法
        }
        //如果碰到子弹道具
        if(collision.CompareTag("BulletProp"))
        {
            firebullet = 1;
            Destroy(collision.gameObject);                        //销毁子弹道具
        }
        //如果碰到生命道具
        if(collision.CompareTag("LifeProp"))
        {
            FindObjectOfType<UIManager>().RestoreHealth();        //调用UIManager的RestoreHealth方法，恢复血量
            Destroy(collision.gameObject);                        //销毁生命道具
        }
        //如果碰到朱雀子弹道具
        if(collision.CompareTag("RosefinchProp"))
        {
            firebullet = 2;
            Destroy(collision.gameObject);
        }
    }
}
