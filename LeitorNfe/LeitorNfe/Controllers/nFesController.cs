using Microsoft.AspNetCore.Mvc;
using System.Xml;

namespace LeitorNfe.Controllers
{
    public class nFesController : Controller
    {
        IWebHostEnvironment _appHostingEnviroment;
        public nFesController(IWebHostEnvironment appHostingEnviroment)
        {
            _appHostingEnviroment = appHostingEnviroment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> ReadFile(IFormFile arq)
        {
            if (arq is null || arq.FileName == "")
            {
                return View("Error");
            }
            else
            {
                string fileName = arq.FileName;
                //obtem o caminho da pasta wwwroot
                string path_WebRoot = _appHostingEnviroment.WebRootPath;
                //monta o caminho onde iremos salvar o arquivo
                string pathLocationFile = path_WebRoot + "\\Files";
                string finalDestinyLocationFile = pathLocationFile + "\\" + fileName;

                using (var reader = new StreamReader(arq.OpenReadStream()))
                {
                    var fileContent = reader.ReadToEnd();
                    var xml = new XmlDocument();
                    xml.LoadXml(fileContent);

                    //notaFiscal
                    ViewBag.numberNfe = xml.SelectSingleNode("//*[local-name()='nNF']").InnerText;
                    ViewBag.acessKey = xml.SelectSingleNode("//*[local-name()='chNFe']").InnerText;
                    ViewBag.dateEmission = xml.SelectSingleNode("//*[local-name()='dhEmi']").InnerText;

                    //emitente
                    ViewBag.name = xml.SelectNodes("//*[local-name()='xNome']")[0].InnerText;
                    ViewBag.cnpj = xml.SelectNodes("//*[local-name()='CNPJ']")[0].InnerText;

                    //pegando o cpf-cnpj independente da tag apenas por localizacao
                    XmlNode emit = xml.SelectSingleNode("//*[local-name()='emit']");
                    ViewBag.cnpj = emit.ChildNodes[0].InnerText;

                    ViewBag.lgr = xml.SelectNodes("//*[local-name()='xLgr']")[0].InnerText;
                    ViewBag.number = xml.SelectNodes("//*[local-name()='nro']")[0].InnerText;
                    ViewBag.hood = xml.SelectNodes("//*[local-name()='xBairro']")[0].InnerText;
                    ViewBag.city = xml.SelectNodes("//*[local-name()='xMun']")[0].InnerText;
                    ViewBag.uf = xml.SelectNodes("//*[local-name()='UF']")[0].InnerText;
                    ViewBag.cep = xml.SelectNodes("//*[local-name()='CEP']")[0].InnerText;

                    //destinatario
                    ViewBag.nameD = xml.SelectNodes("//*[local-name()='xNome']")[1].InnerText;
                    ViewBag.emailD = xml.SelectNodes("//*[local-name()='email']")[0].InnerText;

                    //pegando o cpf-cnpj independente da tag apenas por localizacao
                    XmlNode dest = xml.SelectSingleNode("//*[local-name()='dest']");
                    ViewBag.cnpjD = dest.ChildNodes[0].InnerText;

                    ViewBag.lgrD = xml.SelectNodes("//*[local-name()='xLgr']")[1].InnerText;
                    ViewBag.numberD = xml.SelectNodes("//*[local-name()='nro']")[1].InnerText;
                    ViewBag.hoodD = xml.SelectNodes("//*[local-name()='xBairro']")[1].InnerText;
                    ViewBag.cityD = xml.SelectNodes("//*[local-name()='xMun']")[1].InnerText;
                    ViewBag.ufD = xml.SelectNodes("//*[local-name()='UF']")[1].InnerText;
                    ViewBag.cepD = xml.SelectNodes("//*[local-name()='CEP']")[1].InnerText;

                    //produtos

                    List<string> prod = new List<string>();
                    var products = xml.SelectNodes("//*[local-name()='prod']").Count;
                    ViewBag.products = products.ToString();

                    for (int i = 0; i < products; i++)
                    {
                        //prod.Add(i + 1.ToString());
                        prod.Add(xml.SelectNodes("//*[local-name()='det'][@nItem]")[i].Attributes["nItem"].InnerText);
                        prod.Add(xml.SelectNodes("//*[local-name()='cProd']")[i].InnerText);
                        prod.Add(xml.SelectNodes("//*[local-name()='xProd']")[i].InnerText);
                        prod.Add(xml.SelectNodes("//*[local-name()='qCom']")[i].InnerText);
                        prod.Add(xml.SelectNodes("//*[local-name()='vUnCom']")[i].InnerText);
                        prod.Add(xml.SelectNodes("//*[local-name()='vProd']")[i].InnerText);
                    }

                    var listProd = new List<string>();
                    var listOfLists = new List<List<string>>();

                    for (int i = 0; i < products; i++)
                    {
                        for (int n = (i * 6); n < ((i * 6) + 6); n++)
                        {
                            listProd.Add(prod[n]);
                        }
                        listOfLists.Add(listProd);
                        listProd = new List<string>();
                    }
                    ViewBag.listOfLists = listOfLists;

                    //total
                    ViewBag.total = xml.SelectSingleNode("//*[local-name()='vNF']").InnerText;

                    return View("Create");

                }

                /*using (var stream = new FileStream(finalDestinyLocationFile, FileMode.Create))
                {
                    await arquive.CopyToAsync(stream);
                }*/


                //XmlDocument doc = new XmlDocument();
                //doc.Load(arq);




            }
        }
    }
}
