using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksCounter : MonoBehaviour
{
    [SerializeField] private List<Block> blocks = new List<Block>();
    [SerializeField] private int blocksCount;

    private void Start()
    {
        blocks.Clear();
        blocks.AddRange(GetComponentsInChildren<Block>());
        blocksCount = blocks.Count;
    }

    public void ReduceBlockCount()
    {
        blocksCount--;
        if (blocksCount <= 0)
        {
            blocks.Clear();
            Invoke("NextLevel", 0.5f);
        }
    }

    private void NextLevel()
    {
        MySceneManager.instance.LoadNextLevel();
    }

    private void OnEnable()
    {
        Block.onBlockHit += ReduceBlockCount;
    }
    private void OnDisable()
    {
        Block.onBlockHit -= ReduceBlockCount;
    }
}
