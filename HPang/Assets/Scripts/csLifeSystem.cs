using UnityEngine;
using System.Collections;

public class csLifeSystem : MonoBehaviour {

    public GameObject objPoint = null;
    public csGameSystem gameMgr = null;

    float fLifeRate = 0.0f;
    public float fLifePoint = 0.0f;
    float fMaxPoint;

    float fAccTime = 0.0f;
    Vector3 vInitPos;
    // Use this for initialization
    void Awake()
    {
        gameMgr = FindObjectOfType(typeof(csGameSystem)) as csGameSystem;
        objPoint = GameObject.Find("LifePoint");

        fMaxPoint = fLifePoint;
        vInitPos = objPoint.transform.position;
    }
    void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        fAccTime += Time.deltaTime;

        fLifePoint -= TimeLevel(fAccTime) * Time.deltaTime;
        if (fLifePoint <= 0)
        {
            print("gameOver!");
        }
        fLifeRate = fLifePoint / fMaxPoint;

        float fMyPos = Mathf.Lerp(8.0f, 0.0f, fLifeRate);
        objPoint.transform.position = vInitPos - new Vector3(fMyPos, 0.0f, 0.0f);
    }

    public void IncreaseLifePoint(float value)
    {
        fLifePoint += value;

        if (fLifePoint >= fMaxPoint)
            fLifePoint = fMaxPoint;
    }

    int TimeLevel(float fAccTime)
    {
        int nLvl = 0;       //감소되는 수치

        if      (fAccTime > 0.0f && fAccTime <= 10.0f)      { nLvl = 5;  }
        else if (fAccTime > 10.0f && fAccTime <= 20.0f)     { nLvl = 6;  }
        else if (fAccTime > 20.0f && fAccTime <= 30.0f)     { nLvl = 7;  }
        else if (fAccTime > 30.0f && fAccTime <= 40.0f)     { nLvl = 8;  }
        else if (fAccTime > 40.0f && fAccTime <= 50.0f)     { nLvl = 9;  }
        else if (fAccTime > 50.0f && fAccTime <= 60.0f)     { nLvl = 10; }
        else if (fAccTime > 60.0f && fAccTime <= 70.0f)     { nLvl = 12; }
        else if (fAccTime > 70.0f && fAccTime <= 80.0f)     { nLvl = 14; }
        else if (fAccTime > 80.0f && fAccTime <= 90.0f)     { nLvl = 16; }
        else if (fAccTime > 90.0f && fAccTime <= 100.0f)    { nLvl = 18; }
        else if (fAccTime > 100.0f && fAccTime <= 110.0f)   { nLvl = 20; }
        else if (fAccTime > 110.0f && fAccTime <= 120.0f)   { nLvl = 22; }
        else                                                { nLvl = 23; }

        return nLvl;
    }
}
