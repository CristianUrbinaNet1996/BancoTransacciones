using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text;



using WebAppBcuentas.Areas.EstadoCuentas.Interfaces;
using WebAppBcuentas.Areas.EstadoCuentas.Models;
using WebAppBcuentas.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using iTextSharp.tool.xml.css;
using ClientDependency.Core;
using iTextSharp.tool.xml;
using iTextSharp.tool.xml.pipeline.html;
using iTextSharp.tool.xml.html;
using iTextSharp.tool.xml.pipeline.css;
using iTextSharp.tool.xml.pipeline.end;
using iTextSharp.tool.xml.parser;


namespace WebAppBcuentas.Areas.EstadoCuentas.Controllers
{
    [Area("EstadoCuenta")]
    public class EstadoCuentaController : Controller
    {
        private readonly ICompositeViewEngine _viewEnginee;
        private readonly IEstadoCuenta _estadoCuenta;
        private readonly IRazorViewEngine _viewEngine;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITempDataProvider _empDataProvider;
        public EstadoCuentaController(IEstadoCuenta estadoCuenta, IRazorViewEngine viewEngine,ICompositeViewEngine compositeViewEngine,
        ITempDataDictionaryFactory tempDataDictionaryFactory,ITempDataProvider tempDataProvider,
        IServiceProvider serviceProvider)
        {
            _estadoCuenta = estadoCuenta;
            _viewEngine = viewEngine;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
            _serviceProvider = serviceProvider;
            _empDataProvider = tempDataProvider;
            _viewEnginee = compositeViewEngine ?? throw new ArgumentNullException(nameof(compositeViewEngine));

        }


        public async Task<IActionResult> Index()
        {
            var EstadoCuenta = await _estadoCuenta.GetEstadoCuentaByTarjeta(1);
            return View(EstadoCuenta);
        }


        public async Task<IActionResult> AgregarTransaccion() {  
        
        
            return View();  
        
        }  

        [HttpGet("GetEstadoCuenta/{idTarjeta}")]
        public async Task<IActionResult> GetEstadoCuenta(int idTarjeta)
        {
            var EstadoCuenta = await _estadoCuenta.GetEstadoCuentaByTarjeta(idTarjeta);
            return View(EstadoCuenta);
        }


        public async Task<ActionResult> ExportEstadoDeCuentaToPdf(int idTarjeta)
        {
            var modelo = await _estadoCuenta.GetEstadoCuentaByTarjeta(idTarjeta);

            // Renderizar la vista a string
            string htmlString = await RenderRazorViewToStringAsync("PDFEstadoCuenta", modelo);

            using (MemoryStream stream = new MemoryStream())
            {
                // Crear documento PDF
                using (Document document = new Document(PageSize.A4, 50, 50, 25, 25))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Usar XMLWorkerHelper para convertir HTML a PDF
                    using (var stringReader = new StringReader(htmlString))
                    {
                        var cssResolver = new StyleAttrCSSResolver();
                        ICssFile cssFile = XMLWorkerHelper.GetCSS(new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/css/Styles.css"), FileMode.Open)); // Agregar el archivo CSS
                        cssResolver.AddCss(cssFile);

                        var htmlContext = new HtmlPipelineContext(null);
                        htmlContext.SetTagFactory(Tags.GetHtmlTagProcessorFactory());

                        var pipeline = new CssResolverPipeline(cssResolver, new HtmlPipeline(htmlContext, new PdfWriterPipeline(document, writer)));

                        var worker = new XMLWorker(pipeline, true);
                        var parser = new XMLParser(worker);

                        parser.Parse(stringReader);
                    }

                    document.Close();
                }

                // Devolver el PDF como un archivo descargable
                return File(stream.ToArray(), "application/pdf", "EstadoDeCuenta.pdf");
            }
        }
        private async Task<string> RenderRazorViewToStringAsync(string viewName, object model)
        {
            if (_serviceProvider == null) throw new ArgumentNullException(nameof(_serviceProvider));

            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var routeData = new Microsoft.AspNetCore.Routing.RouteData();
            routeData.Values["area"] = "EstadoCuenta";  // Añade el área si es necesario

            var actionContext = new ActionContext(httpContext, routeData, new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _viewEngine.GetView(null, $"~/Areas/EstadoCuenta/Views/EstadoCuenta/{viewName}.cshtml", true);
                if (!viewResult.Success)
                {
                    viewResult = _viewEngine.FindView(actionContext, viewName, true);
                }

                if (!viewResult.Success)
                {
                    throw new ArgumentNullException($"La vista '{viewName}' no se encontró. Buscado en: {string.Join(", ", viewResult.SearchedLocations)}");
                }

                var viewDictionary = new ViewDataDictionary(
                    new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(),
                    new ModelStateDictionary())
                {
                    Model = model ?? throw new ArgumentNullException(nameof(model)) // Asegurarse de que el modelo no sea null
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(httpContext, _empDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }

    }
}
