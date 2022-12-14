using AutoMapper;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using WebApplication3.Model;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadController : ControllerBase
    {
        private readonly FirstExamContext _context;
        private IMapper _mapper;
        public ReadController(
            FirstExamContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        [HttpGet]
        [Route("Items")]
        public IActionResult GetData() => Ok(_mapper.Map<List<CsvViewVM>>(_context.CsvViews.ToList()));
        

        [HttpGet]
        [Route("Convert")]
        public IActionResult ConvetData()
        {
            //var data = _context.Categories.
            //     SelectMany(x => x.Subcategories).
            //     SelectMany(c => c.Items).Include(x => x.Subcategory).ThenInclude(x => x.Category);
            //var result = _mapper.Map<List<ItemVM>>(data);
            var data = _context.CsvViews.ToList();
            var result = _mapper.Map<List<CsvViewVM>>(data);
            var csvpath = Path.Combine(Environment.CurrentDirectory, $"Items-{DateTime.Now.ToFileTime()}.csv");
            using(var streamWriter = new StreamWriter(csvpath))
            {
                using(var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(result);
                }
            }
            return Ok();
        }


    }
}
