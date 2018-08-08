using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Formatting;
using System.Net;
using System.IO;
using DataAccess;
using Entidade;

namespace MusicBook.Controllers
{
    public class HomeController : Controller
    {
        const string letra = "z";

        public ActionResult Index()
        {
            ViewBag.Title = "Livro Book";

            return View();
        }

        [HttpPost]
        public ActionResult Enviar(string url)
        {
            WebRequest request = WebRequest.Create($"https://www.vagalume.com.br/browse/{letra}.html");

            var response = request.GetResponse();
            
            Stream dataStream = response.GetResponseStream();
            
            StreamReader reader = new StreamReader(dataStream);
            
            string responseFromServer = reader.ReadToEnd();
            

            
            reader.Close();
            response.Close();

            return Json(new {
                html = responseFromServer
                

            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SalvarLista(List<string> artistas)
        {
            var banco = new DAArtista();

            List<List<string>> aux = new  List<List<string>>();
            string listaConcatenada = "";
            var quantidade = 1000;
            var quantidadeInserida = quantidade * aux.Count;

            artistas = artistas.Where(x => x.First().ToString().ToUpper() == letra.ToUpper()).ToList();

            //artistas.RemoveRange(0, artistas.IndexOf("Thania Milagre"));

            while (quantidade == 1000)
            {
                if (quantidadeInserida + quantidade > artistas.Count)
                {
                    quantidade = artistas.Count - quantidadeInserida;
                }

                aux.Add(artistas.GetRange(quantidadeInserida ,quantidade));

                quantidadeInserida = aux.Count * quantidade;
                                
            }
            
            aux.ForEach(x => {
                
                listaConcatenada = string.Join(",", x);

                banco.InserirArtistaArray(listaConcatenada);

            });
            
            return Json(new
            {
                success = true


            }, JsonRequestBehavior.AllowGet);
        }
        
    }
}
