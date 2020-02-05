using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_Scroller : MonoBehaviour {

    float scrollSpeed = 0.04f;
    private Vector2 offset;
    public Renderer rend;

    private GameController gameController;


    // Use this for initialization
    void Start () {


        GameObject gameControllerObj = GameObject.FindWithTag("GameController");

        if (gameControllerObj != null)
        {
            gameController = gameControllerObj.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script.");
        }


        rend = GetComponent<Renderer>();

        offset = rend.material.GetTextureOffset("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {
        scrollSpeed = gameController.getBGOffsetScrollSpeed();

        float x = Mathf.Repeat( scrollSpeed*Time.time, 1);
        Vector2 t_offset = new Vector2(x, offset.y);
        rend.material.SetTextureOffset("_MainTex", t_offset);
	}

    void onDisable()
    {
        rend.material.SetTextureOffset("_MainTex", offset);
    }
}
