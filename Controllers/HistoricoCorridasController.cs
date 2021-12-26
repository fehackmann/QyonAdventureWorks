using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dominio.Entity;
using QyonAdventureWorks.Api.Data;

public class HistoricoCorridasController : Controller
{
    // GET: HistoricoCorridas
    public async Task<IActionResult> Index()
    {
        var qyonAdventureWorksApi = HistoricoCorrida.Include(h => h.Competidor).Include(h => h.PistaCorrida);
        return View(await qyonAdventureWorksApi.ToListAsync());
    }

    // GET: HistoricoCorridas/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var historicoCorrida = await HistoricoCorrida
            .Include(h => h.Competidor)
            .Include(h => h.PistaCorrida)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (historicoCorrida == null)
        {
            return NotFound();
        }

        return View(historicoCorrida);
    }

    // GET: HistoricoCorridas/Create
    public IActionResult Create()
    {
        ViewData["CompetidorId"] = new SelectList(Competidores, "Id", "Nome");
        ViewData["PistaCorridaId"] = new SelectList(PistaCorrida, "Id", "Descricao");
        return View();
    }

    // POST: HistoricoCorridas/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CompetidorId,PistaCorridaId,DataCorrida,TempoGasto,Id,DtCriacao,DtAlteracao")] HistoricoCorrida historicoCorrida)
    {
        if (ModelState.IsValid)
        {
            historicoCorrida.Id = Guid.NewGuid();
            Add(historicoCorrida);
            await SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CompetidorId"] = new SelectList(Competidores, "Id", "Nome", historicoCorrida.CompetidorId);
        ViewData["PistaCorridaId"] = new SelectList(PistaCorrida, "Id", "Descricao", historicoCorrida.PistaCorridaId);
        return View(historicoCorrida);
    }

    // GET: HistoricoCorridas/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var historicoCorrida = await HistoricoCorrida.FindAsync(id);
        if (historicoCorrida == null)
        {
            return NotFound();
        }
        ViewData["CompetidorId"] = new SelectList(Competidores, "Id", "Nome", historicoCorrida.CompetidorId);
        ViewData["PistaCorridaId"] = new SelectList(PistaCorrida, "Id", "Descricao", historicoCorrida.PistaCorridaId);
        return View(historicoCorrida);
    }

    // POST: HistoricoCorridas/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("CompetidorId,PistaCorridaId,DataCorrida,TempoGasto,Id,DtCriacao,DtAlteracao")] HistoricoCorrida historicoCorrida)
    {
        if (id != historicoCorrida.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                Update(historicoCorrida);
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoricoCorridaExists(historicoCorrida.Id))
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
        ViewData["CompetidorId"] = new SelectList(Competidores, "Id", "Nome", historicoCorrida.CompetidorId);
        ViewData["PistaCorridaId"] = new SelectList(PistaCorrida, "Id", "Descricao", historicoCorrida.PistaCorridaId);
        return View(historicoCorrida);
    }

    // GET: HistoricoCorridas/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var historicoCorrida = await HistoricoCorrida
            .Include(h => h.Competidor)
            .Include(h => h.PistaCorrida)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (historicoCorrida == null)
        {
            return NotFound();
        }

        return View(historicoCorrida);
    }

    // POST: HistoricoCorridas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var historicoCorrida = await HistoricoCorrida.FindAsync(id);
        HistoricoCorrida.Remove(historicoCorrida);
        await SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HistoricoCorridaExists(Guid id)
    {
        return HistoricoCorrida.Any(e => e.Id == id);
    }
}