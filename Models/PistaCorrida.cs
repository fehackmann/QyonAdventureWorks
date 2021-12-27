using System;
using System.Collections.Generic;
using System.Component.Model.DataAnnotations;
using System.Ling;
using System.Threading.Tasks;

public class PistaCorrida //classe pistacorrida e declaração de atributos
{
    [Required]
    [MaxLenght(50, ErrorMessage = "Campo com limite de 50 caracteres")]
    public varchar Descricao
}