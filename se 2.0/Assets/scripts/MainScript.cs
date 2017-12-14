using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainScript : MonoBehaviour {
	public Transform NavePrefab; //Para instanciar la nave
	public Transform PickupsPrefab; //Para instanciar los aros
	public Transform EnemyPrefab; //Para instanciar los enemigos
	
	//Referencia a la nave y a su script, para manejarla
    private GameObject nave;
	private NaveScript naveScript; 
	
	//Referencia a la camara y a su script, para manejarla
    private GameObject camara;
	private CameraScript camaraScript; 
	
	//Información en pantalla
	private TextMesh killedText;
	private TextMesh collectedText;
	private TextMesh timeText;
	
	public float tiempoMax = 120f;
	
	//Para delimitar el escenario
	public int maxX = 5;
	public int maxY = 3;

	//Cada z unidades generar un pickup o enemigo
	public float zGeneradorPickups = 30f;
	public float zGeneradorEnemy = 50f;
	public float unidadTiempoGeneradorPickups = 5f; //Cada cuanto tiempo genera 
	public float unidadTiempoGeneradorEnemy = 10f; //Cada cuanto tiempo genera 
	private int ultimoTiempoGeneradoPickups = -1;
	private int ultimoTiempoGeneradoEnemy = -1;
	public static System.Random ran = new System.Random();

	// Use this for initialization
	void Start () {
		crearEscenario();		
	}	

	// Update is called once per frame
	void Update () {
			if(tiempoMax - Time.time < 0)
				SceneManager.LoadScene("perdiste-scene", LoadSceneMode.Single);
				
					//Movimiento con las teclas
					if (Input.GetKey(KeyCode.DownArrow)){
						naveScript.rotarAbajo();
						}        
					if (Input.GetKey(KeyCode.UpArrow)){
						naveScript.rotarArriba();
						}
					if (Input.GetKey(KeyCode.RightArrow)){
						  naveScript.rotarDerecha();
					}        
					if (Input.GetKey(KeyCode.LeftArrow)){
						naveScript.rotarIzquierda();
					}			      
					if (Input.GetKeyDown(KeyCode.Space)){ //Disparo
						 naveScript.disparar();
					 }
					 
					 
					collectedText = GameObject.Find("/MainCamera/Plane/Text/collectedText").GetComponent<TextMesh>();
					int aros = naveScript.getCollected();
					collectedText.text = "Aros: " + aros;
					killedText = GameObject.Find("/MainCamera/Plane/Text/killedText").GetComponent<TextMesh>();
					int eliminados = naveScript.getKilled();
					killedText.text = "Eliminados: " + eliminados;
					timeText = GameObject.Find("/MainCamera/Plane/Text/timeText").GetComponent<TextMesh>();
					int tiempoRestante = (int) (tiempoMax - Time.time);
					timeText.text = "Tiempo: " + tiempoRestante;
					
					if(eliminados > 5 && aros > 10 )
						SceneManager.LoadScene("ganaste-scene", LoadSceneMode.Single);	

						//Posición actual de la nave en z
						float zActual = nave.transform.position.z;

						//Cada unidadTiempoGenerador se generan pickups cada z especificado
						if((Time.time % unidadTiempoGeneradorPickups < 1) && (int)Time.time != ultimoTiempoGeneradoPickups){
							ultimoTiempoGeneradoPickups = (int)Time.time;
							int xPickup = ran.Next(-maxX+1, maxX-1); 
							int yPickup = ran.Next(-maxY+1, maxY-1); 
							Instantiate(PickupsPrefab, new Vector3(xPickup, yPickup, zActual+zGeneradorPickups), Quaternion.identity);
						}
						//Cada unidadTiempoGenerador se generan enemigos cada z especificado
						if((Time.time % unidadTiempoGeneradorEnemy < 1) && (int)Time.time != ultimoTiempoGeneradoEnemy){
							ultimoTiempoGeneradoEnemy = (int)Time.time;
							int xEnemy = ran.Next(-maxX+1, maxX-1); 
							int yEnemy = ran.Next(-maxY+1, maxY-1); 
							Instantiate (EnemyPrefab, new Vector3(xEnemy, yEnemy, zActual+zGeneradorEnemy), Quaternion.Euler(new Vector3(0, 180, 0)));
						}
			
	}
	
	void crearEscenario(){		
		//Instanciar la nave, los pickups y los enemigos
		Instantiate(NavePrefab, transform.position, transform.rotation);				
					
				
				//Obtener la camara
				camara = GameObject.FindWithTag("MainCamera");
				camaraScript = camara.GetComponent<CameraScript>(); 
				
				//Obtener la nave
				nave = GameObject.FindWithTag("Nave");
				naveScript = nave.GetComponent<NaveScript>(); 
				
				//Asociar la camara a la nave
				camaraScript.target = nave.transform;
				
				//Activar texto en pantalla
				GameObject.Find("/MainCamera/Plane/Text/collectedText").SetActive(true);
				GameObject.Find("/MainCamera/Plane/Text/killedText").SetActive(true);
				GameObject.Find("/MainCamera/Plane/Text/timeText").SetActive(true);
				collectedText = GameObject.Find("/MainCamera/Plane/Text/collectedText").GetComponent<TextMesh>();
				killedText = GameObject.Find("/MainCamera/Plane/Text/killedText").GetComponent<TextMesh>();
				timeText = GameObject.Find("/MainCamera/Plane/Text/timeText").GetComponent<TextMesh>();
	}
}

