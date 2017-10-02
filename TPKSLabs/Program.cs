using System;
using TPKSLabs.Helpers;
using TPKSLabs.Topology;

namespace TPKSLabs
{
    class Program
    {
        static void Main(string[] args)
        {
            RingTopology ring = new RingTopology(ClusterType.DoubleStar, Libraries.TopologyRule[ClusterType.DoubleStar], 24);

            MatrixOperations.OutPutMatrix(ring.GlobalMatrix);

            Console.ReadKey();
        }
    }
}
