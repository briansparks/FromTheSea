using UnityEngine;

public class RaidSceneSetup : MonoBehaviour
{
    [SerializeField]
    private GameDataManager gameDataManager;

    [SerializeField]
    private RaidManager raidManager;

    [SerializeField]
    private BoatManager boatManager;

    [SerializeField]
    private CharacterManager characterManager;

    // Start is called before the first frame update
    void Start()
    {
        gameDataManager.Initialize();
        boatManager.Initialize();
        characterManager.Initialize();
        raidManager.Initialize();
    }
}
