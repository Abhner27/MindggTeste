using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField]
    private GameObject _treeGraphics;
    [SerializeField]
    private GameObject _logPrefab;

    public void Respawn()
    {
        _treeGraphics.SetActive(true);
    }

    public void Cut()
    {
        if (this == null)
            return;

        _treeGraphics.SetActive(false);
        Instantiate(_logPrefab, transform.position, Quaternion.identity);
    }
}