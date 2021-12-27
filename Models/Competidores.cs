using System;
using System.Collections.Generic;
using System.Component.Model.DataAnnotations;
using System.Ling;
using System.Threading.Tasks;

public class Competidores //classe competidores e declaração de atributos
{
    [Required]
    [MaxLenght(150, ErrorMessage = "Campo com limite de 150 caracteres")]
    public varchar Nome {get; set}

    [Required]
    public char SexoCompetidor 
    {
        M,
        F
    }

    [Required]
    public decimal TemperaturaMediaCorpo {get; set}

    [Required]
    public decimal Peso {get; set}

    [Required]
    public decimal Altura {get; set}
}