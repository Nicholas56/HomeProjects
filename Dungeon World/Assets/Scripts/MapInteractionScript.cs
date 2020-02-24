using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractionScript : MonoBehaviour
{
    Vector3 mousePos1;
    Vector3 mousePos2;
    Vector3 tileOffset = new Vector3(0, 0.25f, 0);

    bool addToSelected;

    public List<GameObject> selectableObjects;
    List<GameObject> selectedObjects;

    // Start is called before the first frame update
    void Awake()
    {
        selectableObjects = new List<GameObject>();
        selectedObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            for(int i = 0; i < selectedObjects.Count; i++)
            {
                Interact(selectedObjects[i].transform);
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            addToSelected = true;
        }
        else { addToSelected = false; }
        if (Input.GetButtonDown("Fire1"))
        {
            if (!addToSelected)
            {
                selectedObjects.Clear();
            }
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if(mousePos1 != mousePos2)
            {
                SelectObjects(true);
            }
        }
        if (Input.GetButton("Fire1"))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (mousePos1 != mousePos2)
            {
                SelectObjects(false);
            }
        }
    }

    void SelectObjects(bool isSelect)
    {
        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        for(int i = 0;i<selectableObjects.Count;i++)
        {
            if(selectableObjects != null)
            {
                if (selectRect.Contains(Camera.main.WorldToViewportPoint(selectableObjects[i].transform.position + tileOffset), true))
                {
                    if (isSelect)
                    {
                        //What happens when a selection is made
                        selectedObjects.Add(selectableObjects[i]);
                        selectableObjects[i].GetComponent<TileBehaviourScript>().SelectCheck(TileBehaviourScript.status.Selected);
                    }
                    else
                    {
                        //What happens as selection box is hovering
                        selectableObjects[i].GetComponent<TileBehaviourScript>().SelectCheck(TileBehaviourScript.status.Hovered);
                    }
                }
                else
                {
                    //What happens to tiles not in the selection box
                    if (!selectedObjects.Contains(selectableObjects[i]))
                    {
                        selectableObjects[i].GetComponent<TileBehaviourScript>().ResetState();
                    }
                    else { selectableObjects[i].GetComponent<TileBehaviourScript>().SelectCheck(TileBehaviourScript.status.Selected); }
                }
            }
        }
    }

    void Interact(Transform clickedObject)
    {
        if (clickedObject.GetComponent<TileBehaviourScript>())
        {
            clickedObject.GetComponent<TileBehaviourScript>().ReduceTile();
        }
    }
}
