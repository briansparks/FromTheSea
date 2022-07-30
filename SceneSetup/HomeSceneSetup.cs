using UnityEngine;

public class HomeSceneSetup : MonoBehaviour
{
    [SerializeField]
    private CharacterManager characterManager;

    [SerializeField]
    private BoatManager boatManager;

    [SerializeField]
    private HomeManager homeManager;

    [SerializeField]
    private GameDataManager gameDataManager;

    void Start()
    {
        gameDataManager.Initialize();
        boatManager.Initialize();
        characterManager.Initialize();
        homeManager.Initialize();
    }
}
