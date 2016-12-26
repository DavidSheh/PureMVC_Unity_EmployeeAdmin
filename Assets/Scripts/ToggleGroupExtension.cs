using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleGroupExtension : ToggleGroup
{
    public Action<Toggle> onValueChanged;

    private List<Toggle> toggles = new List<Toggle>();

	// Use this for initialization
	protected override void Awake () {
        Toggle[] tempToggles = transform.GetComponentsInChildren<Toggle>();
        toggles.AddRange(tempToggles);
        for (int i = 0; i < tempToggles.Length; i++)
        {
            tempToggles[i].onValueChanged.AddListener((check) => 
            {
                if (check && null != onValueChanged)
                {
                    onValueChanged(tempToggles[i]);
                }
            });
        }
    }

    public void Add(Toggle toggle)
    {
        toggle.transform.parent = transform;

        toggle.onValueChanged.AddListener((check) =>
        {
            if (check && null != onValueChanged)
            {
                onValueChanged(toggle);
            }
        });
    }

    public void Remove(Toggle toggle)
    {
        toggle.transform.parent = null;

        toggle.onValueChanged.RemoveAllListeners();

        Destroy(toggle);
    }

    public void Clear()
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            toggles[i].onValueChanged.RemoveAllListeners();
        }

        toggles.Clear();
    }
}
