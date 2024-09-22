using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float thrustForce = 100f;
    public float rotationSpeed = 120f;
    public static int SCORE = 0;
    public GameObject panel, gun;
    public static bool game_paused = false;
    private float xBorderLimit = 9.5f;
    private float yBorderLimit = 5.5f;
    public Vector3 thrustDirection;
    private Rigidbody _rigidbody;
    void Start()
    {
        // rigidbody nos permite aplicar fuerzas en el jugador
        _rigidbody = GetComponent<Rigidbody>();

        game_paused = false;
        yBorderLimit = Camera.main.orthographicSize+1;
        xBorderLimit = (Camera.main.orthographicSize+1) * Screen.width / Screen.height;
    }
    void Update(){
        var newPos = transform.position;
        if (newPos.x > xBorderLimit)
            newPos.x = -xBorderLimit+1;
        else if (newPos.x < -xBorderLimit)
            newPos.x = xBorderLimit - 1;
        else if (newPos.y > yBorderLimit)
            newPos.x = -yBorderLimit+1;
        else if (newPos.y < -yBorderLimit)
            newPos.y = yBorderLimit - 1;
        transform.position = newPos;
        if (!game_paused){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GameObject bullet = ObjectPool.SharedInstance.GetPooledObject(); 
                if (bullet != null) {
                    bullet.transform.position = gun.transform.position;
                    bullet.transform.rotation = Quaternion.identity;
                    bullet.SetActive(true);
                }
                Bullet balaScript = bullet.GetComponent<Bullet>();

                balaScript.targetVector = transform.right;
            } 
        }
    }
    private void FixedUpdate()
    {  
        // obtenemos las pulsaciones de teclado
        float rotation = Input.GetAxis("Rotate") * Time.deltaTime;
        float thrust = Input.GetAxis("Thrust") * Time.deltaTime;
        // la dirección de empuje por defecto es .right (el eje X positivo)
        thrustDirection = transform.right;
        // rotamos con el eje "Rotate" negativo para que la dirección sea correcta
        transform.Rotate(Vector3.forward, -rotation * rotationSpeed);
        // añadimos la fuerza capturada arriba a la nave del jugador
        _rigidbody.AddForce(thrust * thrustDirection * thrustForce);        
    }
    public void pause() {
        Time.timeScale=0;
        game_paused = true;
        panel.SetActive(true);
    }
    public void resume(){
        Time.timeScale = 1;
        game_paused = false;
        panel.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Enemy"){
            SCORE = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else{
            Debug.Log("He colisionado con otra cosa");
        }
    }
}
