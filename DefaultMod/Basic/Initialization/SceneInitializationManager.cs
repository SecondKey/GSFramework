using GSFramework.Default;
using GSFramework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SceneInitializationManager : MonoBehaviour
{
    void Start()
    {
        var all = Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[];
        foreach (var o in all.Where(o => o.activeInHierarchy && o.GetComponent<IInitializableObject>() != null))
        {
            InitializeManager.PerformInitialization(o.GetComponent<IInitializableObject>());
        }
    }
}
