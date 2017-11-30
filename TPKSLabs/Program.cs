using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TPKSLabs.Helpers;
using TPKSLabs.Topology;

namespace TPKSLabs
{
    class Program
    {

        public static Dictionary<int, Action> LabDictionary = new Dictionary<int, Action>{
            {1, RunLab1},
            {2, RunLab2},
            {3, RunLab3},
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Enter lab number: ");
            int labRank;
            labRank = Convert.ToInt32(Console.ReadLine());

            LabDictionary[labRank]();

        }

        #region private calculation methods

        public static int CalculateDiameter(int[,] matrix)
        {
            return matrix.Cast<int>().Max();
        }

        public static double CalculateAverageDiameter(int[,] matrix)
        {
            return (double)matrix.Cast<int>().Sum() / (matrix.GetLength(0) * (matrix.GetLength(0) - 1));
        }


        public static int CalculateStage(int[,] matrix)
        {
            int maxResult = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var row = matrix.GetRow(i);
                int numberOfEdges = row.Count(x => x == 1);
                maxResult = Math.Max(maxResult, numberOfEdges);
            }
            return maxResult;
        }

        public static double CalculateCost(double D, int n, int S)
        {
            return D * n * S;
            //return matrix.Cast<int>().Count(x => x == 1);
        }

        public static double CalculateTopologyGraph(double averageDiametr, int stage)
        {
            return 2 * averageDiametr / stage;
        }
        #endregion

        #region labMethods

        public static void RunLab1()
        {
            for (int i = 1; i < 16; i++)
            {
                RingTopology ring = new RingTopology(ClusterType.Cluster_Lab1,
                    Libraries.Lab1_Rule[ClusterType.Cluster_Lab1],i*9);

                var topologyMatrix = ring.GlobalMatrix;
                //DextraTest
                DextraHelper dextra = new DextraHelper(topologyMatrix);
                var distanceMatrix = dextra.CalculateShortestDistances();

                //Console.WriteLine("shortest distance matrix:");

                //MatrixOperations.OutPutMatrix(distanceMatrix);
                double diametr = CalculateDiameter(distanceMatrix);
                double averageDiametr = CalculateAverageDiameter(distanceMatrix);

                int stage = CalculateStage(topologyMatrix);
                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("<-------------------------------------------------->");
                Console.WriteLine("Итерация " + i);
                Console.WriteLine("Количество процессоров " + i*9);
                Console.WriteLine("Диаметр: " + CalculateDiameter(distanceMatrix));
                Console.WriteLine("Средний диаметр: " + averageDiametr);
                Console.WriteLine("Степень = " + stage);
                Console.WriteLine("Цена = " + CalculateCost(diametr, i * 9, stage));
                Console.WriteLine("Торологический трафик = " + CalculateTopologyGraph(averageDiametr, stage));
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        public static void RunLab2()
        {
            for (int i = 1; i < 10; i++)
            {
                MeshTopology meshTopology = new MeshTopology(ClusterType.Cluster_Lab2, Libraries.Lab2_Row_Inner_Rule[ClusterType.Cluster_Lab2],
                    Libraries.Lab2_Row_Outer_Rule[ClusterType.Cluster_Lab2], 
                    Libraries.Lab2_Col_Inner_Rule[ClusterType.Cluster_Lab2],
                    Libraries.Lab2_Col_Outer_Rule[ClusterType.Cluster_Lab2], i);

                var topologyMatrix = meshTopology.GlobalMatrix;
                //DextraTest
                DextraHelper dextra = new DextraHelper(topologyMatrix);
                var distanceMatrix = dextra.CalculateShortestDistances();

                //Console.WriteLine("shortest distance matrix:");

                //MatrixOperations.OutPutMatrix(distanceMatrix);
                double diametr = CalculateDiameter(distanceMatrix);
                double averageDiametr = CalculateAverageDiameter(distanceMatrix);

                int stage = CalculateStage(topologyMatrix);

                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("<-------------------------------------------------->");
                Console.WriteLine("Итерация " + i);
                Console.WriteLine("Количество процессоров " + i *i* 6);
                Console.WriteLine("Диаметр: " + CalculateDiameter(distanceMatrix));
                Console.WriteLine("Средний диаметр: " + averageDiametr);
                Console.WriteLine("Степень = " + stage);
                Console.WriteLine("Цена = " + CalculateCost(diametr, i *i* 6, stage));
                Console.WriteLine("Торологический трафик = " + CalculateTopologyGraph(averageDiametr, stage));
                Console.WriteLine();
            }



            Console.ReadKey();
        }
        public static void RunLab3()
        {
            for (int i = 1; i < 10; i++)
            {
                 TreeTopology treeTopology = new TreeTopology(ClusterType.Cluster_Lab3, Libraries.Lab3_Cross_Levels_Rule[ClusterType.Cluster_Lab3],
                    Libraries.Lab3_Inner_Level_Rule[ClusterType.Cluster_Lab3],
                    Libraries.Lab3_Inner_Side_Rule[ClusterType.Cluster_Lab3], i);

                var topologyMatrix = treeTopology.GlobalMatrix;
                //DextraTest
                DextraHelper dextra = new DextraHelper(topologyMatrix);
                var distanceMatrix = dextra.CalculateShortestDistances();
                double diametr = CalculateDiameter(distanceMatrix);
                double averageDiametr = CalculateAverageDiameter(distanceMatrix);
                int stage = CalculateStage(topologyMatrix);

                Console.OutputEncoding = Encoding.UTF8;
                Console.WriteLine("<-------------------------------------------------->");
                Console.WriteLine("Итерация " + i);
                Console.WriteLine("Количество процессоров " + treeTopology.processors);
                Console.WriteLine("Диаметр: " + CalculateDiameter(distanceMatrix));
                Console.WriteLine("Средний диаметр: " + averageDiametr);
                Console.WriteLine("Степень = " + stage);
                Console.WriteLine("Цена = " + CalculateCost(diametr, treeTopology.processors, stage));
                Console.WriteLine("Торологический трафик = " + CalculateTopologyGraph(averageDiametr, stage));
                Console.WriteLine();
            }



            Console.ReadKey();
        }

        #endregion


    }
}
