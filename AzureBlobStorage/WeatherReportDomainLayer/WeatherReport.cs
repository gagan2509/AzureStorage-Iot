using AzureBlobStorage.Interface;
using AzureBlobStorage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace WeatherReportDomainLayer
{
    public class WeatherReport : IWeatherReport
    {
        private IBlobOperations _blobOperation;
        string blobUrl = ConfigurationManager.AppSettings["blobUrl"].ToString();
        string sasToken = ConfigurationManager.AppSettings["sasToken"].ToString();

        public WeatherReport(IBlobOperations blobOperation)
        {
            _blobOperation = blobOperation;
        }
        /// <summary>
        /// Getting the data for all the Sensons
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="dateTime"></param>
        /// <returns> Senson Report</returns>
        public IEnumerable<SensorReport> GetAllSensorsReport(string deviceId, DateTime dateTime)
        {
            var sensors = Enum.GetNames(typeof(SensorType));
            List<SensorReport> sensorsReport = new List<SensorReport>();
            foreach (var sensor in sensors)
            {
                sensorsReport.Add(GetSensorReport(deviceId, sensor.ToLower(), dateTime));
            }

            return sensorsReport;
        }
        /// <summary>
        /// Getting the data for a particular type of sensor
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="sensorType"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public SensorReport GetSensorReport(string deviceId, string sensorType, DateTime dateTime)
        {
            return GetSensorData(deviceId, sensorType, dateTime);
        }
        /// <summary>
        ///  Getting the data for a particular type of sensor and returning to Method calls
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="sensorType"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private SensorReport GetSensorData(string deviceId, string sensorType, DateTime dateTime)
        {
            try
            {
                var Url = string.Format("{0}/{1}/{2}/{3}.csv?{4}", blobUrl, deviceId, sensorType, dateTime.ToString("yyyy-MM-dd"), sasToken);
                var getData = _blobOperation.GetBlobData(Url);
                var rows = getData.Split('\n');
                var recordList = new List<DataFormat>();
                foreach (var row in rows)
                {
                    if (!string.IsNullOrEmpty(row))
                    {
                        var columnItem = row.Split(',');
                        recordList.Add(new DataFormat() { Date = Convert.ToDateTime(columnItem[0].Split(';').FirstOrDefault()), SensorData = Convert.ToInt32(columnItem[1]), Unit = Convert.ToInt32(columnItem[0].LastOrDefault()) });
                    }
                }
                var records = (from rc in recordList
                               where rc.Date.ToString("yyyy-MM-dd") == dateTime.Date.ToString("yyyy-MM-dd")
                               select rc.SensorData).ToList();
                return new SensorReport() { Type = sensorType, Sensor = records };
            }
            catch (Exception)
            {
                return new SensorReport() { Type = sensorType, Sensor = null };
            }
        }
    }
}
