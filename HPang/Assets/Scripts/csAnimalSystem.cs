using UnityEngine;
using System.Collections;
using FoodInfo;

namespace AnimalSystem
{
    public enum ANIMALTYPE
    {
        eAnimalNone = -1,
        eAnimalMouse,
        eAnimalCow,
        eAnimalDragon,
        eAnimalTiger,
        eAnimalSnake,
        eAnimalHorse,
        eAnimalSheep,
        eAnimalMonkey,
        eAnimalChicken,
        eAnimalWolf,
        eAnimalTypesNum
    }
    public enum EATTYPE
    {
        eEatNone = -1,
        eEatExcellent,
        eEatGreat,
        eEatGood,
        eEatTypeNum
    }

    public class Animal
    {
        public FoodInfo.FOODTYPE[] ArrCanEatFood = new FoodInfo.FOODTYPE[(int)EATTYPE.eEatTypeNum];
        public ANIMALTYPE eAnimalType = ANIMALTYPE.eAnimalNone;
        public ANIMALTYPE eFriendAnimalType = ANIMALTYPE.eAnimalNone;

        public Animal(ANIMALTYPE eType, FoodInfo.FOODTYPE alfa, FoodInfo.FOODTYPE bravo, FoodInfo.FOODTYPE charlie)
        {
            eAnimalType = eType;

            ArrCanEatFood[(int)EATTYPE.eEatExcellent]   = alfa;
            ArrCanEatFood[(int)EATTYPE.eEatGreat]       = bravo;
            ArrCanEatFood[(int)EATTYPE.eEatGood]        = charlie;
        }

        public int EatableFood(FoodInfo.FOODTYPE testType)
        {
            int nResult = 3;   // 2 - Excellent, 1 - Great, 0 - Good, -1 - Can'tEat
            foreach (FoodInfo.FOODTYPE myEatType in ArrCanEatFood)
            {
                if (myEatType == testType){
                    return nResult;
                }
                --nResult;
            }
            return nResult;
        }
    }

    public class AnimalManager
    {
        static AnimalManager pointer = null;
        Animal[] tAnimalList = null;

        public Animal[] GetAnimalList{ get { return tAnimalList; }}

        public static AnimalManager getInstance()
        {
            if (null == pointer){
                pointer = new AnimalManager();
                pointer.SettingData();
            }
            return pointer;
        }

        public void SettingData()
        {
            tAnimalList = new Animal[(int)ANIMALTYPE.eAnimalTypesNum];
            // 쥐
            tAnimalList[(int)ANIMALTYPE.eAnimalMouse] = new Animal(
                    ANIMALTYPE.eAnimalMouse,
                    FoodInfo.FOODTYPE.eFoodCheese,
                    FoodInfo.FOODTYPE.eFoodHay,
                    FoodInfo.FOODTYPE.eFoodChicken
                );
            // 소
            tAnimalList[(int)ANIMALTYPE.eAnimalCow] = new Animal(
                    ANIMALTYPE.eAnimalCow,
                    FoodInfo.FOODTYPE.eFoodGrass,
                    FoodInfo.FOODTYPE.eFoodHay,
                    FoodInfo.FOODTYPE.eFoodCow
                );
            // 호랑이
            tAnimalList[(int)ANIMALTYPE.eAnimalTiger] = new Animal(
                    ANIMALTYPE.eAnimalTiger,
                    FoodInfo.FOODTYPE.eFoodCow,
                    FoodInfo.FOODTYPE.eFoodChicken,
                    FoodInfo.FOODTYPE.eFoodLeaf
                );
            // 용
            tAnimalList[(int)ANIMALTYPE.eAnimalDragon] = new Animal(
                    ANIMALTYPE.eAnimalDragon,
                    FoodInfo.FOODTYPE.eFoodFire,
                    FoodInfo.FOODTYPE.eFoodNone,
                    FoodInfo.FOODTYPE.eFoodNone
                );
            // 뱀
            tAnimalList[(int)ANIMALTYPE.eAnimalSnake] = new Animal(
                    ANIMALTYPE.eAnimalSnake,
                    FoodInfo.FOODTYPE.eFoodMouse,
                    FoodInfo.FOODTYPE.eFoodEgg,
                    FoodInfo.FOODTYPE.eFoodGrass
                );
            // 말
            tAnimalList[(int)ANIMALTYPE.eAnimalHorse] = new Animal(
                    ANIMALTYPE.eAnimalHorse,
                    FoodInfo.FOODTYPE.eFoodCarrot,
                    FoodInfo.FOODTYPE.eFoodCorn,
                    FoodInfo.FOODTYPE.eFoodHorse
                );
            // 양
            tAnimalList[(int)ANIMALTYPE.eAnimalSheep] = new Animal(
                    ANIMALTYPE.eAnimalSheep,
                    FoodInfo.FOODTYPE.eFoodPaper,
                    FoodInfo.FOODTYPE.eFoodGrass,
                    FoodInfo.FOODTYPE.eFoodSheep
                );
            // 원숭이
            tAnimalList[(int)ANIMALTYPE.eAnimalMonkey] = new Animal(
                    ANIMALTYPE.eAnimalMonkey,
                    FoodInfo.FOODTYPE.eFoodBanana,
                    FoodInfo.FOODTYPE.eFoodCookedRice,
                    FoodInfo.FOODTYPE.eFoodGrass
                );
            // 닭
            tAnimalList[(int)ANIMALTYPE.eAnimalChicken] = new Animal(
                    ANIMALTYPE.eAnimalChicken,
                    FoodInfo.FOODTYPE.eFoodLarva,
                    FoodInfo.FOODTYPE.eFoodRice,
                    FoodInfo.FOODTYPE.eFoodEgg
                );
            // 늑대
            tAnimalList[(int)ANIMALTYPE.eAnimalWolf] = new Animal(
                    ANIMALTYPE.eAnimalWolf,
                    FoodInfo.FOODTYPE.eFoodSheep,
                    FoodInfo.FOODTYPE.eFoodMouse,
                    FoodInfo.FOODTYPE.eFoodFeed
                );
        }
    }
}


public class csAnimalSystem : MonoBehaviour {

    public csFoodSystem     foodSystem  = null;
    public csResourceMgr    resourceMgr = null;

    public csAnimalItem[] AnimalDeckList = new csAnimalItem[4];
    public GameObject objClickUI = null;

	// Use this for initialization
    void Awake()
    {

 
    }
	void Start () {
        //test.GetComponent<csFoodSystem>().
        int[] animalNumHash = new int[4];
        animalNumHash[0] = animalNumHash[1] = animalNumHash[2] = animalNumHash[3] = -1;
        int nAnimalHashIndex = 0;
        int nFoodIndex = 0;

        //오른쪽이 플레이어가 고른 동물 넘버
        animalNumHash[0] = 0;
        animalNumHash[1] = 1;
        animalNumHash[2] = 2;
        animalNumHash[3] = 3;

        AnimalSystem.Animal[] AnimalList = AnimalSystem.AnimalManager.getInstance().GetAnimalList;

        foreach (csAnimalItem Deckitem in AnimalDeckList)
        {
            //print(AnimalDeckList);
            //print(Deckitem);

            AnimalSystem.Animal comAnimal = AnimalList[animalNumHash[nAnimalHashIndex]];
            foreach (FoodInfo.FOODTYPE types in comAnimal.ArrCanEatFood)
            {
                if (FoodInfo.FOODTYPE.eFoodNone != types)
                {
                    foodSystem.ComposeFoodBoxList(nFoodIndex, types);
                    ++nFoodIndex;
                }
            }
            Deckitem.SettingMyAnimalData(comAnimal, resourceMgr.GetAnimalSpriteArray[animalNumHash[nAnimalHashIndex++]]);
        }

        foodSystem.nFoodtypeNum = nFoodIndex; // 푸드종류 총갯수
        //(nFoodIndex);
    }
	
	// Update is called once per frame
	void Update () {

        foreach (csAnimalItem Deckitem in AnimalDeckList)
        {
            if(Deckitem.bClick == true)
            {
                objClickUI.GetComponent<SpriteRenderer>().enabled = true;
                objClickUI.transform.position = Deckitem.transform.position;
            }
        }
    }
}
