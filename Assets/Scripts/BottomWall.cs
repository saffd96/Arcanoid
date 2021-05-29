using UnityEngine;

public class BottomWall : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(Tags.PickUp))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag(Tags.Ball))
        {
            GameManager.Instance.RemoveLive();
        }
    }
    
}