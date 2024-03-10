using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgColorChange : MonoBehaviour
{
    public Camera mainCamera;
    public Color[] backgroundColors;
    private int index = 0;
     void Start()
    {
        // 初始化摄像机背景颜色
        backgroundColors = new Color[5];
        backgroundColors[0] = new Color(0f, 0f, 0f);
        backgroundColors[1] = new Color(0.147593f, 0.1399074f, 0.2264151f);
        backgroundColors[2] = new Color(0.2200516f, 0.2333094f, 0.4056604f);
        backgroundColors[3] = new Color(0.1142755f, 0.1411507f, 0.2264151f);
        backgroundColors[4] = new Color(0.1509434f, 0.1509434f, 0.1509434f);
        mainCamera.backgroundColor = backgroundColors[index];
    }

    public void ChangeColor()
    {
        // 切换到下一个颜色
        index = (index + 1) % backgroundColors.Length;

        // 更新摄像机背景颜色
        mainCamera.backgroundColor = backgroundColors[index];
    }
}
