using UnityEngine;
using System.Collections;

public class ButtonListener : MonoBehaviour {

	public GUIControl SGUI;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{

		//a simple listener for buttons - transfers the name of the object to guimanager.action
		SGUI.Action(this.gameObject.name);
	}
}
