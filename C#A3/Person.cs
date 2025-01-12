using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_A3
{
    /// <summary>
    /// Holds the information assigned to an instance of "person".
    /// </summary>
    internal class Person
    {
        public string Name { get; private set; } //Public get, private set. Only this class can manipulate values.
        public double Age { get; private set; }
        public double Weight { get; private set; }
        public double Height { get; private set; }
        public Enums.Gender PersonGender { get; private set; }
        public Enums.ActivityLevel PersonActivityLevel { get; private set; }

        /// <summary>
        /// Constructor assigns validated input values to instance variables of the class.
        /// </summary>
        /// <param name="name">The name of the person</param>
        /// <param name="age">The age of the person</param>
        /// <param name="height">The height of the person</param>
        /// <param name="weight">The weight of the person</param>
        /// <param name="personGender">The gender of the person</param>
        /// <param name="personActivityLevel">The activity level of the person</param>
        public Person(string name, double age, double height, double weight, Enums.Gender personGender, Enums.ActivityLevel personActivityLevel)
        {
            this.Name = name;
            this.Age = age;
            this.Weight = weight;
            this.Height = height;
            this.PersonGender = personGender;
            this.PersonActivityLevel = personActivityLevel;
        }

    }
}
