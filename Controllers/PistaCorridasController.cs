using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dominio.Entity;
using QyonAdventureWorks.Api.Data;

public class PistaCorridasController : Controller
{

    // GET: PistaCorridas
    public async Task<IActionResult> Index()
    {
        return View(await PistaCorrida.ToListAsync());
    }

    // GET: PistaCorridas/Details/5
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pistaCorrida = await PistaCorrida
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pistaCorrida == null)
        {
            return NotFound();
        }

        return View(pistaCorrida);
    }

    // GET: PistaCorridas/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PistaCorridas/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Descricao,Id,DtCriacao,DtAlteracao")] PistaCorrida pistaCorrida)
    {
        if (ModelState.IsValid)
        {
            pistaCorrida.Id = Guid.NewGuid();
            Add(pistaCorrida);
            await SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(pistaCorrida);
    }

    // GET: PistaCorridas/Edit/5
    public async Task<IActionResult> Edit(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pistaCorrida = await PistaCorrida.FindAsync(id);
        if (pistaCorrida == null)
        {
            return NotFound();
        }
        return View(pistaCorrida);
    }

    // POST: PistaCorridas/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to, for 
    // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, [Bind("Descricao,Id,DtCriacao,DtAlteracao")] PistaCorrida pistaCorrida)
    {
        if (id != pistaCorrida.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                Update(pistaCorrida);
                await SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PistaCorridaExists(pistaCorrida.Id))
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
        return View(pistaCorrida);
    }

    // GET: PistaCorridas/Delete/5
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var pistaCorrida = await PistaCorrida
            .FirstOrDefaultAsync(m => m.Id == id);
        if (pistaCorrida == null)
        {
            return NotFound();
        }

        return View(pistaCorrida);
    }

    // POST: PistaCorridas/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(Guid id)
    {
        var pistaCorrida = await PistaCorrida.FindAsync(id);
        PistaCorrida.Remove(pistaCorrida);
        await SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PistaCorridaExists(Guid id)
    {
        return PistaCorrida.Any(e => e.Id == id);
    }
}