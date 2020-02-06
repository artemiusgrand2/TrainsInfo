

namespace TrainsInfo.Common.BusinessObjects
{
    public struct ModelTrainPassOpz
    {
        public string TrainNumber { get; private set; }

        public string StationOperation { get; private set; }

        public string TimeOperation { get; private set; }

        public string Lateness { get; private set; }

        public string FullInfo { get; private set; }


        public ModelTrainPassOpz(string trainNumber, string stationOperation, string timeOperation, string lateness, string fullInfo)
        {
            TrainNumber = trainNumber;
            StationOperation = stationOperation;
            TimeOperation = timeOperation;
            Lateness = lateness;
            FullInfo = fullInfo;
        }
    }
}
