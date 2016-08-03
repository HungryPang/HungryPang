using UnityEngine;
using System.Collections;

public class csRenderImage : MonoBehaviour {

    Sprite RenderImage = null;

    // Use this for initialization
    void Awake()
    {
        RenderImage = GetComponent<SpriteRenderer>().sprite;
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetImage(Sprite sprite)
    {
        if (sprite)
            GetComponent<SpriteRenderer>().sprite = sprite;
        else
            GetComponent<SpriteRenderer>().sprite = RenderImage;
    }
}
