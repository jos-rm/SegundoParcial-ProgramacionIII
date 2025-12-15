using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaParcial2_JoselineRuiz.Models
{
    public enum CategoriaMeta 
    {
        DesarrolloPersonal = 0,
        Carrera = 1,
        Salud = 2,
        Finanzas = 3,
        Relaciones = 4,
        Otros = 5
    }

    public enum PrioridadMeta
    {
        Alta = 0,
        Media = 1,
        Baja = 2
    }

    public enum EstadoMeta
    {
        NoIniciada = 0,
        EnProgreso = 1,
        Completada = 2,
        Abandonada = 3
    }

    public class Meta
    {
        [Key]
        public int IdMeta { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Mínimo 5 caracteres")]
        [MaxLength(100, ErrorMessage = "Máximo 200 caracteres")]
        [DisplayName("Título")]
        public string Titulo { get; set; }

        [StringLength(200)]
        [DisplayName("Descripción")]
        public string? Descripcion { get; set; }

        [Required]
        [DisplayName("Categoría")]
        public CategoriaMeta Categoria { get; set; }

        [Required]
        [DisplayName("Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [DisplayName("Fecha Límite")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly? FechaLimite { get; set; }

        [Required]
        public PrioridadMeta Prioridad { get; set; }

        [Required]
        public EstadoMeta Estado { get; set; }

        //Propiedad de navegacion
        public virtual  ICollection<Tarea> Tareas { get; set; } = new List<Tarea>();
    }
}
