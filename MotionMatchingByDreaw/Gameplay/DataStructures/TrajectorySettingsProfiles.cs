using MotionMatching.Gameplay;
using System.Collections.Generic;
using UnityEngine;


namespace MotionMatching
{
	[CreateAssetMenu(fileName = "TrajectorySettingsProfiles", menuName = "Motion Matching/Data/Trajectory Settings Profiles")]
	public class TrajectorySettingsProfiles : ScriptableObject
	{
		[SerializeField]
		private List<TrajectorySettingsProfile> profiles;

		Dictionary<string, TrajectorySettingsProfile> profilesMap;

		private void OnEnable()
		{
			Initialize();
		}

		private void Initialize()
		{
			if (profiles != null)
			{
				profilesMap = new Dictionary<string, TrajectorySettingsProfile>();
				foreach (var profile in profiles)
				{
					profilesMap.Add(profile.Name, profile);
				}
			}
		}

		public bool SetProfile(string profileName, TrajectoryMaker trajectoryMaker, MotionMatchingComponent motionMatching)
		{
			if (trajectoryMaker == null || motionMatching == null)
			{
				return false;
			}

			if (profilesMap == null || profilesMap.Count != profiles.Count)
			{
				Initialize();
			}

			if (profilesMap.TryGetValue(profileName, out var profile))
			{
				trajectoryMaker.SetTrajectorySettings(profile.Creation);
				motionMatching.SetTrajectoryCorrectionSettings(profile.Correction);
				return true;
			}

			return false;
		}
	}

	[System.Serializable]
	public class TrajectorySettingsProfile
	{
		[SerializeField]
		public string Name = "Profile";
		[SerializeField]
		public TrajectoryCreationSettings Creation;
		[SerializeField]
		public TrajectoryCorrectionSettings Correction;
	}
}