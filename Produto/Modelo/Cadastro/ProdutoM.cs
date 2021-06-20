using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelo.Cadastro
{
    public class ProdutoM
    {
        [Key]
        public long? ProdutoID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name ="Descrição")]
        public string Descricao { get; set; }

        [Required]
        public string Distribuidora { get; set; }
    }
}
