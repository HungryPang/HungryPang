using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ResourceNsp
{
    public struct strImage
    {
        public Sprite[] sprite;
        public int imageCnt;

        public strImage(Sprite[] spt, int cnt) { sprite = spt; imageCnt = cnt; }
    }
}
public class csResourceMgr : MonoBehaviour {

    public string FoodSpritesName = null;
    public string AnimalSpritesName = null;
    public string BoomDustSpritesName = null;
    public string NumberSpritesName = null;

    Sprite[] mFoodSprites = null;
    Sprite[] mAnimalSprites = null;
    Sprite[] mAniamlFrontSprites = null;
    //Sprite[Sprite[15]] mAnimalFrontAnimation = null;

    public List<ResourceNsp.strImage> SpriteImgList = new List<ResourceNsp.strImage>();

    Sprite[] mBoomDustSprites = null;
    Sprite[] mNumberSprites = null;
    Sprite[] mComboSprites = null;

    public Sprite[] mFeedBackSprites = null;

    public Sprite[] GetFoodSpriteArray{ get { return mFoodSprites; } }
    public Sprite[] GetAnimalSpriteArray { get { return mAnimalSprites; } }
    public Sprite[] GetmBoomDustSpriteArray { get { return mBoomDustSprites; } }
    public Sprite[] GetmNumberSpriteArray { get { return mNumberSprites; } }
    public Sprite[] GetmComboSpriteArray { get { return mComboSprites; } }
    public Sprite[] GetAnimalFrontSpriteArray { get { return mAniamlFrontSprites; } }

    // Use this for initialization
    void Awake()
    {
        mFoodSprites = Resources.LoadAll<Sprite>("01.InGame/" + FoodSpritesName.ToString()) as Sprite[];
        mAnimalSprites = new Sprite[4];
        mAniamlFrontSprites = new Sprite[4];
        mFeedBackSprites = new Sprite[4];
        for (int i = 0; i < 4; ++i)
        {
            mAnimalSprites[i] = Resources.Load<Sprite>("01.InGame/animal_" + i.ToString());
            mAniamlFrontSprites = Resources.LoadAll<Sprite>("01.InGame/animal_Front_Sprite_" + i.ToString());

            SpriteImgList.Add(new ResourceNsp.strImage(mAniamlFrontSprites, mAniamlFrontSprites.Length));

            mFeedBackSprites[i] = Resources.Load<Sprite>("02.UI/UI_FeedBack_" + i.ToString());
        }

        mBoomDustSprites = Resources.LoadAll<Sprite>("01.InGame/" + BoomDustSpritesName.ToString()) as Sprite[];
        mNumberSprites = Resources.LoadAll<Sprite>("02.UI/" + NumberSpritesName.ToString()) as Sprite[];
        mComboSprites = Resources.LoadAll<Sprite>("02.UI/" + "UI_Combo") as Sprite[];
        //print(mNumberSprites.Length);
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
