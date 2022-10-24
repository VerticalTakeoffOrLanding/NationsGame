using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    [SerializeField] private LayerMask WhatLayerCanBeClicked = 1 << 8;
    [SerializeField] private Texture2D cursorImage;

    [SerializeField] private GameObject tileSelectionText;
    private TileSelectionUIScript tileSelectionUIScript;

    // Start is called before the first frame update
    void Start()
    {
        setCursor();
        tileSelectionUIScript = tileSelectionText.GetComponent<TileSelectionUIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            Debug.DrawRay(ray.origin, ray.direction, Color.blue);

            // Casts the ray and get the first game object hit
            Physics.Raycast(ray, out hit);

            if (hit.transform.parent.CompareTag("TileTag"))
            {
                //Debug.Log("This hit at " + hit.transform.GetComponentInParent<TileScript>().GetTitle());
                tileSelectionUIScript.SelectTile(hit.transform.GetComponentInParent<TileScript>());
                // need to tell tile it has been selected
            } 
        }
    }

    public void setCursor()
    {
        Cursor.visible = true;
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    } //Changes the cursor to look different

}
