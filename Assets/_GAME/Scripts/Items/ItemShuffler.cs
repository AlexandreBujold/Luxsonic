using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ItemShuffler : MonoBehaviour
{
    [SerializeField]
    private float scaleTime = 0.5f;

    [SerializeField]
    private List<GameObject> objectsToAppear = new List<GameObject>();

    [SerializeField]
    private List<Transform> objectPositions = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        ShuffleObjects();
    }

    private void ShuffleObjects()
    {
        //Duplicate List
        List<Transform> positionsAvailable = new List<Transform>();
        for (int i = 0; i < objectPositions.Count; i++)
        {
            positionsAvailable.Add(objectPositions[i]);
        }

        for (int i = 0; i < objectPositions.Count; i++)
        {
            if (i >= objectsToAppear.Count)
            {
                break;
            }

            Transform chosenTransform = positionsAvailable[Random.Range(0, positionsAvailable.Count)];
            positionsAvailable.Remove(chosenTransform);
            objectsToAppear[i].transform.position = chosenTransform.position;
        }

        StartCoroutine(ScaleObjects());
    }

    private IEnumerator ScaleObjects()
    {
        List<Vector3> endScales = new List<Vector3>();

        foreach (GameObject item in objectsToAppear)
        {
            endScales.Add(item.transform.localScale);
        }

        for (float time = 0; time < scaleTime; time += Time.deltaTime)
        {
            float t = time / scaleTime;
            for (int i = 0; i < objectsToAppear.Count; i++)
            {
                objectsToAppear[i].transform.localScale = Vector3.Lerp(endScales[i] / 10, endScales[i], t);
            }
            yield return null;
        }
    }
}
