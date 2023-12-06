using UnityEngine;


public class CameraMove : MonoBehaviour
{
    private Vector3 distance;
    private Vector3 originPosition;

    private bool canDrag = false;

    private float zoomSpeed = 0.5f;
    private float minZoomFOV = 5f;
    private float maxZoomFOV = 18f;

    private Rect screenLimits;

    private void Start()
    {
        screenLimits = new Rect(0, 0, Screen.width, Screen.height);
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            distance = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
            if (!canDrag && Input.mousePosition.x > 200)
            {
                canDrag = true;
                originPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            canDrag = false;
        }

        if (canDrag)
        {
            Camera.main.transform.position = originPosition - distance;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f && GetComponent<Camera>().orthographicSize > minZoomFOV && screenLimits.Contains(Input.mousePosition))
        {
            GetComponent<Camera>().orthographicSize -= zoomSpeed;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f && GetComponent<Camera>().orthographicSize < maxZoomFOV && screenLimits.Contains(Input.mousePosition))
        {
            GetComponent<Camera>().orthographicSize += zoomSpeed; ;
        }

    }
}
