using AzureBlobStorage.Models;
using System;
using System.Collections.Generic;

namespace AzureBlobStorage.Interface
{
    public interface IWeatherReport
    {
        /// <summary>
        /// Getting Data for Sesnor Type
        /// </summary>
        SensorReport GetSensorReport(string deviceId, string sensorType, DateTime dateTime);

        /// <summary>
        /// Getting full Report
        /// </summary>
        IEnumerable<SensorReport> GetAllSensorsReport(string deviceId, DateTime dateTime);
    }
}
