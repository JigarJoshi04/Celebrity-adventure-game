using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rightbutton : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer thisRenderer;
    public Sprite rightSprite;

    void Start()
    {
        Debug.Log("Right sprite is working....");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RightDrag()
    {
        Vector3 acc = Vector3.zero;
        acc.x = 0.1f;
        thisRenderer.sprite = rightSprite;
        Debug.Log("Right side movement is enabled.........");
    }
    public void justdebug()
    {
        Debug.Log("This is working:  ");
    }
}
