using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody _rigid;
    public GameObject gun, bulletPrefab;

    public float thrustForce = 10f ;
    public float rotationSpeed = 12f;
    public static int SCORE = 0;
    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 thrustDirection = transform.right;


        float rotation = Input.GetAxis("Horizontal") * Time.deltaTime;
        float thrust = Input.GetAxis("Vertical") * Time.deltaTime;


        _rigid.AddForce(thrust*thrustDirection * thrustForce);
        transform.Rotate(Vector3.forward,-rotation*rotationSpeed);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
        
            Ammo balaScript = bullet.GetComponent<Ammo>();

            balaScript.targetVector = transform.right;
        
        }
    }
}
