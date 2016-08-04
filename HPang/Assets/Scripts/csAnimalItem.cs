using UnityEngine;
using System.Collections;
using AnimalSystem;

public class csAnimalItem : MonoBehaviour {

    public Animal MyAnimal  = null;
    public Sprite MyImage   = null;

    public csGameSystem gameMgr         = null;
    public csSelectedManger selectMgr   = null;

    public bool bClick = false;
    // Use this for initialization
    void Start () {
        //gameMgr   = GetComponent<csGameSystem>();
        //selectMgr = GetComponent<csSelectedManger>();
        //print(selectMgr);
    }

    // Update is called once per frame
    void Update () {

        // AnimalItem 클릭시 행동 ( selectMgr 에 클릭한 동물 정보 넘김 )
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);


            //print(this.transform);
            //print(hit.collider.transform);
            bClick = false;
            if (hit.collider != null && hit.collider.transform == this.transform)
            {
                selectMgr.SetupAnimal(this);
                bClick = true;
            }
        }
	}

    public void SettingMyAnimalData(Animal animal, Sprite sprite)
    {
        MyAnimal    = animal;
        MyImage     = sprite;
        GetComponent<SpriteRenderer>().sprite = MyImage;
    }
}
