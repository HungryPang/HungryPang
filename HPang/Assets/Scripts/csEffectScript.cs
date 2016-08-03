using UnityEngine;
using System.Collections;
using QuickPool;

public class csEffectScript : MonoBehaviour {

    public float EffectAnimationTime;
    public float FrameSpeed = 1.0f;
    public string strEffectImgName;

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

    }
	
	// Update is called once per frame
	void Update () {

        fEffectAccTime += Time.deltaTime;
        int nFrame = (int)((fEffectAccTime * FrameSpeed) / EffectAnimationTime);

        if(nFrame >= FrameCnt)
        {
            this.gameObject.Despawn();
        }
        else
            GetComponent<SpriteRenderer>().sprite = SpriteArray[nFrame];
    }
}
