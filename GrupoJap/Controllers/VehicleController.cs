using GrupoJap.Data;
using GrupoJap.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrupoJap.Controllers
{
    public class VehicleController : Controller
    {

        readonly private ApplicationDbContext _db;

        public VehicleController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Vehicle> vehicles = _db.Vehicles.ToList();
            return View(vehicles);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Vehicle vehicle)
        {
            try
            {
                var existingVehicle = _db.Vehicles.FirstOrDefault(v => v.LicensePlate == vehicle.LicensePlate);

                if (existingVehicle != null)
                {
                    ModelState.AddModelError("LicensePlate", "Já existe uma viatura com essa matricula.");
                    return View(vehicle);
                }

                if (ModelState.IsValid)
                {
                    _db.Vehicles.Add(vehicle);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(vehicle);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao registar a viatura");
                return View(vehicle);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var vehicle = await _db.Vehicles.FindAsync(id);
                if (vehicle == null)
                {

                    ModelState.AddModelError("", "Viatura não encontrada");
                    return RedirectToAction("Index");

                }

                _db.Vehicles.Remove(vehicle);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao eliminar a viatura");
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
            var vehicle = await _db.Vehicles.FindAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vehicle vehicle)
        {
            try
            {
                var existingVehicle = await _db.Vehicles.FindAsync(vehicle.VehicleId);

                if (existingVehicle == null)
                {
                    ModelState.AddModelError("", "Viatura não encontrada");
                    return View(vehicle);
                }

                var vehicleWithSameLicensePlate = _db.Vehicles.FirstOrDefault(v => v.LicensePlate == vehicle.LicensePlate && v.VehicleId != vehicle.VehicleId);

                if (vehicleWithSameLicensePlate != null)
                {
                    ModelState.AddModelError("LicensePlate", "Já existe uma viatura com essa matricula.");
                    return View(vehicle);
                }

                existingVehicle.Brand = vehicle.Brand;
                existingVehicle.Model = vehicle.Model;
                existingVehicle.LicensePlate = vehicle.LicensePlate;
                existingVehicle.ManufacturingYear = vehicle.ManufacturingYear;
                existingVehicle.FuelType = vehicle.FuelType;


                if (ModelState.IsValid)
                {
                    _db.Vehicles.Update(existingVehicle);
                    await _db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }

                return View(vehicle);

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao editar a viatura");
                return View(vehicle);
            }

        }

        }
}
