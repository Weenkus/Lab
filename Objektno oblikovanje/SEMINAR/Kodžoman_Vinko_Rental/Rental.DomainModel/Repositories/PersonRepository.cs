﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rental
{
    public class PersonRepository
    {
        // Repo is a singlton, make data consistancy easier and more practical
        private static PersonRepository instance;
        private List<Person> _personList = new List<Person>();

        private PersonRepository() { }

        public static PersonRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonRepository();
                }
                return instance;
            }
        }

        public int Count()
        {
            return _personList.Count;
        }

        public Person Get(int id)
        {
            return _personList.Where(p => p.Id == id).SingleOrDefault();
        }

        public Person Get(Person person)
        {
            return _personList.Where(p => p.Equals(person)).SingleOrDefault();
        }

        public void Add(Person person)
        {
            _personList.Add(person);
        }

        public void Remove(int id)
        {
            _personList.RemoveAll(p => p.Id == id);
        }
    }
}
