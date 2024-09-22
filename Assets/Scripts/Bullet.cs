using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15f;
    public float maxLifeTime = 3f;
    public Vector3 targetVector;
    void Start()
    {
    // nada más nacer, le damos unos segundos de vida, 
    // lo suficiente para salir de la pantalla
        gameObject.SetActive(false);
    }
    void Update()
    {
    // la bala se mueve en la dirección del jugador al disparar
        transform.Translate(targetVector * speed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision){
        if (collision.gameObject.tag == "Enemy"){
            IncreaseScore();
            Destroy(collision.gameObject);
            gameObject.SetActive(false);
        }
    }
    public void IncreaseScore()
    {
        // cuando un asteroide es destruido, llama a esta función para darnos puntos.
        Player.SCORE++;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        // llamamos a esta función cada vez que ganamos puntos para actualizar el marcador
        GameObject go = GameObject.FindGameObjectWithTag("UI");
        go.GetComponent<Text>().text = "Points : " + Player.SCORE;
    }

}

