using UnityEngine;
using System.Collections;

public class csMouseSystem : MonoBehaviour {

    Camera  mCamera = null;
    Vector2 vMouseWPos;

    public bool ProgressClick = false;      // 눌르고있는중
    public bool FirstClick = false;         // 첫번째 눌림

  

    public Vector2 GetMouseWorldPos { get { return vMouseWPos; } }

    // Use this for initialization
    void Start () {
        mCamera = GetComponentInParent<Camera>();

        //Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        // 스크린 좌표 저장하기
        vMouseWPos = mCamera.ScreenToWorldPoint(Input.mousePosition);
        transform.position = vMouseWPos;

        if(Input.GetMouseButtonDown(0))
        {
            FirstClick = true;
        }
        else
        {
            FirstClick = false;
        }
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        if (Input.GetMouseButton(0))
        {
            ProgressClick = true;
        }
        else
        {
            ProgressClick = false;
        }

        //if (FirstClick)
        //    print("TRUE");

        
        collider.enabled = ProgressClick;
        //print(ProgressClick);
    }
}
