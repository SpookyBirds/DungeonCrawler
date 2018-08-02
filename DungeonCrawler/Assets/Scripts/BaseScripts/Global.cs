using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Global : MonoBehaviour {

    public static Global inst;

    public static string[] tags;
    public static Dictionary<int, string> numberedTags;

    public Transform level;
    public GameObject Player;
    public GameObject EmptyHandFist;

    [TagSelector] 
    public string neutralTag = "Neutral";

    private void Awake()
    {
        inst = this;

#if UNITY_EDITOR
        tags = UnityEditorInternal.InternalEditorUtility.tags;
#else
        tags = new string[]{
            "Untagged",
            "Respawn",
            "Finish",
            "EditorOnly",
            "MainCamera",
            "Player",
            "GameController",
            "Friendly_NPC",
            "Enemy_NPC",
            "Neutral"
        };
#endif

        // <Setup the tag dictionary>
        string[] entitiesNames = Enum.GetNames(typeof(Entities));

        numberedTags = new Dictionary<int, string>();
        numberedTags.Add(0, "Untagged");

        for (int index = 1; index < entitiesNames.Length; index++)
        {
            if (tags.Contains(entitiesNames[index]))
                numberedTags.Add(index, entitiesNames[index]);
        }
        // </Setup the tag dictionary>
    }

    /// <summary>
    /// Returns the int values of all selected fields in the flagged enum
    /// </summary>
    /// <typeparam name="TEnum">Has to be an enum</typeparam>
    /// <param name="selection">The enum instance using the "[EnumFlags]"</param>
    /// <returns></returns>
    public static int[] GetSelectedEntries<TEnum>(TEnum selection) where TEnum : struct
    {
        if (!typeof(TEnum).IsEnum)
            throw new ArgumentException("\"type\" must be an enum type");

        List<int> selectedElements = new List<int>();
        for (int i = 0; i < System.Enum.GetValues(typeof(TEnum)).Length; i++)
        {
            int layer = 1 << i;
            if (((int)(ValueType)selection & layer) != 0)
            {
              selectedElements.Add(i);
            }
        }

        return selectedElements.ToArray();
    }

    /// <summary>
    /// Returns the tag corresponding to the Entities enum entry as a string 
    /// </summary>
    public static string GetFactionTag(Entities entitiesEntry)
    {
        return GetFactionTag((int)entitiesEntry);
    }

    /// <summary>
    /// Returns the tag corresponding to the Entities enum entry as a string 
    /// </summary>
    public static string GetFactionTag(int entitiesEntry)
    {
        if(numberedTags.ContainsKey(entitiesEntry))
            return numberedTags[entitiesEntry];

        Debug.LogWarning("Attention! Couldn't find tag for this faction, you propably forgot to assign the right faction.");
        return null;
    }
}

public static class Extentions
{
    /// <summary>
    /// Checks if the tag on the current component is equal to any tags corresponding to an provided entities list
    /// </summary>
    public static bool IsAnyTagEqual(this Component component, int[] entitiesEntries)
    {
        for (int index = 0; index < entitiesEntries.Length; index++) 
        {
            if (component.CompareTag(Global.GetFactionTag(entitiesEntries[index])))
                return true;
        }
        
        return false;
    }

    public static Vector3 MultipliedBy(this Vector3 originalFactor, Vector3 secondFactor)
    {
        return new Vector3(
            originalFactor.x * secondFactor.x, 
            originalFactor.y * secondFactor.y, 
            originalFactor.z * secondFactor.z);
    }

    public static Quaternion AddRotation(this Quaternion origialQuat, Quaternion secondQuat)
    {
        return Quaternion.Euler(origialQuat.eulerAngles + secondQuat.eulerAngles);
    }

    public static Quaternion OnlyY(this Quaternion originalQuat)
    {
        return Quaternion.Euler(0, originalQuat.eulerAngles.y, 0);
    }
}

public enum State
{
    UseLeft,
    UseRight,
    Jump
}

public enum UseType
{
    shortAttack,
    longAttack
}


public enum EquipmentType
{
    Head,
    Body,
    Legs,
    LeftHand,
    RightHand
}


