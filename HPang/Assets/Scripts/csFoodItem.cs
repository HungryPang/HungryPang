using UnityEngine;
using System.Collections;
using QuickPool;
public class csFoodItem : MonoBehaviour {

    public csGameSystem gameSystem = null;
    public csSelectedManger selectSystem = null;
    public csFoodSystem foodSystem = null;
    FoodInfo.FoodData   myFoodData;

    //public GameObject Efx;

    bool bEffectStart = false;
    float fEffectAccTime    = 0.0f;
    float fEffectAniSpd     = 0.35f;
    Sprite[] m_ArrEffectDust = null;
    //SpriteRenderer effectRender;

    public FoodInfo.FoodData GetSetFoodData
    {
        get { return myFoodData; }
        set { myFoodData = value; }
    }
    // Use this for initialization
    void Awake()
    {
        //effectRender = new SpriteRenderer();
    }
    void Start () {
        GetComponent<SpriteRenderer>().sortingOrder = 0;
        gameSystem = GetComponentInParent<csGameSystem>();
        selectSystem = gameSystem.selectMgr;
        foodSystem = gameSystem.foodSystem;

        m_ArrEffectDust = gameSystem.resourceMgr.GetmBoomDustSpriteArray;
        //print(m_ArrEffectDust.Length);
    }
	
	// Update is called once per frame
	void Update () {

        // 클릭 알고리즘
        if (Input.GetMouseButtonDown(0) && PickingTrue())
        {
            if(selectSystem.EatCheckFood(this) || foodSystem.CheckPigTime())
            {
                foodSystem.ChangeFoodItemInStorage(this);
                bEffectStart = true;
                foodSystem.GatherScore();
                //Efx.Spawn(this.transform.position, Quaternion.identity);
            }
        }
        //if(bEffectStart)
        //    StartEffect();
    }

    // Setting FoodData
    public void SettingFoodData(Sprite sprite, FoodInfo.FoodData data)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        myFoodData = data;
    }
    // Collider FoodItem
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "Mouse")
            print("FoodItem 충돌");
    }

    bool PickingTrue()
    {
        bool bResult = false;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null && hit.collider.transform == this.transform)
        {
            bResult = true;
        }
        return bResult;
    }

    //void StartEffect()
    //{
    //    fEffectAccTime += Time.deltaTime;
    //    float fFrame = m_ArrEffectDust.Length;
    //    int nFrame = (int)((fEffectAccTime * fFrame) / fEffectAniSpd);

    //    if (nFrame >= m_ArrEffectDust.Length)
    //    {
            
    //    }
    //    else
    //        effectRender.sprite = m_ArrEffectDust[nFrame];
    //}
}
