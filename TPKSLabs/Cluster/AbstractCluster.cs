using TPKSLabs.Helpers;

namespace TPKSLabs.Cluster
{
    public class AbstractCluster
    {
        public int ClusterId { get; set; }
        public ClusterType ClusterType { get; set; }
        public byte[,] LocalMatrix { get; set; }
        public int NodesCount => LocalMatrix.GetLength(0);

        #region .ctor

        public AbstractCluster(int clusterId, ClusterType clusterType)
        {
            ClusterId = clusterId;
            ClusterType = clusterType;
            LocalMatrix = Libraries.ClusterMatrices[ClusterType];
        }

        #endregion
    }
}