using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;

namespace AlexTarea2.Controllers
{

    public class SentimentalController : Controller
    {
        private readonly ILogger<SentimentalController> _logger;
        private readonly PredictionEnginePool<Sentimental.ModelInput, Sentimental.ModelOutput> _predictionEnginePool;

        public SentimentalController(ILogger<SentimentalController> logger, PredictionEnginePool<Sentimental.ModelInput, Sentimental.ModelOutput> predictionEnginePool)
        {
            _logger = logger;
            _predictionEnginePool = predictionEnginePool;
        }

        public IActionResult Index()
        
        {
            return View("Views/Sentimental/Index.cshtml");
        }

        [HttpPost]
        public IActionResult Comentario(string comentario)
        {
            var input = new Sentimental.ModelInput
            {
                Col0 = comentario
            };


           Sentimental.ModelOutput prediction = _predictionEnginePool.Predict(input);


            ViewBag.Resultado = prediction.PredictedLabel;

             return View("Views/Sentimental/Evaluacion.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error");
        }
    }
}