using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using c_belt.Models;

    namespace c_belt.Models
    {
        public class User
        {
            [Key]
            [Column("id")]
            public int UserId {get;set;}
            

            [Required(ErrorMessage="First name is required and must be at least 2 characters long ")]
            [MinLength(2, ErrorMessage="First name is required and must be at least 2 characters long ")]
            [RegularExpression("^[a-zA-Z ]*$")]
            [Display(Name = "name")]
            public string Name {get; set;}



            [Required(ErrorMessage="Username is required")]
            [MinLength(3 ,ErrorMessage = "Invalid Username")]
            [Display(Name = "Username")]
            public string Username {get; set;}



            [MinLength(6, ErrorMessage = "Password MUST be 8 characters long")]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password {get; set;}



            [Compare("Password", ErrorMessage="Your Passwords should match")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirm")]
            public string Confirm {get; set;}



            [Column("created_at")] 
            public DateTime CreatedAt {get;set;} = DateTime.Now; 



            [Column("updated_at")] 
            public DateTime UpdatedAt {get;set;} = DateTime.Now; 


        // Navigation Properties
        public List<Hobby> CreatedHobby {get;set;}
        public List<Join> JoinIssued {get;set;}



        }
    }

  


    