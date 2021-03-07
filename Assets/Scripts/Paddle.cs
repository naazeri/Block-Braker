using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float screenWidthInUnits = 16f;
    [SerializeField] private float minX = 0f;
    [SerializeField] private float maxX = 16f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Input.mousePosition.x / Screen.width * screenWidthInUnits
        var mousePositionX = Input.mousePosition.x / Screen.width * screenWidthInUnits;
        var safePosition = Mathf.Clamp(mousePositionX, minX, maxX);
        transform.position = new Vector2(safePosition, transform.position.y);;
    }
}