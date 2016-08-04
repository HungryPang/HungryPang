using UnityEngine;
using System.Collections;
using QuickPool;

public class csEffectScript : MonoBehaviour {

    public float EffectAnimationTime;
    public float FrameSpeed = 1.0f;
    public string strEffectImgName;
    public bool EffectRandomRot = false;

    int FrameCnt;
    Sprite[] SpriteArray;
    float fEffectAccTime;


    // Use this for initialization
    void Awake()
    {
        SpriteArray = Resources.LoadAll<Sprite>("01.InGame/" + strEffectImgName.ToString()) as Sprite[];
        FrameCnt = SpriteArray.Length;
    }
    void Start () {
        if(true == EffectRandomRot)
        {
            this.transform.Rotate(new Vector3(0.0f, 0.0f, Random.Range(0, 360)));
        }
    }
	
	// Update is called once per frame
	void Update () {

        fEffectAccTime += Time.deltaTime;
        int nFrame = (int)((fEffectAccTime * FrameSpeed) / EffectAnimationTime);
        //print(nFrame);

        if(nFrame >= FrameCnt)
        {
            fEffectAccTime = 0;
            this.gameObject.Despawn();
        }
        else
            GetComponent<SpriteRenderer>().sprite = SpriteArray[nFrame];
    }
}
