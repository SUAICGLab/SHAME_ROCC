﻿using UnityEngine;

/// <summary>
/// Класс, содержащий вспомогательные функции
/// </summary>
public static class Helpers
{
    public static Vector3 GetPointInCircle(Vector3 center, float radius)
    {
        Vector2 point = Random.insideUnitCircle * radius;

        center.x += point.x;
        center.z += point.y;

        return center;
    }

    public static bool IsPlayerInTheArea(Vector3 center, float radius)
    {
        var netManager = GameObject.FindObjectOfType<NetManager>();
        if (netManager == null)
        {
            Debug.LogWarning("Helpers.IsPlayerInTheArea: Can't find NetManager on the scene");
            return false;
        }

        foreach (var player in netManager.spawnedPlayers)
            if (Vector3.Distance(player.transform.position, center) < radius)
                return true;

        return false;
    }

    public static T GetComponentInParentAndChildren<T>(this GameObject gameObject)
    {
        return gameObject.transform.root.gameObject.GetComponentInChildren<T>();
    }

    public static T GetComponent<T>(this GameObject gameObject, string transformPath)
    {
        if (!gameObject.transform.Find(transformPath))
        {
            Debug.LogErrorFormat("Helpers.GetComponent: Can't find transform path \"{0}\" at game object {1}",
                                 transformPath, gameObject.name);
            return default(T);
        }

        return gameObject.transform.Find(transformPath).GetComponent<T>();
    }

    public static void EnableComponentsInParentAndChildren<T>(this GameObject gameObject, bool enabled = true) where T : Behaviour
    {
        foreach (var component in gameObject.transform.root.gameObject.GetComponentsInChildren<T>())
            component.enabled = enabled;
    }

    public static void SetLayerRecursively(this GameObject gameObject, string transformPath, Layer layer)
    {
        if (!gameObject.transform.Find(transformPath))
        {
            Debug.LogErrorFormat("Helpers.SetLayerRecursively: Can't find transform path \"{0}\" at game object {1}", 
                                 transformPath, gameObject.name);
            return;
        }

        foreach (Transform tr in gameObject.transform.Find(transformPath).GetComponentsInChildren<Transform>(true))
            tr.gameObject.layer = (int)layer;
    }

    public static void DefineMainCamera(this GameObject gameObject, string transformPath)
    {
        if (!gameObject.transform.Find(transformPath))
        {
            Debug.LogErrorFormat("Helpers.DefineMainCamera: Can't find transform path \"{0}\" at game object {1}", 
                                 transformPath, gameObject.name);
            return;
        }

        if (!gameObject.transform.Find(transformPath).GetComponentInChildren<Camera>())
        {
            Debug.LogErrorFormat("Helpers.DefineMainCamera: Game object {0} has no Camera component at transform path \"{1}\"", 
                                 gameObject.name, transformPath);
            return;
        }

        gameObject.transform.Find(transformPath).tag = "MainCamera";
    }
}
