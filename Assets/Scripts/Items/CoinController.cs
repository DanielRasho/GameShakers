using System;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Collider2D collider;
    
    [SerializeField] private AudioClip SoundFx;

    private void Awake()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlayFX(SoundFx);
            GameObject.FindFirstObjectByType<LevelManager>().AddCoin();
            Destroy(gameObject);
        }
    }
}
