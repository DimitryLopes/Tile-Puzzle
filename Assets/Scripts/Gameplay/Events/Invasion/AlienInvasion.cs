using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienInvasion : GameEvent
{
    [SerializeField, Space]
    private int numberOfShips = 10;
    [SerializeField]
    private float spawnInterval = 5f;
    [SerializeField]
    private AlienShip shipPrefab;
    [SerializeField]
    private Vector3 targetPosition;
    [SerializeField]
    private float shipSpawnDistance;
    [SerializeField]
    private Transform shipContainer;

    private List<AlienShip> alienShips = new List<AlienShip>();

    private float spawnTimer;
    private int shipsDestroyed = 0;

    public override void EndEvent(bool isWin)
    {
        if (isWin)
        {
            foreach (var ship in alienShips)
            {
                if (!ship.IsActive) continue;
                ship.DestroyShip();
            }
        }

        EventManager.OnGameEventEnded.Invoke(this, isWin);
    }

    public override void StartEvent()
    {
        shipsDestroyed = 0;
        //play music
    }

    public override void UpdateEvent()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            GetAvailableShip();
            spawnTimer = spawnInterval;
        }
    }

    private void OnShipReachedTarget()
    {
        EndEvent(false);
    }

    private void OnShipClicked()
    {
        shipsDestroyed++;
        if (shipsDestroyed >= numberOfShips)
        {
            EndEvent(true);
        }
    }

    private void GetAvailableShip()
    {
        foreach(AlienShip ship in alienShips)
        {
            if (ship.IsActive) continue;
            {
                ship.Setup(targetPosition, OnShipReachedTarget, OnShipClicked);
                ship.Activate();
                PositionShip(ship);
                return;
            }
        }

        var newShip = Instantiate(shipPrefab, shipContainer);
        newShip.Setup(targetPosition, OnShipReachedTarget, OnShipClicked);
        newShip.Activate();
        PositionShip(newShip);
        alienShips.Add(newShip);
    }

    private void PositionShip(AlienShip ship)
    {
        ship.rectTransform.anchoredPosition = Random.insideUnitCircle.normalized * shipSpawnDistance;

    }
}
