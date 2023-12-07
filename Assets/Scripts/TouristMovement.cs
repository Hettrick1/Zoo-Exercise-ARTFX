using UnityEngine;

public class TouristMovement : MonoBehaviour
{
    private int goHome;
    private float speed = 1;
    private float chillTime = 1f;

    private bool justSpawn = true;
    private bool canMove;
    private bool isGoingHome;

    private Vector2 targetPosition1;
    private Vector2 targetPosition;
    private Vector2 targetPosition2;

    private Vector2 targetPosition3;

    private GameObject road;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        road = GameObject.Find("Road");
        targetPosition1 = new Vector2(1, -12);
        targetPosition2 = new Vector2(-1.2f, -13);
        targetPosition3 = new Vector2(-2, -27);
    }

    // Update is called once per frame
    void Update()
    {
        if (justSpawn)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition1, speed * gameManager.GetSpeedTime() * Time.deltaTime);
        }
        if (!justSpawn && goHome < 5)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * gameManager.GetSpeedTime() * Time.deltaTime);
        }

        if ((Vector3)targetPosition1 == transform.position) 
        {
            justSpawn = false;
            Direction();
        }
        if (targetPosition.x == transform.position.x && targetPosition.y == transform.position.y && canMove)
        {
            canMove = false;
            Invoke(nameof(Direction), chillTime / gameManager.GetSpeedTime());
        }
        if (goHome >=5 && canMove && !isGoingHome)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition2, speed * Time.deltaTime);
            if (targetPosition2.x == transform.position.x && targetPosition2.y == transform.position.y)
            {
                canMove = false;
                Invoke(nameof(GoHome), chillTime / gameManager.GetSpeedTime());
            }
        } 

        if (isGoingHome)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition3, speed * gameManager.GetSpeedTime() * Time.deltaTime);
        }
        if (isGoingHome && targetPosition3.x == transform.position.x && targetPosition3.y == transform.position.y)
        {
            Disapear();
        }
    }

    private void Direction()
    {
        speed = Random.Range(0.4f, 1f);
        chillTime = Random.Range(2f, 4f);
        goHome += 1;

        targetPosition = new Vector2(Random.Range(road.transform.position.x - (road.GetComponent<SpriteRenderer>().bounds.size.x / 2), road.transform.position.x + (road.GetComponent<SpriteRenderer>().bounds.size.x / 2)), Random.Range(road.transform.position.y - (road.GetComponent<SpriteRenderer>().bounds.size.y / 2), road.transform.position.y + (road.GetComponent<SpriteRenderer>().bounds.size.y / 2)));
        canMove = true;
    }

    private void GoHome()
    {
        isGoingHome = true;
    }

    private void Disapear()
    {
        Destroy(gameObject);
        gameManager.InvokeTourists();
    }
}
