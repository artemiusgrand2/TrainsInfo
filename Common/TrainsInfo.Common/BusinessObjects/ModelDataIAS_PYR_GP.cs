using System;

namespace TrainsInfo.Common.BusinessObjects
{
    public class ModelDataIAS_PYR_GP
    {
        public string TrainId { get; set; }

        public int TrainNumber { get; set; }

        public string StationCode { get; set; }

        public string OperationCode { get; set; }

        public DateTime OperationTime { get; set; }

        public string Index1 { get; set; }

        public string Index2 { get; set; }

        public string Index3 { get; set; }

        public string DirectionFromStation { get; set; }

        public string DirectionToStation { get; set; }

        public string OperationTimeSTR
        {
            get
            {
                return string.Format("{0} {1}", OperationTime.ToShortDateString(), OperationTime.ToShortTimeString());
            }
        }
    }
}
