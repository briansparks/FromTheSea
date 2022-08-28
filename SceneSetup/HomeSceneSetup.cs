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

    [SerializeField]
    private EquipmentAssignmentManager equipmentAssignmentManager;

    [SerializeField]
    private ResourceManager resourceManager;
    void Start()
    {
        gameDataManager.Initialize();
        resourceManager.Initialize();
        equipmentAssignmentManager.Initialize();
        boatManager.Initialize();
        characterManager.Initialize();
        homeManager.Initialize();
    }
}
