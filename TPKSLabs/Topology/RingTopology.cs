using System.Collections.Generic;
using TPKSLabs.Cluster;
using TPKSLabs.Helpers;

namespace TPKSLabs.Topology
{
    public class RingTopology
    {
        public List<AbstractCluster> Clusters { get; set; }
        public List<ConnectionItem> ConnectionRule { get; set; }
        public int[,] GlobalMatrix { get; set; }
        public int ClustersProcessorsCount { get; set; }

        #region .ctor

        public RingTopology(ClusterType clusterType, List<ConnectionItem> connectionRule, int iter=100)
        {
            //generate clusters
            ConnectionRule = connectionRule;
            Clusters = new List<AbstractCluster>();

            int processorsCount = 0,
                currentCluster = 0;
            while (processorsCount < iter)
            {
                AbstractCluster cluster = new AbstractCluster(currentCluster++, clusterType);
                Clusters.Add(cluster);
                processorsCount += cluster.NodesCount;
            }

            //create globalMatrix

            GlobalMatrix = new int[processorsCount, processorsCount];
            var clustersLocalMatrix = Clusters[0].LocalMatrix;
            ClustersProcessorsCount = Clusters[0].NodesCount;

            //set matrix diagonal
            for (int i = 0; i < clustersLocalMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < clustersLocalMatrix.GetLength(1); j++)
                {
                    for (int k = 0; k < Clusters.Count; k++)
                    {
                        if (clustersLocalMatrix[i, j] == 1)
                        {
                            GlobalMatrix[i + k * ClustersProcessorsCount, j + k * ClustersProcessorsCount] =
                                clustersLocalMatrix[i, j];
                        }
                    }
                }
            }

            //set matrix by rule
            //go through clusters from 2nd
            for (var i = 1; i < Clusters.Count; i++)
            {
                ConnectClustersByRule(i-1, i);
            }
            
            //make ring
            if (Clusters.Count>1)
            {
                ConnectClustersByRule(Clusters.Count - 1, 0); 
            }
            

        }
        #endregion

        #region private methods

        public void ConnectClustersByRule(int clusterFrom, int clusterTo)
        {
            foreach (var rule in ConnectionRule)
            {
                GlobalMatrix[rule.NodeFrom + clusterFrom * ClustersProcessorsCount, rule.NodeTo + clusterTo * ClustersProcessorsCount] = 1;
                GlobalMatrix[rule.NodeTo + clusterTo * ClustersProcessorsCount, rule.NodeFrom + clusterFrom * ClustersProcessorsCount] = 1;
            }
        }

        #endregion

    }


}