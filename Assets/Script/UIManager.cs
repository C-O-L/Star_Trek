using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;                                                           //允许我们使用UI

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Image first, second, third;                                          //三滴血图片
    int life = 3;                                                               //三滴血
    int score;                                                                  //计分

    // 计分
    public void GetScore(int amount)
    {
        // zero.Length = 4
        // zero.Length - score.ToString().Length
        string zero = "0000";
        score += amount;
        //更改scoreText中的分数值
        scoreText.text = "SCORE:" + zero.Substring(0, zero.Length - score.ToString().Length) + score;  
    }

    // 掉血
    public void TakeDamage()
    {
        life--;
        // 血量为3
        if(life == 3)
        {
            // 三张图全部显示
            first.enabled = true;
            second.enabled = true;
            third.enabled = true;
        }
        // 血量为2
        else if(life == 2)
        {
            // 隐藏第三张图
            first.enabled = true;
            second.enabled = true;
            third.enabled = false;
        }
        // 血量为1
        else if(life == 1)
        {
            // 隐藏后两张图
            first.enabled = true;
            second.enabled = false;
            third.enabled = false;
        }
        // 血量为0
        else
        {
            // 三张图全部隐藏
            first.enabled = false;
            second.enabled = false;
            third.enabled = false;

            // GameOver
            FindObjectOfType<GameManager>().GameOver();                                     //调用GameManager的GameOver方法
        }
    }

    // 恢复血量
    public void RestoreHealth()
    {
        if(life < 3)
        {
            life ++;
            // 血量为3
            if(life == 3)
            {
                // 三张图全部显示
                first.enabled = true;
                second.enabled = true;
                third.enabled = true;
            }
            // 血量为2
            else if(life == 2)
            {
                // 隐藏第三张图
                first.enabled = true;
                second.enabled = true;
                third.enabled = false;
            }
        }
    }
}
