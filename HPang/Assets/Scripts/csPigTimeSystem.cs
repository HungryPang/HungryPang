﻿using UnityEngine;
using System.Collections;

public class csPigTimeSystem : MonoBehaviour {

    public float fPigValue = 0.0f;
    public float fPigMaxValue = 100.0f;
    public float fPigValueRate;

    public bool bPigTime = false;
    public bool GetSetPigTime{
        get { return bPigTime; }
        set { GetSetPigTime = bPigTime; }
    }

    public GameObject objPig = null;
    public csGameSystem gameMgr = null;
    Vector3 vInitScale;
   
    float fAccTime = 0.0f;
    float fPigAccTime = 0.0f;
    public float fScaleDelta = 1.0f;
    public float fSpeed = 10.0f;
    public float fDecrease = 30.0f;


    // Use this for initialization
    void Start () {
        gameMgr = FindObjectOfType(typeof(csGameSystem)) as csGameSystem;
        objPig = GameObject.Find("PigImage");
        vInitScale = objPig.GetComponent<Transform>().localScale;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (true == bPigTime)
        {
            fAccTime += Time.deltaTime;

            ShowPigTime();
            DecreasePigValue(fAccTime);
        }

    }
    // 피그타임일때 보여주는 것
    void ShowPigTime()
    {
        fPigAccTime += Time.deltaTime * fSpeed;

        objPig.GetComponent<SpriteRenderer>().enabled = true;
        objPig.GetComponent<Transform>().localScale = new Vector3(vInitScale.x + Mathf.Sin(fPigAccTime) * fScaleDelta
                                                                , vInitScale.y + Mathf.Sin(fPigAccTime) * fScaleDelta
                                                                , vInitScale.z);
    }
    // 피그타임 게이지 상승
    public void IncreasePigValue(float value)
    {
        if (bPigTime) return;   // 피그타임일떈 게이지상승 x

        fPigValue += value;
        if (fPigValue >= fPigMaxValue)
        {
            bPigTime = true;
            fPigValue = fPigMaxValue;
        }
    }
    // 피그타임 게이지 감소
    public void DecreasePigValue(float time)
    {
        fPigValue -= Time.deltaTime * fDecrease;
        if(fPigValue <= 0)
        {
            fPigValue = 0.0f;
            EndPigTime();
        }
    }
    // 피그타임 종료 선언
    public void EndPigTime()
    {
        fAccTime = 0.0f;
        fPigAccTime = 0.0f;
        objPig.GetComponent<Transform>().localScale = vInitScale;

        bPigTime = false;
        objPig.GetComponent<SpriteRenderer>().enabled = false;
    }
}