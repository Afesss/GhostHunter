using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Canvas canvas;
    [SerializeField] private Ghost prefab;
    [Header("Settings")]
    [SerializeField, Range(0, 2)] private float spawnTime;
    [SerializeField] private int maxGhostCount;

    private float LeftBorder => Screen.width * 0.1f;
    private float RightBorder => Screen.width * 0.9f;
    private readonly byte _poolCount = 30;

    private List<Ghost> _ghostPool = new List<Ghost>();

    [Inject] private DiContainer _diContainer;

    private void Start()
    {
        for(var i = 0; i < _poolCount; i++)
        {
            InstantiateGhost();
        }

        StartCoroutine(Spawn());
    }
    
    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            var activeGhostCount = 0;

            for(var i = 0; i < _ghostPool.Count; i++)
            {
                if (_ghostPool[i].IsActive)
                    activeGhostCount++;
            }

            if (activeGhostCount >= maxGhostCount)
                continue;

            bool activated = false;
            for(var i = 0; i < _ghostPool.Count; i++)
            {
                if (!_ghostPool[i].IsActive)
                {
                    activated = true;
                    ActivateGhost(_ghostPool[i]);
                    break;
                }
            }

            if (!activated)
            {
                var ghost = InstantiateGhost();
                ActivateGhost(ghost);
            }
            
        }
    }

    private void ActivateGhost(Ghost ghost)
    {
        var xPos = Random.Range(LeftBorder, RightBorder);
        ghost.SetPosition(new Vector3(xPos,
            -(ghost.Rect.sizeDelta.y * canvas.scaleFactor * 0.5f), 0));
        ghost.SetActive(true);
    }

    private Ghost InstantiateGhost()
    {
        var obj = _diContainer.InstantiatePrefab(prefab.gameObject, transform);
        var ghost = obj.GetComponent<Ghost>();
        ghost.SetActive(false);
        _ghostPool.Add(ghost);
        return ghost;
    }
}
