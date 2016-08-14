using UnityEngine;
using System.Collections;
using System;

public class csComboSystem : MonoBehaviour {


    public int nComboCnt;

    public GameObject objCont = null;
    public GameObject objCombo = null;

    public csGameSystem gameMgr = null;
    public csResourceMgr resourceMgr = null;

    GameObject[] contArray;
    const int ArraySize = 5;
    
    Sprite[] NumImageArray = null;
    int[] ResultArray = new int[ArraySize];

    float fCombomLifeTime;
    float fAccTime;

    bool bScalingCheck = false;

    
	// Use this for initialization
    void Awake()
    {
        gameMgr = FindObjectOfType(typeof(csGameSystem)) as csGameSystem;
        resourceMgr = gameMgr.resourceMgr;
        NumImageArray = resourceMgr.GetmNumberSpriteArray;

        contArray = new GameObject[ArraySize];

        for (int i = 0; i < ArraySize; ++i)
        {
            GameObject item = Instantiate(objCont);
            item.name = "Count";
            item.transform.parent = this.transform;
            item.transform.position = objCombo.transform.position + Vector3.left * i * 0.6f + Vector3.left * 1.5f;
            item.GetComponent<SpriteRenderer>().sortingOrder = 2;
            contArray[i] = item;
        }
    }
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        fCombomLifeTime = ApplyComboLifeTime();
        fAccTime += Time.deltaTime;

        if (fCombomLifeTime < fAccTime)
        {
            nComboCnt = 0;
        }

        if (nComboCnt != 0)
            ShowComboCount();
        else
        {
            for (int i = 0; i < ArraySize; ++i)
            {
                contArray[i].GetComponent<SpriteRenderer>().enabled = false;
                objCombo.GetComponent<SpriteRenderer>().enabled = false;
            }

        }
    }

    void ShowComboCount()
    {
        for (int i = 0; i < ArraySize; ++i)
        {
            int nDest = (int)Math.Pow(10.0, i + 1);
            int nSour = nComboCnt % nDest;

            ResultArray[i] = nSour / (nDest / 10);

            contArray[i].GetComponent<SpriteRenderer>().enabled = true;
            contArray[i].GetComponent<SpriteRenderer>().sprite = NumImageArray[ResultArray[i]];
        }
        objCombo.GetComponent<SpriteRenderer>().enabled = true;
        EnableZeroData(ArraySize);
    }
    void EnableZeroData(int num)
    {
        if (num == 0) return;

        if (ResultArray[num - 1] == 0)
        {
            contArray[num - 1].GetComponent<SpriteRenderer>().enabled = false;
            EnableZeroData(num - 1);
        }
    }

    float ApplyComboLifeTime()
    {
        float fDefaultTime = 2.0f;
        int nCntSize = 50;

        if (nComboCnt < nCntSize) fDefaultTime = 2.0f;
        else if (nComboCnt >= nCntSize && nComboCnt < nCntSize * 2) fDefaultTime = 1.7f;
        else if (nComboCnt >= nCntSize * 2 && nComboCnt < nCntSize * 3) fDefaultTime = 1.7f;
        else if (nComboCnt >= nCntSize * 3 && nComboCnt < nCntSize * 4) fDefaultTime = 1.4f;
        else if (nComboCnt >= nCntSize * 4 && nComboCnt < nCntSize * 5) fDefaultTime = 1.1f;
        else if (nComboCnt >= nCntSize * 5 && nComboCnt < nCntSize * 6) fDefaultTime = 0.8f;
        else if (nComboCnt >= nCntSize * 6) fDefaultTime = 0.5f;

        return fDefaultTime;
    }


    public void IncreaseComboCount(int plusCount)
    {
        fAccTime = 0.0f;
        nComboCnt += plusCount;
        for(int i = 0; i < ArraySize; ++i)
        {
            contArray[i].transform.localScale = Vector3.one * -1;
        }

        for (int i = 0; i < ArraySize; ++i)
        {
            iTween.ScaleTo(contArray[i].gameObject, iTween.Hash("x", 1.5f, "y", 1.5f, "time", 0.5f
                          , "easetype", iTween.EaseType.easeOutElastic));
        }
    }
}
