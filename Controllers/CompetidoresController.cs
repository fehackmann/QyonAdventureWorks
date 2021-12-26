using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dominio.Entity;
using QyonAdventureWorks.Api.Data;

public class CompetidoresController : Controller
{
    // GET: Competidores
        public async Task<IActionResult> Index()
        {
            return View(await Competidores.ToListAsync());
        }

    // GET: Competidores/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var competidores = await Competidores
            .FirstOrDefaultAsync(m => m.Id == id);
        if (competidores == null)
        {
            return NotFound();
        }

        return View(competidores);
    }

    // POST: Competidores/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Nome,Sexo,TemperaturaMediaCorpo,Peso,Altura,Id,DtCriacao,DtAlteracao")] Competidores competidores)
    {
        if (id != competidores.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                Update(competidores);
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompetidoresExists(competidores.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(competidores);
    }

    // GET: Competidores/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var competidores = await Competidores
            .FirstOrDefaultAsync(m => m.Id == id);
        if (competidores == null)
        {
            return NotFound();
        }

        return View(competidores);
    }

    // POST: Competidores/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var competidores = await Competidores.FindAsync(id);
        Competidores.Remove(competidores);
        await SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CompetidoresExists(Guid id)
    {
        return Competidores.Any(e => e.Id == id);
    }
}

