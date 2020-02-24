using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileBehaviourScript : MonoBehaviour
{
    Transform tileWayPoint;
    Animator anim;

    Image tileImage;

    public enum status { Selected, Hovered, Unselected}
    public status currentStatus;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<MapInteractionScript>().selectableObjects.Add(this.gameObject);

        tileWayPoint = transform.GetChild(0);
        tileWayPoint.gameObject.SetActive(false);

        tileImage = transform.GetChild(1).GetChild(0).GetComponent<Image>();

        anim = GetComponent<Animator>();
    }

    public void ReduceTile()
    {
        anim.SetTrigger("Move");
        tileWayPoint.gameObject.SetActive(true);
    }

    public void Highlight()
    {
        switch (currentStatus)
        {
            case status.Selected:
                tileImage.color = Color.yellow;
                break;
            case status.Hovered:
        tileImage.color = Color.white;
                break;
            case status.Unselected:
        tileImage.color = Color.grey;
                break;
        }
    }

    public void SelectCheck(status newStatus)
    {
        currentStatus = newStatus;
        Highlight();
    }

    public void ResetState()
    {
        currentStatus = status.Unselected;
        Highlight();
    }
}
