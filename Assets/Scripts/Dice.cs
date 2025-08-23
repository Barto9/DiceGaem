using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public Sprite[] diceFaces;
    public SpriteRenderer sprite;
    public int currentValue;    // Stores the current dice value
    public bool locked = false;
    private Vector3 offset;
    [Header("Bounds")]
    public BoxCollider2D matCollider;
    public Bounds matBounds;

    private bool dragging = false;

    void Start()
    {
        if(matCollider != null){
            matBounds = matCollider.bounds;
        }
    }
    
   void Update()
    {
        if (dragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Vector3 newPos = mousePos + offset;

            Bounds bounds = matCollider != null ? matCollider.bounds : matBounds;
            Vector3 ext = sprite != null ? sprite.bounds.extents : Vector3.zero;
            newPos.x = Mathf.Clamp(newPos.x, bounds.min.x + ext.x, bounds.max.x - ext.x);
            newPos.y = Mathf.Clamp(newPos.y, bounds.min.y + ext.y, bounds.max.y - ext.y);

            transform.position = newPos;
        }

        //locking dice
        if (Input.GetMouseButtonDown(1)) // Right mouse button
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform) // Check if THIS object was clicked
                {
                    if (locked)
                        Unlock();
                    else
                        Lock();
                }
            }
        }
    }

    void OnMouseDown()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        offset = transform.position - mousePos;
        dragging = true;

    }
    void OnMouseUp()
    {
        dragging = false;
    }

    public void RollDice()
    {
        currentValue = Random.Range(1, 7);
        sprite.sprite = diceFaces[currentValue - 1];
    }
    public void Lock()
    { 
        locked = true;
        sprite.color = new Color(1, 1, 1, 0.5f);
    }
    public void Unlock()
    { 
        locked = false;
        sprite.color = new Color(1, 1, 1, 1);
    }


 
}
