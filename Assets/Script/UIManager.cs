using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                           //允许我们使用UI

public class UIManager : MonoBehaviour
{
    public Text scoreText;                                                      //分数text
    public Image first, second, third, fourth, fifth, sixth, seventh;           //7滴血图片                               
    int life = 7;                                                               //7滴血
    int score;                                                                  //计分
    
    // 计分
    public void GetScore(int amount)
    {
        // zero.Length = 4
        // zero.Length - score.ToString().Length
        // string zero = "00";
        score += amount;
        //更改scoreText中的分数值
        scoreText.text = "SCORE:" + score;
        // scoreText.text = "SCORE:" + zero.Substring(0, zero.Length - score.ToString().Length) + score;

        // 玩家分数为1000的时候，生成第一个boss
        if(score == 1000)
        {
            // 清空全屏普通敌人
            FindObjectOfType<GameManager>().DesEnemy();
            // 生成boss1
            FindObjectOfType<GameManager>().SpawnNewBoss1();
        }
        
        // 玩家分数大于等于1500分，小于10000分的时候生成敌人3
        if(score >= 1500)
        {
            FindObjectOfType<GameManager>().SpawnNewEnemy3();
        }

        // 玩家分数等于10000的时候，生成第二个boss
        if(score == 10000)
        {
            FindObjectOfType<GameManager>().SpawnNewBoss2();
        }

        // 玩家分数大于等于20000的时候，生成敌人4
        if(score >= 20000)
        {
            Invoke("FindObjectOfType<GameManager>().SpawnNewEnemy4()",1);
            // FindObjectOfType<GameManager>().SpawnNewEnemy4();
        }
    }

    // 掉血
    public void TakeDamage()
    {
        life--;
        // 血量为7
        if(life == 7)
        {
            first.enabled = true;
            second.enabled = true;
            third.enabled = true;
            fourth.enabled = true;
            fifth.enabled = true;
            sixth.enabled = true;
            seventh.enabled = true;
        }
        // 血量为6
        else if(life == 6)
        {
            first.enabled = true;
            second.enabled = true;
            third.enabled = true;
            fourth.enabled = true;
            fifth.enabled = true;
            sixth.enabled = true;
            seventh.enabled = false;
        }
        // 血量为5
        else if(life == 5)
        {
            first.enabled = true;
            second.enabled = true;
            third.enabled = true;
            fourth.enabled = true;
            fifth.enabled = true;
            sixth.enabled = false;
            seventh.enabled = false;
        }
        // 血量为4
        else if(life == 4)
        {
            first.enabled = true;
            second.enabled = true;
            third.enabled = true;
            fourth.enabled = true;
            fifth.enabled = false;
            sixth.enabled = false;
            seventh.enabled = false;
        }
        // 血量为3
        else if(life == 3)
        {
            // 显示前三张图
            first.enabled = true;
            second.enabled = true;
            third.enabled = true;
            fourth.enabled = false;
            fifth.enabled = false;
            sixth.enabled = false;
            seventh.enabled = false;
        }
        // 血量为2
        else if(life == 2)
        {
            // 显示前两张图
            first.enabled = true;
            second.enabled = true;
            third.enabled = false;
            fourth.enabled = false;
            fifth.enabled = false;
            sixth.enabled = false;
            seventh.enabled = false;
        }
        // 血量为1
        else if(life == 1)
        {
            // 显示第一张图
            first.enabled = true;
            second.enabled = false;
            third.enabled = false;
            fourth.enabled = false;
            fifth.enabled = false;
            sixth.enabled = false;
            seventh.enabled = false;
        }
        // 血量为0
        else
        {
            // 全部隐藏
            first.enabled = false;
            second.enabled = false;
            third.enabled = false;
            fourth.enabled = false;
            fifth.enabled = false;
            sixth.enabled = false;
            seventh.enabled = false;

            // GameOver
            FindObjectOfType<GameManager>().GameOver();                                     //调用GameManager的GameOver方法
        }
    }

    // 恢复血量
    public void RestoreHealth()
    {
        if(life < 7)
        {
            life ++;
            // 血量为7
            if(life == 7)
            {
                first.enabled = true;
                second.enabled = true;
                third.enabled = true;
                fourth.enabled = true;
                fifth.enabled = true;
                sixth.enabled = true;
                seventh.enabled = true;
            }
            // 血量为6
            if(life == 6)
            {
                first.enabled = true;
                second.enabled = true;
                third.enabled = true;
                fourth.enabled = true;
                fifth.enabled = true;
                sixth.enabled = true;
                seventh.enabled = false;
            }
            // 血量为5
            if(life == 5)
            {
                first.enabled = true;
                second.enabled = true;
                third.enabled = true;
                fourth.enabled = true;
                fifth.enabled = true;
                sixth.enabled = false;
                seventh.enabled = false;
            }
            // 血量为4
            if(life == 4)
            {
                first.enabled = true;
                second.enabled = true;
                third.enabled = true;
                fourth.enabled = true;
                fifth.enabled = false;
                sixth.enabled = false;
                seventh.enabled = false;
            }
            // 血量为3
            if(life == 3)
            {
                // 显示前三张图
                first.enabled = true;
                second.enabled = true;
                third.enabled = true;
                fourth.enabled = false;
                fifth.enabled = false;
                sixth.enabled = false;
                seventh.enabled = false;
            }
            // 血量为2
            else if(life == 2)
            {
                // 显示前两张图
                first.enabled = true;
                second.enabled = true;
                third.enabled = false;
                fourth.enabled = false;
                fifth.enabled = false;
                sixth.enabled = false;
                seventh.enabled = false;
            }
        }
    }
}
