using EFCore.BulkExtensions;
using ExcelManagerIntegralCotton.Models;
using ExcelManagerIntegralCotton.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using System.Diagnostics;

namespace ExcelManagerIntegralCotton.Controllers
{
    public class HomeController : Controller
    {
        private readonly IntegralCottonDbContext _context;
        private readonly IRecapService _service;
        public List<HviRed> HviData = new();
        public List<ExcelDatum> HviDB = new();
        int tt1 = 0;
        int tt2 = 1;
        int tt3 = 0;

        public HomeController(IRecapService service, IntegralCottonDbContext context)
        {
           _service= service;
           _context= context;
        }

        [HttpPost]
        public IActionResult MostrarDatos([FromForm] IFormFile ArchivoExcel)
        {

            Stream stream = ArchivoExcel.OpenReadStream();

            IWorkbook MiExcel = null;

            if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
            {
                MiExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MiExcel = new HSSFWorkbook(stream);
            }

            ISheet HojaExcel = MiExcel.GetSheetAt(0);

            int cantidadFila = HojaExcel.LastRowNum;
            for (int i = 1; i < cantidadFila; i++)
            {
                IRow fila = HojaExcel.GetRow(i);
                HviData.Add(new HviRed
                {
                    UHML = fila.GetCell(0).ToString(),
                    UI = fila.GetCell(1).ToString(),
                    Strength = fila.GetCell(2).ToString(),
                    SFI = fila.GetCell(3).ToString(),
                    Mic = fila.GetCell(4).ToString(),
                    ColorGrade = fila.GetCell(5).ToString(),
                    TrashID = fila.GetCell(6).ToString(),
                });
            }
            return StatusCode(StatusCodes.Status200OK, HviData);
        }

        [HttpPost]
        public IActionResult EnviarDatos([FromForm] IFormFile ArchivoExcel)
        {

            Stream stream = ArchivoExcel.OpenReadStream();

            IWorkbook MiExcel = null;

            if (Path.GetExtension(ArchivoExcel.FileName) == ".xlsx")
            {
                MiExcel = new XSSFWorkbook(stream);
            }
            else
            {
                MiExcel = new HSSFWorkbook(stream);
            }

            ISheet HojaExcel = MiExcel.GetSheetAt(0);

            int cantidadFila = HojaExcel.LastRowNum;


            var allEntities = _context.ExcelData.ToList();
            _context.ExcelData.RemoveRange(allEntities);
            _context.SaveChanges();


            for (int i = 1; i < cantidadFila; i++)
            {
                IRow fila = HojaExcel.GetRow(i);

                HviDB.Add(new ExcelDatum
                {
                    Uhml = fila.GetCell(0).ToString(),
                    Ui = fila.GetCell(1).ToString(),
                    Strength = fila.GetCell(2).ToString(),
                    Sfi = fila.GetCell(3).ToString(),
                    Mic = fila.GetCell(4).ToString(),
                    Colorgrade = fila.GetCell(5).ToString(),
                    TrashId = fila.GetCell(6).ToString(),
                });
            }           
            
            _context.BulkInsert(HviDB);
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok" });
        }

        [HttpGet]
        public async Task<IActionResult> GetRecap()
        { 
            var list = await _service.GetRecapListAsync();

            AmountUHML(UH, list, uhmlTotal);
            AmountUI(UI, list, uiTotal);
            AmountSTR(STR, list, strTotal);
            AmountSFI(SFI, list, sfiTotal);
            AmountMIC(MIC, list, micTotal);
            AmountCLR(CLR, list, clrTotal);
            AmountTRSH(TRSH, list, trshTotal);
            BaseListFill();

            if (list != null)return View(baseList);
            else return BadRequest();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public List<decimal> UH = new()
        {
         0.8M, 0.85M, 0.86M, 0.89M, 0.9M, 0.92M, 0.93M, 0.95M, 0.96M, 0.98M, 0.99M, 1.01M,
         1.02M, 1.04M, 1.05M, 1.07M, 1.08M, 1.1M, 1.11M, 1.13M, 1.14M, 1.17M, 1.18M, 1.2M,
         1.21M, 1.23M, 1.24M, 1.26M, 1.27M, 1.29M, 1.3M, 1.32M, 1.33M, 1.35M, 1.36M, 99999
        };

        public List<decimal> UI = new()
        {
         70.0M, 70.9M, 71.0M, 71.9M, 72.0M, 72.9M, 73.0M, 73.9M, 74.0M, 74.9M, 75.0M, 75.9M,
         76.0M, 76.9M, 77.0M, 77.9M, 78.0M, 78.9M, 79.0M, 79.9M, 80.0M, 80.9M, 81.0M, 81.9M,
         82.0M, 82.9M, 83.0M, 99999
        };

        public List<decimal> STR = new()
        {
         17.0m, 17.9m, 18.0m, 18.9m, 19.0m, 19.9m, 20.0m, 20.9m, 21.0m, 21.9m, 22.0m, 22.9m,
         23.0m, 23.9m, 24.0m, 24.9m, 25.0m, 25.9m, 26.0m, 26.9m, 27.0m, 27.9m, 28.0m, 28.9m,
         29.0m, 29.9m, 30.0m, 99999
        };

        public List<decimal> SFI = new()
        {
         3.0M, 10.9M, 11.0M, 15.9M, 16.0M, 20.9M, 21.0M, 30,9999
        };

        public List<decimal> MIC = new()
        {
         2.0M, 2.1M, 2.2M, 2.3M, 2.4M, 2.5M, 2.6M, 2.7M, 2.8M, 2.9M, 3.0M, 3.1M, 3.2M, 3.3M, 3.4M,
         3.5M, 3.6M, 3.7M, 3.8M, 3.9M, 4.0M, 4.1M, 4.2M, 4.3M, 4.4M, 4.5M, 4.6M, 4.7M, 4.8M, 4.9M,
         5.0M, 5.1M, 5.2M, 5.3M, 5.4M, 5.5M, 5.6M, 5.7M
        };

        public List<string> CLR = new()
        {
         "11-1", "11-2", "11-3", "11-4", "12-1", "12-2", "12-3", "12-4", "13-1", "13-2", "13-3",
         "13-4", "14-1", "14-2", "14-3", "14-4", "21-1", "21-2", "21-3", "21-4", "22-1", "22-2",
         "22-3", "22-4", "23-1", "23-2", "23-3", "23-4", "24-1", "24-2", "24-3", "24-4", "31-1",
         "31-2", "31-3", "31-4", "32-1", "32-2", "32-3", "32-4", "33-1", "33-2", "33-3", "33-4",
         "34-1", "34-2", "34-3", "34-4", "41-1", "41-2", "41-3", "41-4", "42-1", "42-2", "42-3",
         "42-4", "43-1", "43-2", "43-3", "43-4", "44-1", "44-2", "44-3", "44-4", "51-1", "51-2",
         "51-3", "51-4", "52-1", "52-2", "52-3", "52-4", "53-1", "53-2", "53-3", "53-4", "54-1",
         "54-2", "54-3", "54-4", "61-1", "61-2", "61-3", "61-4", "62-1", "62-2", "62-3", "62-4",
         "63-1", "63-2", "63-3", "63-4"
        };

        public List<decimal> TRSH = new()
        {
         1, 2, 3, 4, 5, 6, 7, 8, 9
        };

        public List<BaseList> baseList = new();
        public List<int> uhmlTotal = new();
        public List<int> uiTotal = new();
        public List<int> strTotal = new();
        public List<int> sfiTotal = new();
        public List<int> micTotal = new();
        public List<int> clrTotal = new();
        public List<int> trshTotal = new();


        public void AmountUHML(List<decimal> list, List<RECAP> recap, List<int> total)
        {
            int pos1 = 0;
            int pos2 = 1;

            for (int i = 0; i < list.Count / 2; i++)
            {
                int result = recap.Where(x => x.Uhml >= list[pos1] && x.Uhml <= list[pos2]).Count();
                total.Add(result);                
                pos1 += 2;
                pos2 += 2;
            }
        }

        public void AmountUI(List<decimal> list, List<RECAP> recap,  List<int> total)
        {
            int pos1 = 0;
            int pos2 = 1;

            for (int i = 0; i < list.Count / 2; i++)
            {
                total.Add(recap.Where(x => x.Ui >= list[pos1] && x.Ui <= list[pos2]).Count());
                pos1 += 2;
                pos2 += 2;
            }
        }

        public void AmountSTR(List<decimal> list, List<RECAP> recap, List<int> total)
        {
            int pos1 = 0;
            int pos2 = 1;

            for (int i = 0; i < list.Count / 2; i++)
            {
                total.Add(recap.Where(x => x.Strength >= list[pos1] && x.Strength <= list[pos2]).Count());
                pos1 += 2;
                pos2 += 2;
            }
        }

        public void AmountSFI(List<decimal> list, List<RECAP> recap, List<int> total)
        {
            int pos1 = 0;
            int pos2 = 1;

            for (int i = 0; i < list.Count / 2; i++)
            {
                total.Add(recap.Where(x => x.Sfi >= list[pos1] && x.Sfi <= list[pos2]).Count());
                pos1 += 2;
                pos2 += 2;
            }
        }

        public void AmountMIC(List<decimal> list, List<RECAP> recap, List<int> total)
        {
            int pos1 = 0;

            for (int i = 0; i < list.Count; i++)
            {
                total.Add(recap.Where(x => x.Mic == list[pos1]).Count());
                pos1++;
            }
        }

        public void AmountCLR(List<string> list, List<RECAP> recap, List<int> total)
        {
            int pos1 = 0;

            for (int i = 0; i < list.Count; i++)
            {
                total.Add(recap.Where(x => x.Colorgrade == list[pos1]).Count());
                pos1++;
            }
        }

        public void AmountTRSH(List<decimal> list, List<RECAP> recap, List<int> total)
        {
            int pos1 = 0;

            for (int i = 0; i < list.Count; i++)
            {
                total.Add(recap.Where(x => x.TrashId == list[pos1]).Count());
                pos1++;
            }
        }

        

        public void BaseListFill()
        {
            for (int i = 0; i < CLR.Count; i++)
            {
                BaseList item = new BaseList();
                if (i < UH.Count/2) item.T1 = $"{UH[tt1]} - {UH[tt2]}";
                if (i < uhmlTotal.Count) item.D1 = uhmlTotal[tt3].ToString();

                if (i < UI.Count/2) item.T2 = $"{UI[tt1]} - {UI[tt2]}";
                if (i < uiTotal.Count) item.D2 = uiTotal[tt3].ToString();

                if (i < STR.Count/2) item.T3 = $"{STR[tt1]} - {STR[tt2]}";
                if (i < strTotal.Count) item.D3 = strTotal[tt3].ToString();

                if (i < SFI.Count /2) item.T4 = $"{SFI[tt1]} - {SFI[tt2]}";
                if (i < sfiTotal.Count) item.D4 = sfiTotal[tt3].ToString();

                if (i < MIC.Count) item.T5 = $"{MIC[tt3]}";
                if (i < micTotal.Count) item.D5 = micTotal[tt3].ToString();

                if (i < CLR.Count) item.T6 = $"{CLR[tt3]}";
                if (i < clrTotal.Count) item.D6 = clrTotal[tt3].ToString();

                if (i < TRSH.Count) item.T7 = $"{TRSH[tt3]}";
                if (i < trshTotal.Count) item.D7 = trshTotal[tt3].ToString();

                baseList.Add(item);

                tt1 += 2; 
                tt2 += 2;
                tt3++;
            }            
        }
    }
}