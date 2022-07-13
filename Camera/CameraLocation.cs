using UnityEngine;

public class CameraLocation : MonoBehaviour
{
    private HeadBone headBone;

    // Start is called before the first frame update
    void Start()
    {
        headBone = gameObject.transform.parent.GetComponentInChildren<HeadBone>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = headBone.transform.position;
    }
}
