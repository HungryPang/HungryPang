using UnityEngine;
using System.Collections;

public class csSelectedManger : MonoBehaviour {

    public csRenderImage SelectedAnimalImg      = null;
    public csRenderImage SelectedAnimalFoodImgA = null;
    public csRenderImage SelectedAnimalFoodImgB = null;
    public csRenderImage SelectedAnimalFoodImgC = null;

    public csAnimalItem SelectedAnimalItem = null;
    //AnimalSystem.Animal SelectedAnimal =

    csRenderImage[] FoodArray = new csRenderImage[3];


    public csResourceMgr resourceMgr = null;

    // Use this for initialization
    void Start () {
        FoodArray[0] = SelectedAnimalFoodImgA;
        FoodArray[1] = SelectedAnimalFoodImgB;
        FoodArray[2] = SelectedAnimalFoodImgC;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    
    // 동물 선택시 등록
    public void SetupAnimal(csAnimalItem item)
    {
        SelectedAnimalItem = item;
        AnimalSystem.Animal itemAnimal = SelectedAnimalItem.MyAnimal;
        SelectedAnimalImg.SetImage(resourceMgr.GetAnimalSpriteArray[(int)itemAnimal.eAnimalType]);
        //print("B");
        int nCnt = 0;
        foreach (csRenderImage FoodIter in FoodArray)
        {
            if (FoodInfo.FOODTYPE.eFoodNone != itemAnimal.ArrCanEatFood[nCnt])
                FoodIter.SetImage(resourceMgr.GetFoodSpriteArray[(int)itemAnimal.ArrCanEatFood[nCnt]]);
            else
                FoodIter.SetImage(null);
            ++nCnt;
        }
    }

    public bool EatCheckFood(csFoodItem item)
    {
        if (SelectedAnimalItem == null) return false;

        bool bResult = false;
        int EatableFoodType = SelectedAnimalItem.MyAnimal.EatableFood(item.GetSetFoodData.eType);
        if (EatableFoodType >= 0)
        {
            bResult = true;
        }
        return bResult;
    }
}
