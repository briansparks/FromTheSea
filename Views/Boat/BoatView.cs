using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoatView : MonoBehaviour
{
    public float Speed;

    private List<SeatView> seats;
    private Action currentAction;

    private Vector3 lookAtTargetPos;
    private Rigidbody rb;

    private bool reachedDock;
    private bool finishedDocking;

    public void Initialize()
    {
        if (seats == null)
        {
            SetSeats();
        }

        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void InitializeForRaid()
    {
        Initialize();

        var pathToDock = GameObject.FindObjectOfType<PathToDock>()?.GetComponent<DOTweenPath>();
        var pathToEscape = GameObject.FindObjectOfType<PathToEscape>()?.GetComponent<DOTweenPath>();
        var dockPort = GameObject.FindObjectOfType<DockingPort>();

        var boatHead = gameObject.GetComponentInChildren<BoatHead>();
        var endOfDockPoint = dockPort.GetComponentInChildren<EndOfDockPoint>();

        if (pathToDock != null)
        {
            BeginPath(pathToDock);
            UpdateCurrentAction(() => HandleDockPath(boatHead.gameObject, endOfDockPoint.gameObject));
        }
    }
    void Update()
    {
        if (currentAction != null)
        {
            currentAction.Invoke();
        }
    }

    public void BeginPath(DOTweenPath doTweenPath)
    {
        rb.DOPath(doTweenPath.path.wps, 50.0f, PathType.CatmullRom).OnWaypointChange((waypointIndex) => OnWaypointChange(waypointIndex, doTweenPath));
    }

    public void HandleDockPath(GameObject boatHeadObject, GameObject endOfDockPointObj)
    {
        //LookAtNextWaypoint();

        if (!reachedDock)
        {
            if (boatHeadObject.ReachedDestinationTarget(endOfDockPointObj.transform.position, 7.0f))
            {
                Debug.Log("Triggering BoatInRangeOfDock!");
                EventManager.TriggerEvent("BoatInRangeOfDock");
                reachedDock = true;
            }
        }
    }
    public void LookAtNextWaypoint()
    {
        if (lookAtTargetPos != null)
        {
            LookAtTarget(lookAtTargetPos, 1.0f);
        }
    }
    private void OnWaypointChange(int waypointIndex, DOTweenPath doTweenPath)
    {
        lookAtTargetPos = doTweenPath.path.wps[waypointIndex];
    }

    private void LookAtTarget(Vector3 target, float rotationSpeed)
    {
        var direction = target - gameObject.transform.position;
        var rotation = Quaternion.LookRotation(direction);

        float increment = rotationSpeed * Time.fixedDeltaTime;

        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, rotation, increment));
    }

    private void SetSeats()
    {
        seats = gameObject.GetComponentsInChildren<SeatView>().ToList();
    }

    public void DockBoat(GameObject endOfDockObj, GameObject boatLookAtPoint)
    {
        if (!finishedDocking)
        {
            LookAtTarget(boatLookAtPoint.transform.position, 3.0f);

            if (Vector3.Angle(gameObject.transform.forward, gameObject.transform.position - boatLookAtPoint.transform.position) < 3.0f)
            {
                EventManager.TriggerEvent("BoatDocked");
                finishedDocking = true;
            }
        }
    }
    public bool TryFindNextOpenSeat(out SeatView openSeat)
    {
        var unoccupiedSeats = seats.Where(s => !s.IsOccupied);

        if (unoccupiedSeats.Any())
        {
            openSeat = unoccupiedSeats.ToList().OrderBy(s => s.Priority).First();
            return true;
        }

        openSeat = null;
        return false;
    }

    public bool TryFindSeatById(Guid seatId, out SeatView seat)
    {
        try
        {
            seat = seats.First(s => s.GetId() == seatId);
            return true;
        }
        catch (Exception ex)
        {
            seat = null;
            Debug.LogError($"Failed to find seat {seatId} on boat! {ex}", this);
            return false;
        }
    }
    public void ResetAllSeats()
    {
        foreach (var seat in seats)
        {
            seat.IsOccupied = false;
        }
    }

    public void UpdateCurrentAction(Action nextAction)
    {
        currentAction = nextAction;
    }
}
