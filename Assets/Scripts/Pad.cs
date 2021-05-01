using UnityEngine;

public class Pad : MonoBehaviour
{
    #region Variables

    [SerializeField] private float boundary;

    #endregion


    #region Unity Lifecycle

    private void Update()
    {
        Vector3 positionInPixels = Input.mousePosition;
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(positionInPixels);
        Vector3 padPosition = positionInWorld;

        padPosition.y = transform.position.y;
        padPosition.z = 0;

        transform.position = padPosition;
        
        //чтобы пад за границы экрана не выходил
        if (padPosition.x < -boundary)
        {
            transform.position = new Vector3(-boundary, padPosition.y, padPosition.z);
        }

        if (padPosition.x > boundary)
        {
            transform.position = new Vector3(boundary, padPosition.y, padPosition.z);
        }
    }

    #endregion
}
