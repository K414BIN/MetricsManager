using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class HomeController : ControllerBase
    {
        private readonly INotifierMediatorService _notifierMediatorService;

        public HomeController(INotifierMediatorService notifierMediatorService)
        {
            _notifierMediatorService = notifierMediatorService;
        }

        [HttpGet("")]
        public ActionResult<string> NotifyAll()
        {
            _notifierMediatorService.Notify();
            return "Completed";
        }
    }

    //public class HomeController : ControllerBase
    //{ 
    //    private readonly IEnumerable<INotifier> _notifiers;

    //    public HomeController(IEnumerable<INotifier> notifiers)
    //    {
    //        _notifiers = notifiers;
    //    }

    //    [HttpGet("")]
    //    public ActionResult<string> NotifyAll()
    //    {
    //        _notifiers.ToList().ForEach(x => x.Notify());
    //        return "Completed";
    //    }
    }




