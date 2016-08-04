using UnityEngine;
using System.Collections;

public class csSelectedManger : MonoBehaviour {

    public csRenderImage SelectedAnimalImg      = null;
    public csRenderImage SelectedAnimalBackGround = null;
    public csRenderImage SelectedAnimalFoodImgA = null;
    public csRenderImage SelectedAnimalFoodImgB = null;
    public csRenderImage SelectedAnimalFoodImgC = null;

    public csAnimalItem SelectedAnimalItem = null;
    //AnimalSystem.Animal SelectedAnimal =

    csRenderImage[] FoodArray = new csRenderImage[3];
    Sprite[] animalSpriteImg = null;

    public float fAccTime = 0;

    public csResourceMgr resourceMgr = null;

    // Use this for initialization
    void Start () {
        FoodArray[0] = SelectedAnimalFoodImgA;
        FoodArray[1] = SelectedAnimalFoodImgB;
        FoodArray[2] = SelectedAnimalFoodImgC;
        
        for(int i= 0; i < 3; ++i)
        {
            FoodArray[i].transform.localScale *= 0.7f;
        }

        SelectedAnimalImg.transform.localScale *= 1.3f;
    }
	
	// Update is called once per frame
	void Update () {

        //동물 애니메이션 리소스매니저에서 리스트<구조체> 로 담아서 구조체의 spirte배열을 시간단위로 돌림 정리 필요
        fAccTime += Time.deltaTime;
        if(animalSpriteImg != null)
        {
            int FrameCnt = (int)((fAccTime * 2.0f) / 1.0f);
            if(FrameCnt >= 2.0f)
            {
                fAccTime = 0.0f;
            }
            else
            {
                // 이미지없어서 예외처리 대충
                if ((int)SelectedAnimalItem.MyAnimal.eAnimalType == 0)
                    SelectedAnimalImg.SetImage(resourceMgr.SpriteImgList[(int)SelectedAnimalItem.MyAnimal.eAnimalType].sprite[FrameCnt]);
                //else
                //    SelectedAnimalImg.SetImage(resourceMgr.GetAnimalFrontSpriteArray[]);
            }
                
        }
    }
    
    // 동물 선택시 등록
    public void SetupAnimal(csAnimalItem item)
    {
        SelectedAnimalItem = item;
        AnimalSystem.Animal itemAnimal = SelectedAnimalItem.MyAnimal;

        animalSpriteImg = resourceMgr.SpriteImgList[(int)itemAnimal.eAnimalType].sprite;
        print((int)itemAnimal.eAnimalType);
        if((int)itemAnimal.eAnimalType == 0)    // 스프라이트 이미지가 없음
            SelectedAnimalImg.SetImage(resourceMgr.SpriteImgList[(int)itemAnimal.eAnimalType].sprite[0]);
        else
            SelectedAnimalImg.SetImage(resourceMgr.mTempFront[(int)itemAnimal.eAnimalType]);

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

    public int EatCheckFood(csFoodItem item)
    {
        if (SelectedAnimalItem == null) /*return false;*/ return 0;

        //bool bResult = false;
        int EatableFoodType = SelectedAnimalItem.MyAnimal.EatableFood(item.GetSetFoodData.eType);
        //if (EatableFoodType >= 0)
        //{
        //    bResult = true;
        //}
        return EatableFoodType;
    }
}
