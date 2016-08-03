using UnityEngine;
using System.Collections;
using System;

public class csScoreSystem : MonoBehaviour {

    public csGameSystem gameMgr = null;
    public csResourceMgr resourceMgr = null;

    Sprite[] NumImageArray = null;

    public GameObject Number = null;
    const int nArraySize = 8;
    const float fSize = 0.6f;
    GameObject[] NumberArray = new GameObject[nArraySize];

    public int nGameScore = 0;
	// Use this for initialization
    void Awake ()
    {
        gameMgr = FindObjectOfType(typeof(csGameSystem)) as csGameSystem;
        resourceMgr = gameMgr.resourceMgr;
        NumImageArray = resourceMgr.GetmNumberSpriteArray;
    }
	void Start () {

        for (int i = 0; i < nArraySize; ++i)
        {
            GameObject item = Instantiate(Number);
            item.name = "Number";
            item.transform.parent = this.transform;
            item.transform.position = this.transform.position + Vector3.left * i * fSize;
            item.GetComponent<SpriteRenderer>().sprite = NumImageArray[0];
            NumberArray[i] = item;
        }
	}
	
	// Update is called once per frame
	void Update () {

        CheckScoreImage();
    }

    void CheckScoreImage()
    {
        if (nGameScore <= 0) return;

        for(int i = 0; i < nArraySize; ++i)
        {
            int nDest = (int)Math.Pow(10.0, i + 1);
            int nSour = nGameScore % nDest;

            int nResult = nSour / (nDest / 10);

            NumberArray[i].GetComponent<SpriteRenderer>().sprite = NumImageArray[nResult];
        }
    }

    public void IncreaseScore(int score)
    {
        nGameScore += score;
    }
}
