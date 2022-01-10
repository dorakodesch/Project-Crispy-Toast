using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class OptionsMenu : MonoBehaviour
{
	[SerializeField]
	Dropdown resolutionDropdown;

	Resolution[] resolutions;

	List<string> resolutionOptions; // has to be a list to add options to dropdown

	private void Start()
	{
		resolutions = Screen.resolutions; //
		resolutionOptions = new List<string>();

		for (int i = 0; i < resolutions.Length; i++)
		{
			resolutionOptions.Add(resolutions[i].width + " x " + resolutions[i].height);
		}

		resolutionDropdown.ClearOptions();
		resolutionDropdown.AddOptions(resolutionOptions);
	}

	private void Update()
	{
		int resolutionIndex = resolutionDropdown.value;
		Resolution resolution = resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}
}