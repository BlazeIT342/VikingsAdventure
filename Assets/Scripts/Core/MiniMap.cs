using UnityEngine;

public class MiniMap : MonoBehaviour
{
    [SerializeField] GameObject Map;
    Transform PlayerTransform; 

    private void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);
        //MapPosition();
    }

    private void MapPosition()
    {
        Vector3 newPos = PlayerTransform.position; 
        newPos.y = transform.position.y; 
        transform.position = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z); 
    }
}