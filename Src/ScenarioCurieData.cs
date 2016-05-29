using System;
using System.Collections.Generic;
using UnityEngine;

namespace Curie
{
	[KSPScenario(ScenarioCreationOptions.AddToAllGames, GameScenes.SPACECENTER, GameScenes.FLIGHT, GameScenes.TRACKSTATION)]
	public class ScenarioCurieData : ScenarioModule
	{
		List<CrewMemberInfo> crew;
		List<CuriePlaceEmitter> placeEmitters;
		public ScenarioCurieData(){
			crew = new List<CrewMemberInfo>();
			placeEmitters = new List<CuriePlaceEmitter>();
		}

		public override void OnAwake (){
			//if( FlightGlobals.Bodies[0].gameObject.GetComponent<CuriePlaceEmitter>() == null )
			//	FlightGlobals.Bodies[0].gameObject.AddComponent<CuriePlaceEmitter>();
			foreach(Vessel v in FlightGlobals.Vessels)
				if(v.GetComponent<BoundsRenderer>() == null){
					//var br = v.gameObject.AddComponent<BoundsRenderer>();
					//br.Init(v);
				}
		}

		public override void OnSave (ConfigNode node){
			base.OnSave(node);
			//node.ClearData(); 
			foreach( var cmi in crew ){
				cmi.Save(node);
			}   
		}

		public override void OnLoad (ConfigNode node){
			base.OnLoad(node); 
			var crewNodes = node.GetNodes(CrewMemberInfo.ConfigNodeName);
			foreach(ConfigNode n in crewNodes)
			{ 
				crew.Add(CrewMemberInfo.Load(n));
			}
			//crew 
			var allCrew = HighLogic.CurrentGame.CrewRoster.Crew;
			foreach(var c in allCrew)
			{
				if(crew.FindIndex(c_ => c_.name == c.name) == -1)
					crew.Add(new CrewMemberInfo(c.name,"",Guid.Empty));
			}

			var allVessels = FlightGlobals.Vessels;
			this.Log(allVessels.Count.ToString());
			foreach(Vessel v in allVessels)
			{
				var currentCrew = v.GetVesselCrew();
				foreach(ProtoCrewMember pcm in currentCrew)
				{
					int idx = crew.FindIndex(c => c.name == pcm.name);
					if( idx == -1 )
						this.LogError("not found");
					crew[idx].vesselName = v.GetName();
					crew[idx].vesselId = v.id;
				}
			}
			//Loading CuriePlaceEmitters
			var nodes = node.GetNodes(CuriePlaceEmitter.ConfigNodeName);
			foreach(var n in nodes){
				placeEmitters.Add(CuriePlaceEmitter.Load(n));
				this.Log("PlaceEmitter for " + placeEmitters[placeEmitters.Count-1].bodyName + " loaded from save");
			}
			var defaultNodes = GameDatabase.Instance.GetConfigNode("Curie/CurieSettings").GetNodes(CuriePlaceEmitter.ConfigNodeName);
			this.Log(defaultNodes.GetLength(0).ToString());
			foreach(var n in defaultNodes){
				this.Log(n.ToString());
				if(placeEmitters.FindIndex(pe => pe.bodyName == n.GetValue("bodyName")) != -1)
					continue;
				placeEmitters.Add(CuriePlaceEmitter.Load(n));
				this.Log("PlaceEmitter for " + placeEmitters[placeEmitters.Count-1].bodyName + " loaded from config");
			}
		}

		void FixedUpdate()
		{
			foreach(var e in placeEmitters)
			{
				e.FixedUpdate();
			}
		}

		void OnDestroy()
		{
			//foreach (Component c in children)
			//{
			//	Destroy(c);
			//}
			//children.Clear();
		}
	}
}

