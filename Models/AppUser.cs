/*AppUser.cs
This file contains the AppUser class. 
The AppUser class is one of the Model classes used by the ProjectManager web application. Each model in the application represents one entity 
in the application. This entity is used in the client side, on the server side, and is also used to define and create database schemes and relationships.

The AppUser class represents a user in the ProjectManager web application. This class also inherits from the IdentityUser class (provided by the Identity 
system in ASP.NET Core, which helps handle user accounts and authorization). This class adds additional attributes to the IdentityUser class so that 
the Model represented by the class fits the needs of the ProjectManager web application
*/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace ProjectManager.Models
{
    //the user data and profile for our application
    //extends the IdentityUser class, which provides attributes needed for authentication (like unique username, password, etc)
    public class AppUser : IdentityUser
    {
        //the first name of the user
        [Required]
        [MaxLength(50)]
        public string FirstName {get; set;}

        //the last name of the user
        [Required]
        [MaxLength(50)]
        public string LastName {get; set;}

        //the user's bio
        // [MaxLength(50)]
        // public string Email {get; set;}
        [MaxLength(300)]
        public string Bio{get; set;}
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]    
        List<ProjectUser> ProjectUsers {get; set;}
        //navigation property, used to set up Entity Framework Core table foreign key relationship and table-to-table relationships
        //not directly used by the application
        [JsonIgnore]
        List<TaskUser> TaskUsers {get; set;}

    }
}