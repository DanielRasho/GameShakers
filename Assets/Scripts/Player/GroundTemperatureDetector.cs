using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

public class GroundTemperatureByTilemap : MonoBehaviour
{
    [Header("Tilemaps de piso")]
    [SerializeField] private Tilemap groundNeutralTilemap;
    [SerializeField] private Tilemap groundColdTilemap;
    [SerializeField] private Tilemap groundHotTilemap;

    [Header("Detección")]
    [SerializeField] private Vector3 feetOffset = new Vector3(0f, -0.2f, 0f);

    [Header("Tiempos")]
    [SerializeField] private float slowInterval = 3f;
    [SerializeField] private float fastInterval = 2f;

    [Header("Temperatura")]
    [SerializeField] private int currentTemperature = 0;

    private float timer = 0f;
    private GroundType lastGroundType = GroundType.None;
    private bool lastMovingState = false;

    private enum GroundType
    {
        None,
        Neutral,
        Hot,
        Cold
    }

    private void Update()
    {
        GroundType currentGround = DetectGroundType();
        bool isMoving = IsPlayerMoving();

        if (currentGround != lastGroundType || isMoving != lastMovingState)
        {
            timer = 0f;
            lastGroundType = currentGround;
            lastMovingState = isMoving;
        }

        if (currentGround == GroundType.Neutral || currentGround == GroundType.None)
            return;

        float currentInterval = GetCurrentInterval(currentGround, isMoving);

        timer += Time.deltaTime;

        if (timer >= currentInterval)
        {
            ApplyTemperatureEffect(currentGround, isMoving);
            timer = 0f;
        }
    }

    private GroundType DetectGroundType()
    {
        Vector3 checkPosition = transform.position + feetOffset;

        if (IsOnTilemap(groundHotTilemap, checkPosition))
            return GroundType.Hot;

        if (IsOnTilemap(groundColdTilemap, checkPosition))
            return GroundType.Cold;

        if (IsOnTilemap(groundNeutralTilemap, checkPosition))
            return GroundType.Neutral;

        return GroundType.None;
    }

    private bool IsOnTilemap(Tilemap tilemap, Vector3 worldPosition)
    {
        if (tilemap == null) return false;

        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition) != null;
    }

    private bool IsPlayerMoving()
    {
        if (Keyboard.current == null) return false;

        return Keyboard.current.wKey.isPressed ||
               Keyboard.current.aKey.isPressed ||
               Keyboard.current.sKey.isPressed ||
               Keyboard.current.dKey.isPressed;
    }

    private void ApplyTemperatureEffect(GroundType groundType, bool isMoving)
    {
        switch (groundType)
        {
            case GroundType.Hot:
                currentTemperature += 1;
                Debug.Log($"HOT | {(isMoving ? "Moviéndose" : "Quieto")} | Temperatura: {currentTemperature}");
                break;

            case GroundType.Cold:
                currentTemperature -= 1;
                Debug.Log($"COLD | {(isMoving ? "Moviéndose" : "Quieto")} | Temperatura: {currentTemperature}");
                break;
        }
    }

    private float GetCurrentInterval(GroundType groundType, bool isMoving)
    {
        switch (groundType)
        {
            case GroundType.Hot:
                return isMoving ? fastInterval : slowInterval;

            case GroundType.Cold:
                return isMoving ? slowInterval : fastInterval;

            default:
                return slowInterval;
        }
    }
}