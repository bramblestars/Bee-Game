using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveIndicator : MonoBehaviour
{
    [SerializeField] GameObject bee;
    [SerializeField] GameObject arrow;
    [SerializeField] float lengthToAppear = 5f;
    [SerializeField] float cameraHeight = 9f;
    [SerializeField] float cameraWidth = 21f;
    [SerializeField] float arrowOffset = 2f;

    private side sideToAppearOn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        
        if(canSeeHive())
        {
            transform.position = bee.transform.position + new Vector3(0, 90f, 0);
            arrow.transform.position = transform.position;
        }
        else 
        {
            transform.position = bee.transform.position + getOffsetVectorAndSetArrow(bee.transform.position);
        }
    }

    private bool canSeeHive()
    {
        return bee.transform.position.magnitude < lengthToAppear;
    }

    private side getSide(Vector3 beePos)
    {
        if (Mathf.Abs(beePos.y) >= Mathf.Abs(beePos.x))
        {
            return beePos.y > 0? side.Bottom : side.Top;
        }
        else  
        {
            return beePos.x > 0? side.Left : side.Right;
        }
    }

    private Vector3 getOffsetVectorAndSetArrow(Vector3 beePos)
    {
        
        side currentSide = getSide(beePos);
        Vector3 offset = Vector3.zero;
        float leftRightAngle = Vector3.Angle(Vector3.up, beePos) * Mathf.Deg2Rad;
        float topBottomAngle = Vector3.Angle(Vector3.right, beePos) * Mathf.Deg2Rad;
        switch (currentSide)
        {
            case side.Top:
                offset = new Vector3(-Mathf.Cos(topBottomAngle) * cameraWidth * 0.7f, cameraHeight / 2f, 0);
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, Vector3.Angle(Vector3.right, -bee.transform.position) - 90f);
                break;
            case side.Bottom:
                offset = new Vector3(-Mathf.Cos(topBottomAngle) * cameraWidth * 0.7f, -cameraHeight / 2f, 0);
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, -Vector3.Angle(Vector3.right, -bee.transform.position) - 90f);
                break;
            case side.Right:
                offset = new Vector3(cameraWidth / 2f, -Mathf.Cos(leftRightAngle) * cameraHeight * 0.7f, 0);
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, -Vector3.Angle(Vector3.up, -bee.transform.position));
                break;
            case side.Left:
                offset = new Vector3(-cameraWidth / 2f, -Mathf.Cos(leftRightAngle) * cameraHeight * 0.7f, 0);
                arrow.transform.rotation = Quaternion.Euler(0f, 0f, Vector3.Angle(Vector3.up, -bee.transform.position));
                break;
        }
        float arrowAngle = arrow.transform.eulerAngles.z * Mathf.Deg2Rad;
        arrow.transform.position = transform.position + arrowOffset * new Vector3(-Mathf.Sin(arrowAngle), Mathf.Cos(arrowAngle),0);
        return offset;
    }

    private enum side
    {
        Top,
        Bottom,
        Left,
        Right
    }

}
