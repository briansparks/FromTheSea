using Cinemachine;
using UnityEngine;

public class AbstractStationPanelView : MonoBehaviour
{
    public GameObject StationGameObject;
    void Awake()
    {
        var cameraPos = StationGameObject.GetComponentInChildren<StationCameraPosition>();

        var camera = GameObject.FindGameObjectWithTag("MainCamera");

        var cinemachineBrian = camera.GetComponentInChildren<CinemachineBrain>();
        cinemachineBrian.enabled = false;

        camera.transform.SetParent(StationGameObject.transform);
        camera.transform.position = cameraPos.transform.position;
        camera.transform.rotation = cameraPos.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
