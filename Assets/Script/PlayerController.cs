using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;                                     //玩家移速
    public float bulletSpeed;                                   //子弹速度
    Rigidbody2D rb;
    public float fireRate;
    float timer;                                                //计时
    public GameObject bulletPrefab;                             //子弹
    public Transform firePoint;                                 //发炮位置

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                       //抓取Rigidbody2D游戏组件，并保存在rb里
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
            Fire();                                             //调用Fier方法
        }
    }

    //发射子弹的方法
    void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        //抓取子弹的Rigidbody2D组件，速度方向设置为上，大小设置为bulletSpeed
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.up * bulletSpeed;

        //定时销毁子弹
        Destroy(bullet, 0.7f);
    }
}
