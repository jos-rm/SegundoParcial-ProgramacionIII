using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaParcial2_JoselineRuiz.Models
{
    public enum EstadoTarea
    {
        Pendiente = 0,
        EnProgreso = 1,
        Completada = 2
    }
    public enum DificultadTarea 
    { 
        Facil = 0,
        Media = 1,
        Dificil = 2
    }

    public class Tarea
    {
        [Key]
        public int IdTarea { get; set; }

        [Required(ErrorMessage = "La descripción es requerida")]
        [MinLength(5, ErrorMessage = "Mínimo 5 caracteres")]
        [MaxLength(200, ErrorMessage = "Máximo 200 caracteres")]
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }

        [Required]
        [DisplayName("Fecha de Creación")]
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        [DisplayName("Fecha Límite")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateOnly? FechaLimite { get; set; }

        [Required]
        public EstadoTarea Estado { get; set; } = EstadoTarea.Pendiente;

        [Required]
        public DificultadTarea Dificultad { get; set; }

        [Required]
        [DisplayName("Tiempo Estimado")]
        public TimeOnly TiempoEstimado { get; set; }

        //relación uno a muchos con Meta
        [ForeignKey("Meta")]
        public int MetaId { get; set; }
        public virtual Meta? Meta { get; set; }
    }
}
