using System;
using System.Collections.Generic;

namespace AzureBlobStorage.Models
{
    /// <summary>
    /// Sensors Report Entity
    /// </summary>
    public class SensorReport
    {
        /// <summary>
        /// Type of Sensors
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Sensor Data
        /// </summary>
        public IEnumerable<int> Sensor { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }
    }
}
