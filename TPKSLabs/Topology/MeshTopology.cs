using System.Collections.Generic;
using TPKSLabs.Cluster;
using TPKSLabs.Helpers;

namespace TPKSLabs.Topology
{
    public class MeshTopology
    {
        public List<AbstractCluster> Clusters { get; set; }
        public List<ConnectionItem> ConnectionRuleRowInner { get; set; }
        public List<ConnectionItem> ConnectionRuleRowOuter { get; set; }
        public List<ConnectionItem> ConnectionRuleColInner { get; set; }
        public List<ConnectionItem> ConnectionRuleColOuter { get; set; }
        public int[,] GlobalMatrix { get; set; }
        public int ClustersProcessorsCount { get; set; }

        #region .ctor

        public MeshTopology(ClusterType clusterType, List<ConnectionItem> connectionRuleRowInner,
    List<ConnectionItem> connectionRuleRowOuter, List<ConnectionItem> connectionRuleColInner, 
    List<ConnectionItem> connectionRuleColOuter, int iter = 100)
        {
            //generate clusters
            ConnectionRuleRowInner = connectionRuleRowInner;
            ConnectionRuleRowOuter = connectionRuleRowOuter;
            ConnectionRuleColInner = connectionRuleColInner;
            ConnectionRuleColOuter = connectionRuleColOuter;
            Clusters = new List<AbstractCluster>();

            int processorsCount = 0,
                currentCluster = 0;
            int rank = Libraries.ClusterMatrices[clusterType].GetLength(0);
            while (processorsCount < rank*iter*iter)
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

            if (Clusters.Count>1)
            {
                //Row
                for (int i = 1; i < iter+1; i++)
                {
                    for (int j = 1; j < iter; j++)
                    {
                        var item1 = iter * (i - 1) + (j - 1);
                        var item2 = iter * (i - 1) + (j);
                        ConnectClustersByRule((item2),(item1),ConnectionRuleRowInner);
                    }
                    var item3 = iter * (i - 1) + (iter - 1);
                    var item4 = iter * (i - 1) + (0);
                    ConnectClustersByRule((item3),(item4),ConnectionRuleRowOuter);
                }
                //Col
                for (int i = 1; i < iter; i++)
                {
                    for (int j = 1; j < iter+1; j++)
                    {                        
                            ConnectClustersByRule(iter*(i)+(j-1), iter * (i-1) + (j - 1), ConnectionRuleColInner); 
                    }
                    ConnectClustersByRule(iter * (iter - 1) + (i - 1), (i - 1), ConnectionRuleColOuter);
                }
            }
            

        }
        #endregion

        #region private methods

        public void ConnectClustersByRule(int clusterFrom, int clusterTo, List<ConnectionItem> connectionRule)
        {
            foreach (var rule in connectionRule)
            {
                GlobalMatrix[rule.NodeFrom + clusterFrom * ClustersProcessorsCount, rule.NodeTo + clusterTo * ClustersProcessorsCount] = 1;
                GlobalMatrix[rule.NodeTo + clusterTo * ClustersProcessorsCount, rule.NodeFrom + clusterFrom * ClustersProcessorsCount] = 1;
            }
        }

        #endregion

    }


}