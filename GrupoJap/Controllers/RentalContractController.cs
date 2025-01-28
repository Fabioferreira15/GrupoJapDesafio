using GrupoJap.Data;
using GrupoJap.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GrupoJap.Controllers
{
    public class RentalContractController : Controller
    {

        private readonly ApplicationDbContext _db;

        public RentalContractController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<RentalContract> rentalContracts = _db.RentalContracts.Include(c => c.Client).Include(v => v.Vehicle).ToList();
            return View(rentalContracts);
        }



        public async Task<IActionResult> Create()
        {
            ViewData["ClientId"] = new SelectList(_db.Clients, "ClientId", "Name");
            ViewData["VehicleId"] = new SelectList(
                _db.Vehicles.Where(v => v.Status == "Disponivel"),
                "VehicleId",
                "LicensePlate"
            );
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RentalContract rentalContract)
        {


            try
            {
                var existingRentalContract = _db.RentalContracts.FirstOrDefault(rc => rc.ClientId == rentalContract.ClientId && rc.VehicleId == rentalContract.VehicleId);

                if (existingRentalContract != null)
                {
                    ModelState.AddModelError("ClientId", "Já existe um contrato de aluguer para este cliente e viatura.");
                    ViewData["ClientId"] = new SelectList(_db.Clients, "ClientId", "Name");
                    ViewData["VehicleId"] = new SelectList(
                        _db.Vehicles.Where(v => v.Status == "Disponivel"),
                        "VehicleId",
                        "LicensePlate"
                    );
                    return View(rentalContract);
                }

                if (ModelState.IsValid)
                {
                    _db.RentalContracts.Add(rentalContract);

                    var vehicle = _db.Vehicles.FirstOrDefault(v => v.VehicleId == rentalContract.VehicleId);

                    if (vehicle != null)
                    {
                        vehicle.Status = "Alugado";
                    }

                    await _db.SaveChangesAsync();

                    TempData["Message"] = "Contrato de aluguer registado com sucesso!";
                    return RedirectToAction("Index");
                }

                ViewData["ClientId"] = new SelectList(_db.Clients, "ClientId", "Name");
                ViewData["VehicleId"] = new SelectList(
                    _db.Vehicles.Where(v => v.Status == "Disponivel"),
                    "VehicleId",
                    "LicensePlate"
                );
                return View(rentalContract);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao registar o contrato de aluguer");
                ViewData["ClientId"] = new SelectList(_db.Clients, "ClientId", "Name");
                ViewData["VehicleId"] = new SelectList(
                    _db.Vehicles.Where(v => v.Status == "Disponivel"),
                    "VehicleId",
                    "LicensePlate"
                );
                return View(rentalContract);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var rentalContract = await _db.RentalContracts.FindAsync(id);
                if (rentalContract == null)
                {
                    ModelState.AddModelError("", "Contrato de aluguer não encontrado");
                    return RedirectToAction("Index");
                }

                var vehicle = _db.Vehicles.FirstOrDefault(v => v.VehicleId == rentalContract.VehicleId);

                if (vehicle != null)
                {
                    vehicle.Status = "Disponivel";
                }

                _db.RentalContracts.Remove(rentalContract);
                await _db.SaveChangesAsync();

                TempData["Message"] = "Contrato de aluguer eliminado com sucesso!";
                return RedirectToAction("Index");

            }
            catch
            {
                ModelState.AddModelError("", "Ocorreu um erro ao eliminar o contrato de aluguer");
                return RedirectToAction("Index");
            }

        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var rentalContract = await _db.RentalContracts.FindAsync(id);
            if (rentalContract == null)
            {
                return NotFound();
            }




            ViewData["ClientId"] = new SelectList(_db.Clients, "ClientId", "Name");
            ViewData["VehicleId"] = new SelectList(
        _db.Vehicles.Where(v => v.Status == "Disponivel" || v.VehicleId == rentalContract.VehicleId),
        "VehicleId",
        "LicensePlate",
        rentalContract.VehicleId
    );
            return View(rentalContract);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RentalContract rentalContract)
        {
            try
            {
                var existingRentalContract = await _db.RentalContracts.FindAsync(rentalContract.RentalContractId);

                if (existingRentalContract == null)
                {
                    ModelState.AddModelError("", "Contrato de aluguer não encontrado");
                    ViewData["ClientId"] = new SelectList(_db.Clients, "ClientId", "Name");
                    ViewData["VehicleId"] = new SelectList(
                        _db.Vehicles.Where(v => v.Status == "Disponivel" || v.VehicleId == rentalContract.VehicleId),
                        "VehicleId",
                        "LicensePlate",
                        rentalContract.VehicleId
    );
                    return View(rentalContract);
                }


                var originalVehicle = await _db.Vehicles.FindAsync(existingRentalContract.VehicleId);

                if (originalVehicle != null)
                {
                    originalVehicle.Status = "Disponivel";
                }

                var vehicle = _db.Vehicles.FirstOrDefault(v => v.VehicleId == rentalContract.VehicleId);

                if (vehicle != null)
                {
                    vehicle.Status = "Alugado";
                }
                else
                {
                    ModelState.AddModelError("", "Viatura não encontrada");
                    ViewData["ClientId"] = new SelectList(_db.Clients, "ClientId", "Name");
                    ViewData["VehicleId"] = new SelectList(
                    _db.Vehicles.Where(v => v.Status == "Disponivel" || v.VehicleId == rentalContract.VehicleId),
                    "VehicleId",
                    "LicensePlate",
                    rentalContract.VehicleId
    );
                    return View(rentalContract);
                }

                if (ModelState.IsValid)
                {
                    existingRentalContract.ClientId = rentalContract.ClientId;
                    existingRentalContract.VehicleId = rentalContract.VehicleId;
                    existingRentalContract.StartDate = rentalContract.StartDate;
                    existingRentalContract.EndDate = rentalContract.EndDate;
                    existingRentalContract.InitialKilometers = rentalContract.InitialKilometers;
                    await _db.SaveChangesAsync();
                    TempData["Message"] = "Contrato de aluguer editado com sucesso!";
                    return RedirectToAction("Index");
                }
                return View(rentalContract);

            }
            catch
            {
                ModelState.AddModelError("", "Ocorreu um erro ao editar o contrato de aluguer");
                ViewData["ClientId"] = new SelectList(_db.Clients, "ClientId", "Name");
                ViewData["VehicleId"] = new SelectList(
        _db.Vehicles.Where(v => v.Status == "Disponivel" || v.VehicleId == rentalContract.VehicleId),
        "VehicleId",
        "LicensePlate",
        rentalContract.VehicleId
    );
                return View(rentalContract);
            }
        }
    }
}