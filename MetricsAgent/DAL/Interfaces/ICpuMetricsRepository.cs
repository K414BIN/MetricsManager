using System;
using System.Collections.Generic;
using System.Data.SQLite;
using MainLibrary;
using MetricsAgent.Controllers;
using MetricsAgent.DAL.Models;

namespace MetricsAgent.DAL.Interfaces
{

    // маркировочный интерфейс
    // необходим, чтобы проверить работу репозитория на тесте-заглушке
    public interface ICpuMetricsRepository : IRepository<CpuMetric>
    {

    }
}