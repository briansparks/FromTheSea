using System;
using UnityEngine;
using UnityEngine.Events;

public class BoatOrchestrator : MonoBehaviour
{
    public BoatManager BoatManager;

    // Start is called before the first frame update
    void Start()
    {
        var updateActiveBoatByIdAction = new UnityAction<Guid>((boatId) => { HandleUpdateActiveBoatById(boatId); });
        EventManager.StartListening("UpdateActiveBoatById", updateActiveBoatByIdAction);
    }

    private void HandleUpdateActiveBoatById(Guid boatId)
    {
        BoatManager.UpdateActiveBoatById(boatId);
    }
}
