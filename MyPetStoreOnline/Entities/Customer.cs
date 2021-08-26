using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPetStoreOnline.Entities
{
    public class Customer
    {
        public int Id { get; private set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; private set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; private set; }
        
        [MaxLength(13)]
        public string Phone { get; private set; }

        [Required]
        [MaxLength(200)]
        public string Email { get; private set; }
        public Address Address { get; set; }

        public ICollection<Order> Orders { get; private set; }
        public Customer()
        {
            // Usado por EF
        }


        // TODO: pedir el userId y delegar el nombre, correo y telefono a la cuenta de usuario
        public Customer(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new InvalidOperationException("El nombre es requerido");

            if (string.IsNullOrEmpty(lastName))
                throw new InvalidOperationException("El apellido es requerido");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("El correo es requerido");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public void Update(string firstName, string lastName, string email)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new InvalidOperationException("El nombre es requerido");

            if (string.IsNullOrEmpty(lastName))
                throw new InvalidOperationException("El apellido es requerido");

            if (string.IsNullOrEmpty(email))
                throw new InvalidOperationException("El correo es requerido");

            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        // agregar o editar teléfono
        public void AddOrUpdatePhone(string phone)
        {
            if (string.IsNullOrEmpty(phone) || phone.Length < 10)
                throw new InvalidOperationException("El teléfono es inválido");

            if (!long.TryParse(phone, out var _))
                throw new InvalidOperationException("El teléfono solo puede contener dígitos");

            Phone = phone;
        }
    }
}