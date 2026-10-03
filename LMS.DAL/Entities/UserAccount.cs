using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.DAL.Entities
{
    public class UserAccount:IdentityUser
    {
        public string Fname { get; private set; }
        public string Lname { get; private set; }
        public DateTime DOB { get; private set; }
        public string Gender { get; private set; }
        public string? ProfilePicture { get; private set; }
        public bool IsDeleted { get; private set; } = false; 
        public DateTime CreatedOn { get; private set; } 
        public DateTime? DeletedOn { get; private set; }
        public DateTime? LastUpdatedOn { get; private set; }
        public Instructor? Instructor { get; set; }
        public Student? Student { get; set; }
        private UserAccount() : base() { }
        public UserAccount(string fname, string lname, string email, string userName, string gender, DateTime dob, string? phoneNumber = null) : base()
        {
            if (string.IsNullOrWhiteSpace(fname))
                throw new ArgumentException("First name is required.");

            if (string.IsNullOrWhiteSpace(lname))
                throw new ArgumentException("Last name is required.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required for account registration.");

            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("UserName is required.");

            if (string.IsNullOrWhiteSpace(gender))
                throw new ArgumentException("Gender must be specified.");

            if (dob == default)
                throw new ArgumentException("Date of birth is required.");

            if (dob > DateTime.UtcNow)
                throw new ArgumentException("Date of birth cannot be in the future.");

            if (dob > DateTime.UtcNow.AddYears(-5))
                throw new ArgumentException("User must be at least 5 years old.");

            Fname = fname;
            Lname = lname;
            Email = email;
            UserName = userName;
            Gender = gender;
            PhoneNumber = phoneNumber;
            DOB = dob;
            CreatedOn = DateTime.UtcNow;
        }
        public void UpdateName(string fname, string lname)
        {
            if (string.IsNullOrWhiteSpace(fname) || string.IsNullOrWhiteSpace(lname))
                throw new ArgumentException("Both first and last names are required for profile updates.");
            Fname = fname;
            Lname = lname;
            LastUpdatedOn = DateTime.UtcNow;
        }

        public void SetProfilePicture(string profilePicturePath)
        {
            if (string.IsNullOrWhiteSpace(profilePicturePath))
                throw new ArgumentException("Profile picture path cannot be empty.");

            ProfilePicture = profilePicturePath;
            LastUpdatedOn = DateTime.UtcNow;
        }

        public void RemoveProfilePicture()
        {
            ProfilePicture = null;
            LastUpdatedOn = DateTime.UtcNow;
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.");

            PhoneNumber = phoneNumber;
            LastUpdatedOn = DateTime.UtcNow;
        }
        public void RemovePhoneNumber()
        {
            PhoneNumber = null;
            LastUpdatedOn = DateTime.UtcNow;
        }
        public void SoftDelete()
        {
            IsDeleted = true;
            DeletedOn = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            DeletedOn = null;
        }
    }
}
