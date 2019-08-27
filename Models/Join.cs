using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace c_belt.Models
{
    public class Join
    {
        [Key]
        [Column("id")]
        public int JoinId {get;set;}
   

        [Column("hobby_Id")]
        public int HobbyId {get;set;}



        [Column("user_id")]
        public int UserId {get;set;}



        [Column("join")]
        public bool join {get;set;}


        

        // Navigation Properties
        public User xUser {get;set;}
        public Hobby xHobby {get;set;}
    }
}