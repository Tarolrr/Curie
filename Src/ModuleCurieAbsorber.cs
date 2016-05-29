using System;
using UnityEngine;

namespace Curie
{
	public class ModuleCurieAbsorber : PartModule
	{
		public const float ABSORB_COEFF = 0.2f;

		[KSPField(isPersistant = true)]
		float doze = 0;
		MaterialColorUpdater mcu;
		public override void OnStart(StartState state)
		{
			base.OnStart(state);
			if(state == StartState.None) return;
			//this.LogError(part.name);
			//FlightGlobals.Bodies[0].
			//this.
			//this.part.
			//HighLogic.CurrentGame.scenarios[0].
			Vector3d a;
			this.LogError(FlightGlobals.Bodies[0].name);
			this.LogError(Vector3d.Distance(this.transform.position,FlightGlobals.Bodies[0].position).ToString());

		}

		public override void OnLoad(ConfigNode node){

		}

		public void AddDoze(float doze_)
		{
			doze += doze_;
		}

		public override void OnUpdate()
		{
			/*if(mcu == null){
				mcu = new MaterialColorUpdater(transform, HighLogic.ShaderPropertyID_RimColor);
			}
			HSBColor c = new HSBColor(1-Mathf.Clamp(Mathf.Log10(doze)/7-.3f,0,1), 1f, 1f);

			mcu.Update(HSBColor.ToColor(c));*/
			//mcu.Update(new HSBColor(0f,1f,.5f).ToColor());
		}

	}
}

