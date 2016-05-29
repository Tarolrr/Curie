using System;
using UnityEngine;

namespace Curie{
	[KSPAddon(KSPAddon.Startup.SpaceCentre, true)]
	public class CurieAddonConfig : MonoBehaviour{
		//PartLoader
		public void Awake(){

			//Part evaPrefab = PartLoader.getPartInfoByName("kerbalEVA").partPrefab;
			//try { evaPrefab.AddModule("ModuleCurieTarget"); }
			//catch { }

		}

		public void Start(){
		}

		void FixedUpdate(){
		}
	}
}

