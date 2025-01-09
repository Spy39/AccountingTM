using Accounting.Data;
using AccountingTM.Domain.Models.Tables;
using AccountingTM.Dto.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AccountingTM.Controllers
{
    [Authorize]
    //Информация о ТС
    public class TechnicalEquipmentInfoController : Controller
    {
        private readonly DataContext _context;

        public TechnicalEquipmentInfoController(DataContext context)
        {
            _context = context;
        }


        //Характеристики технического средства
        [HttpGet]
        public IActionResult GetAllCharacteristic([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Characteristic> query = _context.Characteristics.Include(x => x.Unit).Include(x => x.Indicator);

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Characteristic>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateCharacteristic([FromBody] Characteristic input)
        {
            _context.Characteristics.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteCharacteristic(int id)
        {
            var entity = _context.Characteristics.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Characteristics.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Консервация
        [HttpGet]
        public IActionResult GetAllConservation([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Conservation> query = _context.Conservations.Include(x => x.Employee);

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Conservation>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateConservation([FromBody] Conservation input)
        {
            input.Date += TimeSpan.FromHours(3);
            _context.Conservations.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteConservation(int id)
        {
            var entity = _context.Conservations.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Conservations.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Прием и передача изделия
        [HttpGet]
        public IActionResult GetAllReceptionAndTransmission([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<ReceptionAndTransmission> query = _context.ReceptionAndTransmissions;

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<ReceptionAndTransmission>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateReceptionAndTransmission([FromBody] ReceptionAndTransmission input)
        {
            input.Date += TimeSpan.FromHours(3);
            _context.ReceptionAndTransmissions.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteReceptionAndTransmission(int id)
        {
            var entity = _context.ReceptionAndTransmissions.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.ReceptionAndTransmissions.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Ремонт
        [HttpGet]
        public IActionResult GetAllRepair([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Repair> query = _context.Repairs;

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Repair>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateRepair([FromBody] Repair input)
        {
            input.Date += TimeSpan.FromHours(3);
            _context.Repairs.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteRepair(int id)
        {
            var entity = _context.Repairs.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Repairs.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }


        //Хранение
        [HttpGet]
        public IActionResult GetAllStorage([FromQuery] SearchPagedRequestDto input)
        {
            IQueryable<Storage> query = _context.Storages;

            var entities = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();

            return Ok(new PagedResultDto<Storage>(query.Count(), entities));
        }

        [HttpPost]
        public IActionResult CreateStorage([FromBody] Storage input)
        {
            input.Acceptance += TimeSpan.FromHours(3);
            input.Removal += TimeSpan.FromHours(3);
            _context.Storages.Add(input);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteStorage(int id)
        {
            var entity = _context.Storages.Find(id);
            if (entity == null)
            {
                return NotFound();
            }

            _context.Storages.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
    }
}