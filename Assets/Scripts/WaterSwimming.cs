using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSwimming : MonoBehaviour//skrypt odpowiedzialny za płynięcie wody
{
    public float speedX = 0.1f;
    public float speedY = 0.1f;
    private float curX;
    private float curY;
    Renderer renderer;
    void Start()
    {
        renderer=GetComponent<Renderer>();
        curX=renderer.material.mainTextureOffset.x;
        curY=renderer.material.mainTextureOffset.y;
    }
    
    //tekstura wody przemieszcza się po obiekcie gry, co możę sprawiac wrażenie jakby woda płynęła
    void Update()
    {
        curX+=Time.deltaTime*speedX;
        curY+=Time.deltaTime*speedY;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(curX,curY));
    }
}
