using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Pathfinding.Serialization;

namespace HoneyFramework
{
    /*
     * A* pathfinder hex graph which wraps functionality from point graph and adjust them to hex framework needs.
     */

    [JsonOptIn]
    public class HexGraph : PointGraph
    {

        /// <summary>
        /// Scans world hexes and ensures their existence in hex graph for pathfinder
        /// </summary>
        /// <param name="statusCallback"></param>
        /// <returns></returns>
        protected override IEnumerable<Progress> ScanInternal()
        {
            int hexradius = World.instance.hexRadius;
            List<PointNode> pointNodes = new List<PointNode>();
            Dictionary<Int3, PointNode> nodeDictionary = new Dictionary<Int3, PointNode>();

            for (int x = -hexradius; x <= hexradius; x++)
            {
                for (int y = Mathf.Max(-hexradius, -x - hexradius); y <= Mathf.Min(hexradius, -x + hexradius); y++)
                {
                    int z = -x - y;
                    PointNode pn = new PointNode(active);
                    Int3 pos = new Int3(new Vector3(x, y, z));
                    pn.SetPosition(pos);
                    pn.Walkable = true;
                    pointNodes.Add(pn);
                    nodeDictionary[pos] = pn;
                }
            }

            nodes = pointNodes.ToArray();
            nodeCount = nodes.Length;

			List<Connection> connections = new List<Connection>();

            foreach (PointNode pn in nodes)
            {
                connections.Clear();
                foreach (Vector3i v in HexNeighbors.neighbours)
                {
                    Int3 offset = v.ToInt3();
                    Int3 pos = pn.position + offset;
                    if (nodeDictionary.ContainsKey(pos))
                    {
						connections.Add(new Connection { node = nodeDictionary[pos], cost = 1 });
                    }
                }

                pn.connections = connections.ToArray();
            }

			yield return new Progress(1.0f, "Done");
        }
    }
}