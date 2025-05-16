using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LayerSorter : MonoBehaviour
{
    public string sortingLayerName = "Player & Boxes"; // Set in Inspector
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerName = sortingLayerName;
    }

    void Update()
    {
        // Multiply to keep decimal precision, then factor in X to resolve tie-breaks
        int sortOrder = (int)(transform.position.y * 1 - transform.position.x * 1.5f);
        sr.sortingOrder = sortOrder;
    }
}
