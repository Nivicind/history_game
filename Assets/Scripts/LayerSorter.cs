using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class LayerSorter : MonoBehaviour
{
    public string sortingLayerName = "World Interactive";
    public int sortingOffset = 0;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sortingLayerName = sortingLayerName;
    }

    void Update()
    {
        // Invert Y so higher Y = higher order (in front)
        // Invert X so lower X = higher order (in front)
        float yWeight = 100f;
        float xWeight = 10f;

        int sortOrder = Mathf.RoundToInt(transform.position.y * yWeight - transform.position.x * xWeight) + sortingOffset;
        sr.sortingOrder = sortOrder;
    }
}
