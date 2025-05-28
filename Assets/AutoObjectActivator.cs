using UnityEngine;
using System.Collections.Generic;

public class OptimizedAutoActivator : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 10f;
    public float checkInterval = 0.5f; // Kontrol aralýðý (saniye)
    public string[] targetTags = { "Road", "City" };

    private List<GameObject> targetObjects = new List<GameObject>();
    private Dictionary<GameObject, bool> objectStates = new Dictionary<GameObject, bool>();

    void Start()
    {
        foreach (string tag in targetTags)
        {
            GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in foundObjects)
            {
                targetObjects.Add(obj);
                obj.SetActive(false);
                objectStates[obj] = false;
            }
        }

        InvokeRepeating(nameof(CheckObjects), 0f, checkInterval);
    }

    void CheckObjects()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj == null) continue;

            float distance = Vector3.Distance(player.position, obj.transform.position);
            bool isActive = objectStates[obj];

            if (distance <= activationDistance && !isActive)
            {
                obj.SetActive(true);
                objectStates[obj] = true;
            }
            else if (distance > activationDistance && isActive)
            {
                obj.SetActive(false);
                objectStates[obj] = false;
            }
        }
    }
}
