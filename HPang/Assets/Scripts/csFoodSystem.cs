using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using QuickPool;

namespace FoodInfo
{
    public enum FOODTYPE
    {
        eFoodNone = -1,
        eFoodHay, // 건초
        eFoodGrass, // 풀
        eFoodLarva, // 애벌래
        eFoodFire,
        eFoodCow,
        eFoodCheese,
        eFoodLeaf,
        eFoodChicken,
        eFoodAmond,
        eFoodApple,
        eFoodCorn,
        eFoodRice,
        eFoodMouse,
        eFoodAcorn, // 도토리
        eFoodFeed,
        eFoodEgg,
        eFoodCarrot,
        eFoodHorse,
        eFoodPaper,
        eFoodSeed,
        eFoodSheep,
        eFoodBanana,
        eFoodBread,
        eFoodCookedRice, // 밥
        eFoodBarley,
        eFoodTypesNum
    }

    public struct FoodData
    {
        public FOODTYPE eType;
        public int nScore;

        public FoodData(FOODTYPE type, int score) { eType = type;  nScore = score; }
    }

    public class FoodManager
    {
        static FoodManager pointer = null;
        FoodData[] tFoodTypeList = null;

        public FoodData HashFoodData(FOODTYPE type){ return tFoodTypeList[(int)type]; }
        public FoodData HashFoodData(int nNum) { return tFoodTypeList[nNum]; }

        public static FoodManager getInstance()
        {
            if (null == pointer)
            {
                pointer = new FoodManager();
                pointer.SettingTypeList();
            }
            return pointer;
        }

        public void SettingTypeList()
        {
            tFoodTypeList = new FoodData[(int)FOODTYPE.eFoodTypesNum];
            for (int i = 0; i < ((int)(FOODTYPE.eFoodTypesNum)); ++i)
            {
                tFoodTypeList[i] = new FoodData((FOODTYPE)i, 1000);
            }
            tFoodTypeList[(int)FOODTYPE.eFoodFire].nScore = 3000;
        }
    }
}

public class csFoodSystem : MonoBehaviour {

    FoodInfo.FOODTYPE[] FoodBoxCanTypeArray = new FoodInfo.FOODTYPE[12];        // 선택된 동물이 먹는 먹이종류
    public int nFoodtypeNum = 0;
    public csGameSystem gameMgr = null;
    public csResourceMgr resourceMgr = null;
    public csFoodBox FoodBox = null;
    public csLifeSystem lifeSystem = null;
    public csPigTimeSystem pigtimeSystem = null;
    public csScoreSystem scoreSystem = null;
    public csComboSystem comboSystem = null;
    public csSelectedManger selectMgr = null;


    public GameObject objFoodFeedBack = null;
    Sprite[] FeedBackImg = null;
    int nFoodFeedBack = -1;
    float fFeedAccTime = 0.0f;
    bool bCheckInFeedBack = false;

    public GameObject objFoodSelHelper = null;

    LinkedList<FoodInfo.FOODTYPE> FoodStorageList = new LinkedList<FoodInfo.FOODTYPE>();

    // Use this for initialization
    void Awake()
    {
        lifeSystem = gameMgr.lifeSystem;
        pigtimeSystem = gameMgr.pigtimeSystem;
        scoreSystem = gameMgr.scoreSystem;
        comboSystem = gameMgr.comboSystem;
        selectMgr = gameMgr.selectMgr;
    }

    void Start()
    {
        //print(resourceMgr);
        FoodBox = GetComponentInChildren<csFoodBox>();
        FeedBackImg = resourceMgr.mFeedBackSprites;

        // 푸드박스 채우기
        for (int y = 0; y < FoodBox.nRowSize; ++y)
        {
            for (int x = 0; x < FoodBox.nColSize; ++x)
            {
                int nIndex = (int)FoodBoxCanTypeArray[Random.Range(0, nFoodtypeNum)];
                //int nIndex = Random.Range(0, 5);
                FoodBox.FoodList[y, x].GetComponent<csFoodItem>().gameSystem = gameMgr;
                FoodBox.FoodList[y, x].GetComponent<csFoodItem>().SettingFoodData(
                    resourceMgr.GetFoodSpriteArray[nIndex],
                    FoodInfo.FoodManager.getInstance().HashFoodData(nIndex));
            }
        }
        // 저장리스트 채우기
        for (int i = 0; i < 1000; ++i)
        {
            FoodInfo.FOODTYPE val = FoodBoxCanTypeArray[Random.Range(0, nFoodtypeNum)];
            FoodStorageList.AddLast(val);
        }
    }

    // Update is called once per frame
    void Update() {
        CheckEattingFood();

        fFeedAccTime += Time.deltaTime;
        if(true == bCheckInFeedBack && fFeedAccTime > 0.35f)
        {
            bCheckInFeedBack = false;
            objFoodFeedBack.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void ComposeFoodBoxList(int num, FoodInfo.FOODTYPE type)
    {
        FoodBoxCanTypeArray[num] = type;
    }
    void CheckEattingFood()
    {
        // 도화선 폭파 알고리즘
        int BoomCont = 0;
        bool bCheckClick = false;
        for (int y = 0; y < FoodBox.nRowSize; ++y)
        {
            for (int x = 0; x < FoodBox.nColSize; ++x)
            {
                csFoodItem FoodItem = FoodBox.FoodList[y, x].GetComponent<csFoodItem>();

                if (true == FoodItem.bClick)
                    bCheckClick = true;
                if (true == FoodItem.bPangForComboCheck)
                {
                    //ChangeFoodItemInStorage(FoodItem);
                    //GatherScore();
                    //FoodItem.Efx.Spawn(FoodItem.transform.position, Quaternion.identity);
                    //FoodItem.bChaining = false;
                    FoodItem.bPangForComboCheck = false;
                    ++BoomCont;
                }
            }
        }
        if(0 != BoomCont)
            comboSystem.IncreaseComboCount(1);

        if(8 <= BoomCont)
            Handheld.Vibrate();

        if (0 == BoomCont && Input.GetMouseButtonDown(0) && bCheckClick)
        {
            RenderEatFoodFeedBack(0);
        }
    }

    public void RenderEatFoodFeedBack(int num)
    {
        print(num);
        fFeedAccTime = 0.0f;
        bCheckInFeedBack = true;
        nFoodFeedBack = num;
        objFoodFeedBack.GetComponent<SpriteRenderer>().sprite = FeedBackImg[nFoodFeedBack];
        objFoodFeedBack.GetComponent<SpriteRenderer>().enabled = true;
        objFoodFeedBack.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        iTween.ScaleTo(objFoodFeedBack, iTween.Hash("x", 3.0f, "y", 3.0f, "time", 0.35f
                          , "easetype", iTween.EaseType.easeOutElastic));

    }
    // 도화선 찾기
    public void ExplosionNearFoodItem(int y, int x, FoodInfo.FOODTYPE type, int dist)
    {
        if (x < 0 || y < 0 || x >= FoodBox.nRowSize || y >= FoodBox.nColSize) return;
        if (FoodBox.FoodList[y, x].activeInHierarchy == false) return;

        bool bCheck = false;
        if (FoodBox.FoodList[y, x].GetComponent<csFoodItem>().bChaining == false
            && FoodBox.FoodList[y, x].GetComponent<csFoodItem>().GetSetFoodData.eType == type)
        {
            bCheck = true;
            FoodBox.FoodList[y, x].GetComponent<csFoodItem>().bChaining = true;
            FoodBox.FoodList[y, x].GetComponent<csFoodItem>().nDist = dist;
            FoodBox.FoodList[y, x].GetComponent<csFoodItem>().bPangForComboCheck = true;
        }

        if (true == bCheck)
        {
            if (y % 2 == 0)
            {
                ExplosionNearFoodItem(y - 1, x - 1, type, dist + 1);
                ExplosionNearFoodItem(y - 1, x, type, dist + 1);
                ExplosionNearFoodItem(y + 1, x - 1, type, dist + 1);
                ExplosionNearFoodItem(y + 1, x, type, dist + 1);
                ExplosionNearFoodItem(y, x - 1, type, dist + 1);
                ExplosionNearFoodItem(y, x + 1, type, dist + 1);
            }
            else
            {
                ExplosionNearFoodItem(y - 1, x, type, dist + 1);
                ExplosionNearFoodItem(y - 1, x + 1, type, dist + 1);
                ExplosionNearFoodItem(y + 1, x, type, dist + 1);
                ExplosionNearFoodItem(y + 1, x + 1, type, dist + 1);
                ExplosionNearFoodItem(y, x - 1, type, dist + 1);
                ExplosionNearFoodItem(y, x + 1, type, dist + 1);
            }
        }
    }

    // 먹으면 저장리스트에서 다른걸 꺼내옴
    public void ChangeFoodItemInStorage(csFoodItem item)
    {
        int nIndex = -1;
        FoodInfo.FOODTYPE val = FoodBoxCanTypeArray[Random.Range(0, nFoodtypeNum)];
        FoodStorageList.AddLast(val);
        FoodStorageList.RemoveFirst();
        nIndex = (int)FoodStorageList.First.Value;

        //print(nIndex);
        if (nIndex == -1) print("error");
        // 늦게 등장
        item.bChange = true;
        item.GetComponent<SpriteRenderer>().enabled = false;
        //
        item.SettingFoodData(resourceMgr.GetFoodSpriteArray[nIndex],
            FoodInfo.FoodManager.getInstance().HashFoodData(nIndex));
    }

    // 점수 주기
    public void GatherScore(int nWhatFood)
    {
        int DefaultScore = 1000;
        int nComboCnt = comboSystem.nComboCnt;
        float fComCntPercent = nComboCnt * 0.01f;
        float fLikePercent = 0.0f;

        switch(nWhatFood){
            case 3:   fLikePercent = 2.0f;   break; // excellent
            case 2:   fLikePercent = 1.5f;   break;// great
            case 1:   fLikePercent = 1.0f;   break;// good
        }
        float fLifeValue = 4.0f;
        float Score;
        if(true == pigtimeSystem.bPigTime)
        {
            Score = (DefaultScore * 2.0f + DefaultScore * fComCntPercent) * fLikePercent;
            lifeSystem.IncreaseLifePoint(fLifeValue * 2.0f);
            scoreSystem.IncreaseScore((int)(Score));
        }
        else
        {
            // 콤보에 따른 피그타임 증가수치
            float fPlusPigValue = 1.0f;
            if (nComboCnt >= 100 && nComboCnt < 200) fPlusPigValue *= 2.0f;
            if (nComboCnt >= 200) fPlusPigValue *= 4.0f;

            Score = (DefaultScore + DefaultScore * fComCntPercent) * fLikePercent;
            lifeSystem.IncreaseLifePoint(fLifeValue);
            pigtimeSystem.IncreasePigValue(fPlusPigValue);
            scoreSystem.IncreaseScore((int)(Score));
        }
    }

    public bool CheckPigTime()
    {
        return pigtimeSystem.GetSetPigTime;
    }
}
