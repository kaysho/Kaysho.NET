﻿using System;
using System.Collections.Generic;

namespace Kaysho.NET.Core.HealthChecks
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }

        public IEnumerable<HealthCheck> Checks { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
