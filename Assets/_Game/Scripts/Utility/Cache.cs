using UnityEngine;
using System.Collections.Generic;


public class Cache
{
    private static Camera m_MainCamera;
    public static Camera MainCamera
    {
        get
        {
            if (m_MainCamera == null)
            {
                m_MainCamera = Camera.main;
            }

            return m_MainCamera;
        }
    }

    
    
    //------------------------------------------------------------------------------------------------------------
    private static Dictionary<float, WaitForSeconds> m_WFS = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWFS(float key)
    {
        if (!m_WFS.ContainsKey(key))
        {
            m_WFS[key] = new WaitForSeconds(key);
        }

        return m_WFS[key];
    }

    //------------------------------------------------------------------------------------------------------------
    public static Dictionary<Collider, Character> m_ColliderCharacter = new Dictionary<Collider, Character>();

    public static Character GetCharacter(Collider key)
    {
        if (!m_ColliderCharacter.ContainsKey(key))
        {
            m_ColliderCharacter[key] = key.GetComponent<Character>();
        }

        return m_ColliderCharacter[key];
    }

}

