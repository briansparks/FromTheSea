using UnityEngine;

public class LootCartView : MonoBehaviour
{
    private Axle axle;

    private bool isBeingPushed;
    private float wheelRotationSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        axle = gameObject.GetComponentInChildren<Axle>();

        //TODO: testing
        isBeingPushed = true;
    }

    // Update is called once per framewd
    void Update()
    {
        if (isBeingPushed)
        {
            RotateWheels();
        }
    }

    private void RotateWheels()
    {
        axle.transform.Rotate(Vector3.left * (wheelRotationSpeed * Time.deltaTime));
    }

    private void MoveCartTowardsBoat()
    {

    }
}
