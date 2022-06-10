using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Boat BoatPrefab;
    public BoatSpawn BoatSpawn;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Instantiate<Boat>(BoatPrefab, BoatSpawn.transform.position, BoatSpawn.transform.rotation);
    }
}
