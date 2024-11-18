using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SWE30003_Group5_Koala.Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }

        [Required]
        [Range(1, 20, ErrorMessage = "Party size must be between 1 and 20.")]
        [Display(Name = "Party Size")]
        public int PartySize { get; set; }

        [Required]
        [Display(Name = "Reservation Time")]
        public DateTime Time { get; set; }
        [ForeignKey("Table")]
        [Display(Name = "Assigned Table")]
        public int TableID { get; set; }
        [ValidateNever]
        public Table Table { get; set; }

        [Required]
        [Display(Name = "Reservation Status")]
        [StringLength(20)]
        public string Status { get; set; } // "Pending" (Default), "Confirmed", "Cancelled"
    }

}
