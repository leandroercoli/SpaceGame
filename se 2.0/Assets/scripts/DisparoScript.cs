using UnityEngine;
using System.Collections;

public class DisparoScript : MonoBehaviour {
	//Referencia a la nave y a su script
    private GameObject nave;
	private NaveScript naveScript; 
	
	// Use this for initialization
	void Start () {
		nave = GameObject.FindWithTag("Nave");
		naveScript = nave.GetComponent<NaveScript>(); 
	}
	
	// Update is called once per frame
	void Update () {
	
	}	
	
	void OnParticleCollision(GameObject other) {
		if(other.CompareTag("Enemy")){ 
            naveScript.killedOne(other);
        }

	}
}
