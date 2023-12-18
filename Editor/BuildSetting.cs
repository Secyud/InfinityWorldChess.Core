#if UNITY_EDITOR


using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Core.Editor
{
	public class BuildSetting : IPreprocessBuildWithReport, IPostprocessBuildWithReport
	{
		public int callbackOrder => 0;

		public void OnPreprocessBuild(BuildReport report)
		{
			Debug.Log(nameof(OnPreprocessBuild));
		}
		public void OnPostprocessBuild(BuildReport report)
		{
			Debug.Log(nameof(OnPostprocessBuild));

			FileUtil.CopyFileOrDirectory(In("Data"), Out("Data"));
			FileUtil.CopyFileOrDirectory(In("Localization"), Out("Localization"));
			return;

			string Out(string name)
			{
				return Path.Combine(report.summary.outputPath,"..", name);
			}

			string In(string name)
			{
				return Path.Combine(Application.dataPath[..^6], name);
			}
		}
	}
}
#endif