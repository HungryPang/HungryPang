using UnityEngine;
using System.Collections;

public class csFoodBox : MonoBehaviour {

    public GameObject FoodItem;
    public GameObject FoodSlot;

   // public GameObject test;

    public int nColSize = 0;        //가로 Column
    public int nRowSize = 0;        //세로 Row
    public Vector2 OffsetStartPos;

    public GameObject[,] FoodList = null;
    public GameObject[,] FoodSlotList = null;

    // Use this for initialization
    void Awake()
    {
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        Vector2 pos = transform.position;
        Vector2 size = Vector2.Scale(collider.size * 0.5f, transform.localScale);

        Vector2 offset = new Vector2(pos.x - size.x + OffsetStartPos.x, pos.y + size.y - OffsetStartPos.y);
        Vector2 pitch = new Vector2(size.x / nColSize, size.y / nRowSize) * 2;


        Vector2 temp = new Vector2();

        FoodList        = new GameObject[nColSize, nRowSize];
        FoodSlotList    = new GameObject[nColSize, nRowSize];

        for (int y = 0; y < nRowSize; ++y)
        {
            for (int x = 0; x < nColSize; ++x)
            {
                GameObject newFood      = Instantiate(FoodItem);
                GameObject newFoodSlot  = Instantiate(FoodSlot);

                if (y % 2 == 0)
                {
                    temp.x = 1.18f * x;
                    temp.y = 1.0f * y * -1;
                }
                else
                {
                    temp.x = 1.18f * x + 0.6f;
                    temp.y = 1.0f * y * -1;
                }
                newFood.transform.parent = transform;
                newFoodSlot.transform.parent = transform;

                newFood.transform.position = offset + temp;
                newFoodSlot.transform.position = offset + temp;


                if (y % 2 == 1 && x == nColSize - 1)
                {
                    newFood.SetActive(false);
                    newFoodSlot.SetActive(false);
                }
                FoodList[y, x] = newFood;
                FoodSlotList[y, x] = newFoodSlot;
            }
        }
        collider.enabled = false;
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
