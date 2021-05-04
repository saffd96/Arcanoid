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

        var transform1 = transform;
        padPosition.y = transform1.position.y;
        padPosition.z = 0;

        transform1.position = padPosition;
        
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
