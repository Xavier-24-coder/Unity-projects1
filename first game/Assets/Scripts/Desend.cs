using UnityEngine;

public class Desend : MonoBehaviour
{
    public float movementSpeed;
    public float verticalInput;
    public float horizontalInput;
    public float otherHoizontalInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(horizontalInput * movementSpeed * Time.deltaTime, verticalInput * movementSpeed * Time.deltaTime, otherHoizontalInput * movementSpeed * Time.deltaTime);
    }
}
