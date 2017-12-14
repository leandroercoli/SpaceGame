using UnityEngine;
using System.Collections;
 using System.Collections.Generic;

public class NaveScript : MonoBehaviour {
	private const float MAXROT = 3.0f; //Max Rotacion en estado activo 
	private bool izquierda; //Para la animacion en estado idle
	
	public const float VELOCIDADROTACION = 30f;
	public const float VELOCIDADAVANCE = 10f;
	
	private int collected = 0; //Set up a variable to store how many you've collected
	private int killed = 0; //Set up a variable to store how many you've killed
	
	public Transform DisparoPrefab;
		
	private List<GameObject> eliminadas = new List<GameObject>();

	
	void Start(){
		izquierda = true;
	}
	
	// Update is called once per frame
	void Update () {		
		transform.Translate(Vector3.forward * VELOCIDADAVANCE*Time.deltaTime);
        //transform.Translate(Vector3.up * Time.deltaTime, Space.World);
		//transform.Rotate(Vector3.right * Time.deltaTime);
			
			float angle = transform.localEulerAngles.z;
			angle = (angle > 180) ? angle - 360 : angle;
				
			if(izquierda){
				if(angle <= MAXROT){
					//Rota hacia la izquierda
					transform.localEulerAngles = 
									new Vector3(transform.localEulerAngles.x,
									transform.localEulerAngles.y,
									transform.localEulerAngles.z + 2*Time.deltaTime);
				}else izquierda = false;				
			}
			else{
				if(angle >= -MAXROT){
					//Rota hacia la derecha
					transform.localEulerAngles = 
								new Vector3(transform.localEulerAngles.x,
									transform.localEulerAngles.y,
									transform.localEulerAngles.z- 2*Time.deltaTime);
				}else izquierda = true;						
			}
	
				
	}
	
	public void rotarAbajo(){
		transform.localEulerAngles = 
									new Vector3(transform.localEulerAngles.x + VELOCIDADROTACION * Time.deltaTime,
									transform.localEulerAngles.y ,
									transform.localEulerAngles.z);
	}
	
	public void rotarArriba(){
		transform.localEulerAngles = 
									new Vector3(transform.localEulerAngles.x - VELOCIDADROTACION * Time.deltaTime,
									transform.localEulerAngles.y ,
									transform.localEulerAngles.z);
	}
	
	public void rotarDerecha(){
		transform.localEulerAngles = 
									new Vector3(transform.localEulerAngles.x,
									transform.localEulerAngles.y  + VELOCIDADROTACION * Time.deltaTime,
									transform.localEulerAngles.z);
	}
	
	public void rotarIzquierda(){
		transform.localEulerAngles = 
									new Vector3(transform.localEulerAngles.x,
									transform.localEulerAngles.y - VELOCIDADROTACION * Time.deltaTime,
									transform.localEulerAngles.z);
	}
	
	public void disparar(){
		Instantiate(DisparoPrefab, transform.position, transform.rotation);
	}
	
    void OnTriggerEnter(Collider other){ //Checks to see if you've collided with another object
        if(other.CompareTag("PickUp")){ //checks to see if this object is tagged with "collectable"
            //audio.PlayOneShot(collectedSound); //plays the sound assigned to collectedSound
            collected++; //adds a count of +1 to the collected variable
            Destroy(other.gameObject); //destroy's the collectable
        }
	}
	
	void OnCollisionEnter(Collision collision)
    {
		if(collision.collider.CompareTag("Enemy")){
			Application.LoadLevel("mainmenu-scene");
		}
    }
	
	public void killedOne(GameObject other){
		if(!eliminadas.Contains(other)){
			killed++;	
			eliminadas.Add(other);
			Destroy(other.gameObject);
		}
}

	public int getCollected(){
		return collected;
	}
	
	public int getKilled(){
		return killed;
	}
	
}
