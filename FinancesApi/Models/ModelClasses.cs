using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinancesApi.Models
{
    public class Income
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Income Title Must be provided")]
        [StringLength(50, MinimumLength = 2)]
        public string IncomeTitle { get; set; }

        [Required(ErrorMessage = "Please provide Date Income comes in")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime? IncomeInDate { get; set; }

        [Required(ErrorMessage = "Income amount is must")]
        [DataType(DataType.Currency)]
        public int Amount { get; set; }
    }
}