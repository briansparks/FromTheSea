using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public BoatView BoatPrefab;
    public BoatSpawnPosition BoatSpawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Instantiate<BoatView>(BoatPrefab, BoatSpawnPosition.transform.position, BoatSpawnPosition.transform.rotation);
    }
}
