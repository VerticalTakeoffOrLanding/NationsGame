using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] private float baseMoveSpeed = 100;
    private float moveSpeed;
    [SerializeField] private float scrollSpeed = 1300;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = baseMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        moveSpeed = baseMoveSpeed + (transform.position.z * -0.5f);
    } //Zooming out makes the camera move faster

    private void Move()
    {
        if (Input.GetAxisRaw("Vertical") > 0) // D
        {
            transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0), Space.World);
            
        }
        else if (Input.GetAxisRaw("Vertical") < 0) // A
        {
            transform.Translate(new Vector3(0, -moveSpeed * Time.deltaTime, 0), Space.World);
        }
        if (Input.GetAxisRaw("Horizontal") > 0) // W
        {
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0, 0), Space.World);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0) // S
        {
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0, 0), Space.World);
        }

        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) // Scroll Up
        {
            transform.Translate(new Vector3(0, 0, scrollSpeed * Time.deltaTime));
        }
        else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0) // Scroll down
        {
            transform.Translate(new Vector3(0, 0, -scrollSpeed * Time.deltaTime));
        }
    } //Handles camera movement, WASD and scroll wheel


}
