using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using SimpleFileBrowser;
using Parseur;

public class Menu : MonoBehaviour
{
	private Parseur.Parseur p;
	Dictionary<string, List<Dictionary<string, object>>> elementInScene;
	void Start()
	{
		p = new Parseur.Parseur();
	}

	public void SelectInputFile()
    {
		// Set filters (optional)
		// It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
		// if all the dialogs will be using the same filters
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Input", ".txt"), new FileBrowser.Filter("Text Files", ".txt", ".pdf"));

		// Set default filter that is selected when the dialog is shown (optional)
		// Returns true if the default filter is set successfully
		// In this case, set Images filter as the default filter
		FileBrowser.SetDefaultFilter(".txt");

		// Set excluded file extensions (optional) (by default, .lnk and .tmp extensions are excluded)
		// Note that when you use this function, .lnk and .tmp extensions will no longer be
		// excluded unless you explicitly add them as parameters to the function
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");

		StartCoroutine(ShowLoadDialogCoroutine());
	}

	IEnumerator ShowLoadDialogCoroutine()
	{
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");
		if (FileBrowser.Success)
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			for (int i = 0; i < FileBrowser.Result.Length; i++)
			{
				elementInScene= p.readFile(FileBrowser.Result[i]);
			}

			Global.inScene = elementInScene;
			/*// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

			// Or, copy the first file to persistentDataPath
			string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
			FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);*/
		}
	}

	IEnumerator ConfigFileCoroutine()
    {
		// Show a load file dialog and wait for a response from user
		// Load file/folder: both, Allow multiple selection: true
		// Initial path: default (Documents), Initial filename: empty
		// Title: "Load File", Submit button text: "Load"
		yield return FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.FilesAndFolders, true, null, null, "Load Files and Folders", "Load");


		if (FileBrowser.Success)
		{
			// Print paths of the selected files (FileBrowser.Result) (null, if FileBrowser.Success is false)
			for (int i = 0; i < FileBrowser.Result.Length; i++)
			{
				elementInScene = p.readFile(FileBrowser.Result[i]);
			}

			Global.inScene = elementInScene;
			// Read the bytes of the first file via FileBrowserHelpers
			// Contrary to File.ReadAllBytes, this function works on Android 10+, as well
			byte[] bytes = FileBrowserHelpers.ReadBytesFromFile(FileBrowser.Result[0]);

			// Or, copy the first file to persistentDataPath
			string destinationPath = Path.Combine(Application.persistentDataPath, FileBrowserHelpers.GetFilename(FileBrowser.Result[0]));
			FileBrowserHelpers.CopyFile(FileBrowser.Result[0], destinationPath);
		}
	}
	public void SelectConfigFile()
    {
		// Set filters (optional)
		// It is sufficient to set the filters just once (instead of each time before showing the file browser dialog), 
		// if all the dialogs will be using the same filters
		FileBrowser.SetFilters(true, new FileBrowser.Filter("Input", ".json"));
		FileBrowser.SetDefaultFilter(".json");
		FileBrowser.SetExcludedExtensions(".lnk", ".tmp", ".zip", ".rar", ".exe");
	
		StartCoroutine(ConfigFileCoroutine());
	}
	public void LoadLevel(int levelNumber)
    {
		Global.loading.startShowPanel(Global.LoadingType.LoadLevel1);
    }

	// Update is called once per frame
	void Update()
    {
        
    }
}
