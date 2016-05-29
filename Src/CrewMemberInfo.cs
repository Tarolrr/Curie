/**
 * Thunder Aerospace Corporation's Life Support for Kerbal Space Program.
 * Written by Taranis Elsu.
 * 
 * (C) Copyright 2013, Taranis Elsu
 * 
 * Kerbal Space Program is Copyright (C) 2013 Squad. See http://kerbalspaceprogram.com/. This
 * project is in no way associated with nor endorsed by Squad.
 * 
 * This code is licensed under the Attribution-NonCommercial-ShareAlike 3.0 (CC BY-NC-SA 3.0)
 * creative commons license. See <http://creativecommons.org/licenses/by-nc-sa/3.0/legalcode>
 * for full details.
 * 
 * Attribution — You are free to modify this code, so long as you mention that the resulting
 * work is based upon or adapted from this code.
 * 
 * Non-commercial - You may not use this work for commercial purposes.
 * 
 * Share Alike — If you alter, transform, or build upon this work, you may distribute the
 * resulting work only under the same or similar license to the CC BY-NC-SA 3.0 license.
 * 
 * Note that Thunder Aerospace Corporation is a ficticious entity created for entertainment
 * purposes. It is in no way meant to represent a real entity. Any similarity to a real entity
 * is purely coincidental.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Curie
{
    public class CrewMemberInfo
    {
        public const string ConfigNodeName = "CrewMemberInfo";

        public readonly string name;
        public string vesselName;
        public Guid vesselId;
		public float doze;

		public CrewMemberInfo(string crewMemberName, string vesselName, Guid vesselId)
        {
            name = crewMemberName;
            this.vesselName = vesselName;
            this.vesselId = vesselId;
        }

        public static CrewMemberInfo Load(ConfigNode node)
        {
            string name = Utilities.GetValue(node, "name", "Unknown");
            string vesselName = Utilities.GetValue(node, "vesselName", "");
            Guid vesselId;
            if (node.HasValue("vesselId"))
            {
                vesselId = new Guid(node.GetValue("vesselId"));
            }
            else
            {
                vesselId = Guid.Empty;
            }

            CrewMemberInfo info = new CrewMemberInfo(name, vesselName, vesselId);

			info.doze = Utilities.GetValue(node, "doze", 0);

            return info;
        }

        public ConfigNode Save(ConfigNode config)
        {
            ConfigNode node = config.AddNode(ConfigNodeName);
            node.AddValue("name", name);
            node.AddValue("vesselName", vesselName);
            node.AddValue("vesselId", vesselId);
			node.AddValue("doze", doze);
            return node;
        }
    }
}
