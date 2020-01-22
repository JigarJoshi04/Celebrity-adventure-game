////////////////////////////////////////////////////////////////////////////////
//  
// @module IOS Deploy
// @author Stanislav Osipov (Stan's Assets) 
// @support support@stansassets.com
//
////////////////////////////////////////////////////////////////////////////////


using UnityEngine;
using System.IO;
using System.Collections.Generic;


#if UNITY_EDITOR
using UnityEditor;
#endif






namespace SA.IOSDeploy {

	#if UNITY_EDITOR
	[InitializeOnLoad]
	#endif
	public class Settings : ScriptableObject{

		public const string VERSION_NUMBER = "2.2";

		public bool IsfwSettingOpen;
		public bool IsLibSettingOpen;
		public bool IslinkerSettingOpne;
		public bool IscompilerSettingsOpen;
		public bool IsPlistSettingsOpen;
		public bool IsLanguageSettingOpen = true;

		public List<Framework> Frameworks =  new List<Framework>();
		public List<Lib> Libraries =  new List<Lib>();





		public List<string> compileFlags =  new List<string>();
		public List<string> linkFlags =  new List<string>();


		public List<Variable>  PlistVariables =  new List<Variable>();

		public List<string> langFolders = new List<string>();

		
		private const string ISDAssetName = "ISDSettingsResource";
		private const string ISDAssetExtension = ".asset";

		private static Settings instance;

		

		public static Settings Instance
		{
			get
			{
				if(instance == null)
				{
					instance = Resources.Load(ISDAssetName) as Settings;
					if(instance == null)
					{
						instance = CreateInstance<Settings>();
						#if UNITY_EDITOR


						SA.Common.Util.Files.CreateFolder(SA.Common.Config.SETTINGS_PATH);
						string fullPath = Path.Combine(Path.Combine("Assets", SA.Common.Config.SETTINGS_PATH), ISDAssetName + ISDAssetExtension );
						
						AssetDatabase.CreateAsset(instance, fullPath);
						#endif

					}
				}
				return instance;
			}
		}



		public bool ContainsFreamworkWithName(string name) {
			foreach(Framework f in Settings.Instance.Frameworks) {
				if(f.Name.Equals(name)) {
					return true;
				}
			}
			
			return false;
		}

		public bool ContainsPlistVarkWithName(string name) {
			foreach(Variable var in Settings.Instance.PlistVariables) {
				if(var.Name.Equals(name)) {
					return true;
				}
			}
			
			return false;
		}
		
		
		public bool ContainsLibWithName(string name) {
			foreach(Lib l in Settings.Instance.Libraries) {
				if(l.Name.Equals(name)) {
					return true;
				}
			}
			
			return false;
		}
							
	}
}
