using System.Collections.Generic;
using TPKSLabs.Cluster;
using TPKSLabs.Helpers;

namespace TPKSLabs.Topology
{
    public class RingTopology
    {
        public List<AbstractCluster> Clusters { get; set; }
        public List<ConnectionItem> ConnectionRule { get; set; }
        public byte[,] GlobalMatrix { get; set; }

        #region .ctor

        public RingTopology(ClusterType clusterType, List<ConnectionItem> connectionRule,  int minNumberOfProcessors = 100)
        {
            //generate clusters
            ConnectionRule = connectionRule;
            Clusters = new List<AbstractCluster>();

            int processorsCount = 0,
                currentCluster = 0;
            while (processorsCount < minNumberOfProcessors)
            {
                AbstractCluster cluster = new AbstractCluster(currentCluster++, clusterType);
                Clusters.Add(cluster);
                processorsCount += cluster.NodesCount;
            }

            //create globalMatrix

            GlobalMatrix = new byte[processorsCount, processorsCount];
            var clustersLocalMatrix = Clusters[0].LocalMatrix;
            var clustersProcessorsCount = Clusters[0].NodesCount;

            //set matrix diagonal
            for (int i = 0; i < clustersLocalMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < clustersLocalMatrix.GetLength(1); j++)
                {
                    for (int k = 0; k < Clusters.Count; k++)
                    {
                        if (clustersLocalMatrix[i, j] == 1)
                        {
                            GlobalMatrix[i + k * clustersProcessorsCount, j + k * clustersProcessorsCount] =
                                clustersLocalMatrix[i, j];
                        }
                    }
                }
            }
        }
        #endregion
    }
}