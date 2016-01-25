﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xunit;

namespace Rental
{
    public class Tests
    {
        [Fact]
        public void PersonCreation()
        {
            Employee employee = new Employee("Vinko", "Zadric");
            Client client = new Client("Marin", "Veljko", employee);

            PersonRepository.Instance.Add(employee);
            PersonRepository.Instance.Add(client);

            // Check if the repository saved successfully
            Assert.Equal(client, PersonRepository.Instance.Get(client));
            Assert.Equal(employee, PersonRepository.Instance.Get(employee));

            // Test the client link created via the constructor
            Assert.Equal(client.DedicatedAgent, employee);
        }

        [Fact]
        public void FactoryCreationApartmant()
        {
            Employee employee = new Employee("Vinko", "Zadric");
            Client client = new Client("Marin", "Veljko", employee);

            PersonRepository.Instance.Add(employee);
            PersonRepository.Instance.Add(client);

            // Create some features (payed and included)
            List<SpecialFeatures> sF = new List<SpecialFeatures>();
            sF.Add(new SpecialFeatures(200, "Izlet na more."));
            sF.Add(new SpecialFeatures(150, "Vecer u finom restoranu."));

            List<RentalInclude> rF = new List<RentalInclude>();
            rF.Add(new RentalInclude(Offer.balcony, 2));
            rF.Add(new RentalInclude(Offer.kitchen, 2));
            rF.Add(new RentalInclude(Offer.room, 4));

            // Create the apartmant via the factory and add it to the repo
            Apartment a = ApartmanFactory.createApartman(client, "Vila zrinka", "Prekrasna vila na moru...",
                "12004", "Torovinkova 5", 200, rF, sF);
            RentalRepository.Instance.Add(a);

            Assert.Equal(RentalRepository.Instance.Get(a), a);
            Assert.Equal(RentalRepository.Instance.Get(a).Description, a.Description);
            Assert.Equal(RentalRepository.Instance.Get(a).Owner, client);
        }

        [Fact]
        public void RepositoryCRUDTestingPerson()
        {
            Employee employee1 = new Employee("Vinko", "Zadric");
            Employee employee2 = new Employee("Mlako", "Vader");
            Employee employee3 = new Employee("Hesimono", "Kaero");
            Client client = new Client("Marin", "Veljko", employee1);

            // Clean repos (because they are singeltons, they might still have some data left in them)
            PersonRepository.Instance.Clear();

            // Fill the repos
            PersonRepository.Instance.Add(employee1);
            PersonRepository.Instance.Add(employee2);
            PersonRepository.Instance.Add(employee3);
            PersonRepository.Instance.Add(client);

            // Check number
            Assert.Equal(PersonRepository.Instance.Count(), 4);

            // Check delete function of a repo
            PersonRepository.Instance.Remove(employee3);
            Assert.Equal(PersonRepository.Instance.Count(), 3);

            Assert.Equal(PersonRepository.Instance.Contains(employee3), false);
        }
    }
}
