using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace P0_ChrisSophiea
{
    public class Customer
    {
        // public Customer(string fname, string lname){
        //     this.Fname = fname;
        //     this.Lname = lname;
        // }

        [Key]
        public int CustomerId { get; set; }

        private string fName;
        public string Fname
        {
            get { return fName; }
            set
            {
                if (value is string && value.Length < 20 && value.Length > 0)
                {
                    fName = value;
                }
                else
                {
                    throw new Exception("The customer name you sent is not valid");
                }
            }
        }

        private string lName;
        public string Lname
        {
            get { return lName; }
            set
            {
                if (value is string && value.Length < 20 && value.Length > 0)
                {
                    lName = value;
                }
                else
                {
                    throw new Exception("The customer name you sent is not valid");
                }
            }
        }

        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
            set
            {
                if (value is string && value.Length < 50 && value.Length > 6 && value.Contains("@"))
                {
                    emailAddress = value;
                }
                else
                {
                    Console.WriteLine("The email address you entered is not valid");
                }
            }
        }
        //[InverseProperty("Customer1")]
        public ICollection<Purchase> Purchases { get; set; }

        public override string ToString()
        {
            return $"Customer ID: {CustomerId} | Name: {Fname} {Lname} | Email Address: {EmailAddress}";
        }
    }
}
