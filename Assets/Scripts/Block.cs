using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockVFX;
    [SerializeField] int maxHits = 1;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    private Level _level;
    private GameStatus _gameStatus;

    // state variable
    [SerializeField] int timesHit;  // just for debug

    private void Start()
    {
        _level = FindObjectOfType<Level>();
        _gameStatus = FindObjectOfType<GameStatus>();
        if (CompareTag("Breakable"))
        {
            _level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (CompareTag("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);

        TriggerVFX();

        if (timesHit >= maxHits)
        {
            DestroyBlock();
            _gameStatus.AddToScore();
        }
        else
        {
            ShowNextHitSprite();
        }
    }
    
    private void ShowNextHitSprite()
    {
        GetComponent<SpriteRenderer>().sprite = hitSprites[timesHit - 1];
    }

    private void DestroyBlock()
    {
        Destroy(gameObject);
        _level.BlockDestroyed();
    }

    private void TriggerVFX()
    {
        // Create
        GameObject sparkles = Instantiate(blockVFX, transform.position, transform.rotation);

        //Destroy
        Destroy(sparkles, 2f);
    }
}