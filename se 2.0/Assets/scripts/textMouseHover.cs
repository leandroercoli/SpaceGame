using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class textMouseHover : MonoBehaviour {
	public bool isStart;
	public bool isQuit;
	public bool isBack;

	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Color.white;
	}
	
	void OnMouseEnter(){
		GetComponent<Renderer>().material.color = Color.red;
	}
	void OnMouseExit(){
		GetComponent<Renderer>().material.color = Color.white;
	}
	
	void OnMouseUp(){
		if(isStart)
		{
			SceneManager.LoadScene("space-scene", LoadSceneMode.Single);
		}
		if (isQuit)
		{
			Application.Quit();
		}
		if(isBack)
		{
			SceneManager.LoadScene("mainmenu-scene", LoadSceneMode.Single);
		}
	} 
}
