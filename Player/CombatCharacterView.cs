using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacterView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SetRagdollCollidersState(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetRagdollCollidersState(bool state)
    {
        //TODO: pull the layer in through config?
        var colliders = gameObject.GetComponentsInChildrenOfLayer<Collider>(6);

        foreach (var collider in colliders)
        {
            collider.enabled = state;
        }
    }
}
