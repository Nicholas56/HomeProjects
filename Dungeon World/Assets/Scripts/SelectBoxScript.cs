using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using tutorial : https://www.youtube.com/watch?v=vsdIhyLKgjc
public class SelectBoxScript : MonoBehaviour
{
    public RectTransform selectImage;
    Vector3 startPos;
    Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        selectImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            startPos = Input.mousePosition;            
        }
        if (Input.GetButtonUp("Fire1"))
        {
            selectImage.gameObject.SetActive(false);
        }
        if (Input.GetButton("Fire1"))
        {
            if (!selectImage.gameObject.activeInHierarchy)
            {
                selectImage.gameObject.SetActive(true);
            }

            endPos = Input.mousePosition;

            Vector3 squareStart = startPos;
            squareStart.z = 0f;
            Vector3 centre = (squareStart + endPos) / 2f;

            selectImage.position = centre;

            float sizeX = Mathf.Abs(squareStart.x - endPos.x);
            float sizeY = Mathf.Abs(squareStart.y - endPos.y);

            selectImage.sizeDelta = new Vector2(sizeX, sizeY);
        }
    }
}
