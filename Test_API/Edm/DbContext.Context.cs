﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test_API.Edm
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DBEntities : DbContext
    {
        public DBEntities()
            : base("name=DBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Person> People { get; set; }
    
        public virtual int RegisterUser(string p_UserName, string p_Password, string p_LastName, string p_FirstName, Nullable<System.DateTime> p_DateOfBirth, ObjectParameter responseMessage)
        {
            var p_UserNameParameter = p_UserName != null ?
                new ObjectParameter("p_UserName", p_UserName) :
                new ObjectParameter("p_UserName", typeof(string));
    
            var p_PasswordParameter = p_Password != null ?
                new ObjectParameter("p_Password", p_Password) :
                new ObjectParameter("p_Password", typeof(string));
    
            var p_LastNameParameter = p_LastName != null ?
                new ObjectParameter("p_LastName", p_LastName) :
                new ObjectParameter("p_LastName", typeof(string));
    
            var p_FirstNameParameter = p_FirstName != null ?
                new ObjectParameter("p_FirstName", p_FirstName) :
                new ObjectParameter("p_FirstName", typeof(string));
    
            var p_DateOfBirthParameter = p_DateOfBirth.HasValue ?
                new ObjectParameter("p_DateOfBirth", p_DateOfBirth) :
                new ObjectParameter("p_DateOfBirth", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("RegisterUser", p_UserNameParameter, p_PasswordParameter, p_LastNameParameter, p_FirstNameParameter, p_DateOfBirthParameter, responseMessage);
        }
    }
}
