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
    public int SetPosX { set { nMyPosX = value; } }
    public int SetPosY { set { nMyPosY = value; } }

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
    }
	
	// Update is called once per frame
	void Update () {
        // 도화선 폭팔
        if (true == bChaining)
        {
            Efx.Spawn(this.transform.position, Quaternion.identity);
            foodSystem.ChangeFoodItemInStorage(this);
            foodSystem.GatherScore();
            bChaining = false;
        }

        // 클릭 알고리즘
        if (Input.GetMouseButtonDown(0) && PickingTrue())
        {
            if(selectSystem.EatCheckFood(this) || foodSystem.CheckPigTime())
            {
                // 도화선 검사
                foodSystem.ExplosionNearFoodItem(nMyPosY, nMyPosX, myFoodData.eType);
            }
        }
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
}
