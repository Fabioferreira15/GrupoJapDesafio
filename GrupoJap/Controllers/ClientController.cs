using GrupoJap.Data;
using GrupoJap.Models;
using Microsoft.AspNetCore.Mvc;

namespace GrupoJap.Controllers
{
    public class ClientController : Controller
    {

        readonly private ApplicationDbContext _db;

        public ClientController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Client> clients = _db.Clients.ToList();
            return View(clients);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Client client)
        {

            try
            {



                var existingClient = _db.Clients.FirstOrDefault(c => c.Email == client.Email);

                if (existingClient != null)
                {
                    ModelState.AddModelError("Email", "Já existe um cliente registado com esse email");
                    return View(client);
                }

                if (ModelState.IsValid)
                {
                    _db.Clients.Add(client);
                    await _db.SaveChangesAsync();

                    TempData["Message"] = "Cliente registado com sucesso!";

                    return RedirectToAction("Index");
                }

                return View(client);

            }
            catch (Exception ex) 
            { 
                ModelState.AddModelError("", "Ocorreu um erro ao registar o cliente");
                return View(client);
            }
         
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = await _db.Clients.FindAsync(id);

                if (client == null)
                {
                    ModelState.AddModelError("", "Cliente não encontrado");
                    return RedirectToAction("Index");
                }



                _db.Clients.Remove(client);
                await _db.SaveChangesAsync();

                TempData["Message"] = "Cliente eliminado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao eliminar o cliente");
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
            var client = await _db.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(Client client)
        {
            try
            {
                
                var existingClient = await _db.Clients.FindAsync(client.ClientId);

                if (existingClient == null)
                {
                    ModelState.AddModelError("", "Cliente não encontrado.");
                    return RedirectToAction("Index");
                }

                
                var clientWithSameEmail = _db.Clients
                    .FirstOrDefault(c => c.Email == client.Email && c.ClientId != client.ClientId);

                if (clientWithSameEmail != null)
                {
                    ModelState.AddModelError("Email", "Já existe um cliente registado com esse email");
                    return View(client);  
                }

                
                existingClient.Name = client.Name;
                existingClient.Email = client.Email;
                existingClient.PhoneNumber = client.PhoneNumber;
                existingClient.DrivingLicense = client.DrivingLicense;

                if (ModelState.IsValid)
                {
                    _db.Clients.Update(existingClient);
                    await _db.SaveChangesAsync();

                    TempData["Message"] = "Cliente editado com sucesso!";

                    return RedirectToAction("Index");
                }

                return View(client);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao editar o cliente");
                return View(client);
            }
        }

    }
}
