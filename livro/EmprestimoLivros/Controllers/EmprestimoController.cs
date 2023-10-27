using EmprestimoLivros.Data;
using EmprestimoLivros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace EmprestimoLivros.Controllers
{
    public class EmprestimoController : Controller
    {
        private readonly ApplicationDbContext _db;

        public EmprestimoController(ApplicationDbContext db)
        {
                _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<EmprestimoModel> emprestimos = _db.EmprestimoModels;
            
            return View(emprestimos);
        }
        [HttpGet]
        public IActionResult Registar()
        {
            return View();
        }

        [HttpGet]

        public IActionResult Editar(int? Id) 
        {
            if(Id ==null || Id == 0)
            {
                return NotFound();
            }
            EmprestimoModel emprestimo = _db.EmprestimoModels.FirstOrDefault(x => x.Id == Id);
            
            if(emprestimo == null)
            {
                return NotFound();
            }
           
            return View(emprestimo);
        }

        [HttpGet]

        public IActionResult Excluir(int? Id) 
        {
            if (Id == null || Id == 0)
            {
                return NotFound();
            }

            EmprestimoModel emprestimo = _db.EmprestimoModels.FirstOrDefault(x =>x.Id == Id);   

            if(emprestimo == null) 
            {
                return NotFound();
            }
            return View(emprestimo);
        }
        
        
         
        [HttpPost]
        public IActionResult Registar(EmprestimoModel emprestimos) 
        {
            if (ModelState.IsValid)
            {
                _db.EmprestimoModels.Add(emprestimos);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Registo efetuado com sucesso!";
                
                return RedirectToAction("Index");
            }
            TempData["MensagemErro"] = "Erro ao tentar adicionar registo, tente novamente";
            return View();
        }

        [HttpPost]
        
        public IActionResult Editar(EmprestimoModel emprestimo)
        {
            if (ModelState.IsValid)
            {
                _db.EmprestimoModels.Update(emprestimo);
                _db.SaveChanges();

                TempData["MensagemSucesso"] = "Edição efetuada com sucesso!";
                
                return RedirectToAction("Index");

                
            }

            TempData["MensagemErro"] = "Erro ao tentar editar, tente novamente";
            return View(); 
        }

        [HttpPost]

        public IActionResult Excluir(EmprestimoModel emprestimo) 
        {
            if (emprestimo == null)
            {
                return NotFound();

            }

            _db.EmprestimoModels.Remove(emprestimo);
            _db.SaveChanges();

            TempData["MensagemSucesso"] = "Exclusão efetuada com sucesso!";
            return RedirectToAction("Index");

           
        }

        



    }
}
