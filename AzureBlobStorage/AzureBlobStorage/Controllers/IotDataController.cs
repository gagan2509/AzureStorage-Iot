using AzureBlobStorage.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AzureBlobStorage.Controllers
{
    public class IotDataController : ApiController
    {
        private IWeatherReport _weatherReport;

        public IotDataController(IWeatherReport weatherReport)
        {
            _weatherReport = weatherReport;
        }

        /// <summary>
        /// Getting Sensor Report on sensorType
        /// </summary>
        [HttpGet]
        public IHttpActionResult GetReportforSensor(string deviceId, DateTime date, string sensorType)
        {
            if (string.IsNullOrEmpty(deviceId) || date == null || string.IsNullOrEmpty(sensorType))
            {
                return BadRequest("InValid Inputs deviceId,date,sensorType");
            }

            var getData = _weatherReport.GetSensorReport(deviceId, sensorType, date);
            if (getData.Sensor == null)
            {
                return NotFound();
            }
            return Ok(getData);
        }

        /// <summary>
        /// Get Complete Report
        /// </summary>
        [HttpGet]
        public IHttpActionResult GetAllSensorReport(string deviceId, DateTime date)
        {
            if (string.IsNullOrEmpty(deviceId) || date == null)
            {
                return BadRequest("Invalid Inputs deviceId, date");
            }
            var getData = _weatherReport.GetAllSensorsReport(deviceId, date);
            if (getData == null)
            {
                return NotFound();
            }
            return Ok(getData);
        }

    }
}

