using UnityEngine;

public class MovingDude : MonoBehaviour
{

    float speed = 5f; 
    GameObject dude;

    void Start() {
    }



    void Update() {

        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.RightArrow)){
            position.x += (speed * Time.deltaTime);
        } 

        if (Input.GetKey(KeyCode.LeftArrow)){
            position.x -= (speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.DownArrow)){
            position.z -= (speed * Time.deltaTime);

        }

        if (Input.GetKey(KeyCode.UpArrow)){
            position.z += (speed * Time.deltaTime);
        }

        transform.position = position;
    }
}