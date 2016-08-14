using UnityEngine;
using System.Collections;
using QuickPool;
public class csFoodItem : MonoBehaviour {

    public csGameSystem gameSystem = null;
    public csSelectedManger selectSystem = null;
    public csFoodSystem foodSystem = null;
    FoodInfo.FoodData   myFoodData;

    public GameObject Efx;
    public bool bChaining = false;
    public int nMyPosX;
    public int nMyPosY;

    public bool bPangForComboCheck = false;
    public bool bClick = false;
    public bool bChange = false;
    float fChangeTime = 0.0f;
    int nClickFoodStat;

    public int nDist = -1;
    float fExploreTime = 0.0f;
    float fDelayTime = 0.05f;

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
    }
	
	// Update is called once per frame
	void Update () {
        bClick = false;

        if(true == bChaining)
        {
            fExploreTime += Time.deltaTime;
            if(fExploreTime > fDelayTime * nDist)
            {
                foodSystem.ChangeFoodItemInStorage(this);
                foodSystem.GatherScore(nClickFoodStat);
                Efx.Spawn(this.transform.position, Quaternion.identity);
                bChaining = false;
                nDist = -1;
                fExploreTime = 0.0f;
            }
        }

        DelayRenderFoodImg();
        nClickFoodStat = ClickAlgorithm();
    }

    // Setting FoodData
    public void SettingFoodData(Sprite sprite, FoodInfo.FoodData data)
    {
        GetComponent<SpriteRenderer>().sprite = sprite;
        myFoodData = data;
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

    void DelayRenderFoodImg()
    {
        if (bChange)
        {
            fChangeTime += Time.deltaTime;
            if (fChangeTime > 0.2f)
            {
                this.GetComponent<SpriteRenderer>().enabled = true;
                bChange = false;
                fChangeTime = 0.0f;
            }
        }
    }

    int ClickAlgorithm()
    {
        // 클릭 알고리즘
        if (Input.GetMouseButtonDown(0) && PickingTrue())
        {
            bClick = true;
            int EatCheckResult = selectSystem.EatCheckFood(this);
            if (EatCheckResult != 0 || foodSystem.CheckPigTime())
            {
                // 도화선 검사
                foodSystem.ExplosionNearFoodItem(nMyPosY, nMyPosX, myFoodData.eType, 0);
                foodSystem.RenderEatFoodFeedBack(EatCheckResult);

                if (foodSystem.CheckPigTime())
                    foodSystem.RenderEatFoodFeedBack(3);
            }
            return EatCheckResult;
        }
        return -100;
    }
}
