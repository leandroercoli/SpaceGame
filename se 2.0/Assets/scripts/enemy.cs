using UnityEngine;
using System.Collections;

public class enemy : MonoBehaviour {
	public const float VELOCIDADROTACION = 15f;
	public const float VELOCIDADAVANCE = 7f;
	
	private float startTime;
	public const int TTL = 20;

	void Start(){
		startTime = Time.time;
	}

	void Update(){
		if(Time.time - startTime < TTL){		
		transform.Translate(Vector3.forward * VELOCIDADAVANCE*Time.deltaTime);
		
	/*	transform.localEulerAngles = 
									new Vector3(transform.localEulerAngles.x,
									transform.localEulerAngles.y + VELOCIDADROTACION *Time.deltaTime,
									transform.localEulerAngles.z);
		*/

		}
		else{
			//Destruir nave si ya paso su tiempo de vida
			Destroy(gameObject); 
			}
	}
}
