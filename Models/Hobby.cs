using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using c_belt.Models;

namespace c_belt.Models
{
    public class Hobby
    {
        [Key]
        [Column("id")]
        public int HobbyId {get;set;}



        [Column("user_id")]
        public int UserId {get;set;}



        [Column("name")]
        [Required(ErrorMessage="Name is required!")]
        public string Name {get;set;}
        



        [Column("description")]
        [Required(ErrorMessage="Description is required!")]
        public string Description {get;set;}


        [Column("created_at")]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        
        
        
        [Column("updated_at")]
        public DateTime UpdatedAt {get;set;} = DateTime.Now;


        // Navigation Properties    
        public User Creator {get;set;}
        public List<Join> Joiners {get;set;}
    }


}


