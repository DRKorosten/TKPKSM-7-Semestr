using System;
using System.Collections.Generic;
using TPKSLabs.Cluster;
using TPKSLabs.Helpers;

namespace TPKSLabs.Topology
{
    public class TreeTopology
    {
        public List<AbstractCluster> Clusters { get; set; }
        public List<ConnectionItem> ConnectionRuleCrossLevels { get; set; }
        public List<ConnectionItem> ConnectionRuleInnerLevel { get; set; }
        public List<ConnectionItem> ConnectionRuleInnerSide { get; set; }       
        public int[,] GlobalMatrix { get; set; }
        public int ClustersProcessorsCount { get; set; }
        public int processors { get; set; }

        #region .ctor

        public TreeTopology(ClusterType clusterType, List<ConnectionItem> connectionRuleCrossLevels,
    List<ConnectionItem> connectionRuleInnerLevel, List<ConnectionItem> connectionRuleInnerSide, int levels = 100)
        {
            //generate clusters
            ConnectionRuleCrossLevels = connectionRuleCrossLevels;
            ConnectionRuleInnerLevel = connectionRuleInnerLevel;
            ConnectionRuleInnerSide = connectionRuleInnerSide;
            Clusters = new List<AbstractCluster>();

            int processorsCount = 0,
                currentCluster = 0;
            int rank = Libraries.ClusterMatrices[clusterType].GetLength(0);
            var clusters = Math.Pow(2, levels) - 1;
            var clustersWithotLastLevel = Math.Pow(2, levels-1) - 1;
            while (processorsCount < rank* clusters)
            {
                AbstractCluster cluster = new AbstractCluster(currentCluster++, clusterType);
                Clusters.Add(cluster);
                processorsCount += cluster.NodesCount;
            }
            processors = processorsCount;
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
                //levels
               for(var i=0;i< clustersWithotLastLevel; i++)
                {
                    ConnectClustersByRule(2 * i+1, i, ConnectionRuleCrossLevels);
                    ConnectClustersByRule(2 * i+2, i, ConnectionRuleCrossLevels);
                }               
                //innerlevel
                var start = 0;
                var last = 1;
                for (var i = 1; i < levels; i++)
                {
                    start += (int)Math.Pow(2, i-1);
                    last += (int)Math.Pow(2, i);
                    for (var j= start; j < last-1; j++)
                    {
                        ConnectClustersByRule(j+1, j, ConnectionRuleInnerLevel);
                        //innerSide
                        if (j % 2 == 1)
                        {
                            ConnectClustersByRule(j + 1, j, ConnectionRuleInnerSide);
                        }
                    }                   
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