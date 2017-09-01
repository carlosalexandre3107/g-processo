using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GProcessos.Dominio;
using GProcessos.Serializado;


namespace GProcessos.Controllers
{
    public class GProcessoController : Controller
    {


        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EstruturaProcesso()
        {
            return Json(new Processo());
        }

        [HttpPost]
        public ActionResult SalvarProcesso(Processo _processo)
        {
            Arquivo _arquivo = new Arquivo();

            List<Processo> _processos = _arquivo.DeserializarProcessos();
            _processos.Add(_processo);

            _arquivo.SerializarProcessos(_processos);

            return Json(_processos);
        }

        [HttpPost]
        public ActionResult ListarProcessos()
        {
            return Json(new Arquivo().DeserializarProcessos());
        }




    }
}