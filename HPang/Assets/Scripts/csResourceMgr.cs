using UnityEngine;
using System.Collections;

public class csResourceMgr : MonoBehaviour {

    public string FoodSpritesName = null;
    public string AnimalSpritesName = null;
    public string BoomDustSpritesName = null;
    public string NumberSpritesName = null;

    Sprite[] mFoodSprites = null;
    Sprite[] mAnimalSprites = null;
    Sprite[] mBoomDustSprites = null;
    Sprite[] mNumberSprites = null;

    public Sprite[] GetFoodSpriteArray{ get { return mFoodSprites; } }
    public Sprite[] GetAnimalSpriteArray { get { return mAnimalSprites; } }
    public Sprite[] GetmBoomDustSpriteArray { get { return mBoomDustSprites; } }
    public Sprite[] GetmNumberSpriteArray { get { return mNumberSprites; } }

    // Use this for initialization
    void Awake()
    {
        mFoodSprites = Resources.LoadAll<Sprite>("01.InGame/" + FoodSpritesName.ToString()) as Sprite[];
        mAnimalSprites = Resources.LoadAll<Sprite>("01.InGame/" + AnimalSpritesName.ToString()) as Sprite[];
        mBoomDustSprites = Resources.LoadAll<Sprite>("01.InGame/" + BoomDustSpritesName.ToString()) as Sprite[];
        mNumberSprites = Resources.LoadAll<Sprite>("02.UI/" + NumberSpritesName.ToString()) as Sprite[];
        //print(mNumberSprites.Length);
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
