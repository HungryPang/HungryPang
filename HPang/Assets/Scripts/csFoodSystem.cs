using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public csGameSystem     gameMgr = null;
    public csResourceMgr    resourceMgr = null;
    public csFoodBox        FoodBox = null;
    public csLifeSystem     lifeSystem = null;
    public csPigTimeSystem  pigtimeSystem = null;
    public csScoreSystem    scoreSystem = null;

    LinkedList<FoodInfo.FOODTYPE> FoodStorageList = new LinkedList<FoodInfo.FOODTYPE>();

    // Use this for initialization
    void Awake()
    {
        lifeSystem = gameMgr.lifeSystem;
        pigtimeSystem = gameMgr.pigtimeSystem;
        scoreSystem = gameMgr.scoreSystem;
    }

    void Start ()
    {
        //print(resourceMgr);
        FoodBox = GetComponentInChildren<csFoodBox>();

        // 푸드박스 채우기
        for (int y = 0; y < FoodBox.nRowSize; ++y)
        {
            for (int x = 0; x < FoodBox.nColSize; ++x)
            {
                int nIndex = (int)FoodBoxCanTypeArray[Random.Range(0, nFoodtypeNum - 1)];
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
            FoodInfo.FOODTYPE val = FoodBoxCanTypeArray[Random.Range(0, nFoodtypeNum - 1)];
            FoodStorageList.AddLast(val);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ComposeFoodBoxList(int num, FoodInfo.FOODTYPE type)
    {
        FoodBoxCanTypeArray[num] = type;
    }

    // 먹으면 저장리스트에서 다른걸 꺼내옴
    public void ChangeFoodItemInStorage(csFoodItem item)
    {
        //for(int i = 0; i < 8; ++i)
        //{
        //    for(int j = 0; j < 8; ++j)
        //    {
        //        if (FoodBox.FoodList[i, j].GetComponent<csFoodItem>() == item)
        //            print(i + "/" + j);
        //    }
        //}
        int nIndex = -1;
        FoodInfo.FOODTYPE val = item.GetSetFoodData.eType;
        FoodStorageList.AddLast(val);
        FoodStorageList.RemoveFirst();
        nIndex = (int)FoodStorageList.First.Value;

        //print(nIndex);
        if (nIndex == -1) print("error");

        item.SettingFoodData(resourceMgr.GetFoodSpriteArray[nIndex],
            FoodInfo.FoodManager.getInstance().HashFoodData(nIndex));
    }

    // 점수 주기
    public void GatherScore()
    {
        lifeSystem.IncreaseLifePoint(10.0f);
        pigtimeSystem.IncreasePigValue(30.0f);
        scoreSystem.IncreaseScore(500);
    }

    public bool CheckPigTime()
    {
        return pigtimeSystem.GetSetPigTime;
    }
}
