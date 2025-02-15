﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS : MonoBehaviour
{
    float deltaTime = 0.0f;

    GUIStyle style;
    Rect rect;
    
    float msec;
    float fps;
    float worstFps = 100.0f;
    string text;

    public int targetFps = 60;

    void Awake()
    {
        int w = Screen.width, h = Screen.height;

        rect = new Rect(0, 0, w, h * 8 / 100);

        style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 8 / 100;
        style.normal.textColor = Color.white;

        StartCoroutine("worstReset");
    }

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFps;
    }

    IEnumerator worstReset() // 코루틴으로 15초 간격으로 최저 프레임 리셋해줌.
    {
        while (true)
        {
            yield return new WaitForSeconds(15.0f);
            worstFps = 100.0f;
        }
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
    }

    /*
    void OnGUI() // 소스로 GUI 표시.
    {
        msec = deltaTime * 1000.0f;
        fps = 1.0f / deltaTime; // 초당 프레임 - 1초에

        if (fps < worstFps) // 새로운 최저 fps가 나왔다면 worstFps 바꿔줌.
        {
            worstFps = fps;
        }
        text = msec.ToString ("F1") + "ms  (" + fps.ToString ("F1") + ") // worst : " + worstFps.ToString ("F1");
        GUI.Label(rect, text, style);
    }
    */
}
